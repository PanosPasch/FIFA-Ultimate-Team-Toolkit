using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;
using UltimateTeam.Toolkit.Services;

namespace UltimateTeam.Toolkit.Requests
{
    internal class LoginMobileRequest : FutRequestBase, IFutRequest<LoginResponse>
    {
        private readonly LoginDetails _loginDetails;

        private readonly ITwoFactorCodeProvider _twoFactorCodeProvider;

        private string _accessToken;

        private string _route;

        private IHasher _hasher;

        public IHasher Hasher
        {
            get { return _hasher ?? (_hasher = new Hasher()); }
            set { _hasher = value; }
        }

        public LoginMobileRequest(LoginDetails loginDetails, ITwoFactorCodeProvider twoFactorCodeProvider)
        {
            loginDetails.ThrowIfNullArgument();
            _loginDetails = loginDetails;
            _twoFactorCodeProvider = twoFactorCodeProvider;
        }

        public void SetCookieContainer(CookieContainer cookieContainer)
        {
            HttpClient.MessageHandler.CookieContainer = cookieContainer;
        }

        public async Task<LoginResponse> PerformRequestAsync()
        {
            try
            {
                var mainPageResponseMessage = await GetMainPageAsync().ConfigureAwait(false);
                string code = "";
                if (!(await IsLoggedInAsync()))
                    code = await LoginAsync(_loginDetails, mainPageResponseMessage);
                var nucleusId = await GetNucleusIdAsync(code);
                var shards = await GetShardsAsync(nucleusId);
                var userAccounts = await GetUserAccountsAsync(_loginDetails.Platform);
                var sessionId = await GetSessionIdAsync(userAccounts, _loginDetails.Platform);
                var personaId = sessionId.Split(':')[1];
                var personaName = sessionId.Split(':')[2];
                sessionId = sessionId.Split(':')[0];
                var phishingToken = await ValidateAsync(_loginDetails, sessionId);

                return new LoginResponse(nucleusId, shards, userAccounts, sessionId, phishingToken, personaId, personaName);
            }
            catch (Exception e)
            {
                throw new FutException("Unable to login", e);
            }
        }

        private async Task<bool> IsLoggedInAsync()
        {
            var loginResponse = await HttpClient.GetAsync(Resources.LoggedIn);
            var loggedInResponse = await Deserialize<IsUserLoggedIn>(loginResponse);

            return loggedInResponse.IsLoggedIn;
        }

        private async Task<string> ValidateAsync(LoginDetails loginDetails, string sessionId)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, sessionId);
            var validateResponseMessage = await HttpClient.PostAsync(string.Format("{0}/ut/game/fifa16/phishing/validate?_={1}", _route, CreateTimestamp()), new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("answer", Hasher.Hash(loginDetails.SecretAnswer))
                }));
            var validateResponse = await Deserialize<ValidateResponse>(validateResponseMessage);

            return validateResponse.Token;
        }

        private async Task<string> GetSessionIdAsync(UserAccounts userAccounts, Platform platform)
        {
            var persona = userAccounts
                .UserAccountInfo
                .Personas
                .FirstOrDefault(p => p.UserClubList.Any(club => club.Platform == GetNucleusPersonaPlatform(platform)));
            if (persona == null)
            {
                throw new FutException("Couldn't find a persona matching the selected platform");
            }

            var accessTokenResponse = await HttpClient.GetAsync(string.Format("https://accounts.ea.com/connect/auth?client_id=FOS-SERVER&redirect_uri=nucleus:rest&response_type=code&access_token={0}&machineProfileKey=f4fc1ca4da8ded7f", _accessToken));
            accessTokenResponse.EnsureSuccessStatusCode();

            var session_code = (await Deserialize<GatewayResponse>(accessTokenResponse)).code;

            var authResponseMessage = await HttpClient.PostAsync(string.Format("{0}/ut/auth?timestamp={1}", _route, CreateTimestamp()), new StringContent(
               string.Format(@"{{ ""isReadOnly"": false, ""sku"": ""FUT16AND"", ""clientVersion"": 18, ""nucleusPersonaId"": {0}, ""gameSku"": ""{2}"", ""locale"": ""en-GB"", ""method"": ""authcode"", ""priorityLevel"":4, ""identification"": {{ ""authCode"": ""{4}"", ""redirectUrl"": ""nucleus:rest"" }} }}",
                    persona.PersonaId, persona.PersonaName, GetGameSku(platform), GetNucleusPersonaPlatform(platform), session_code)));
            authResponseMessage.EnsureSuccessStatusCode();
            var sessionId = Regex.Match(await authResponseMessage.Content.ReadAsStringAsync(), "\"sid\":\"\\S+\"")
                .Value
                .Split(new[] { ':' })[1]
                .Replace("\"", string.Empty);

            return sessionId + ":" + persona.PersonaId.ToString() + ":" + persona.PersonaName;
        }

        public static string GetGameSku(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                    return "FFA16PS3";
                case Platform.Ps4:
                    return "FFA16PS4";
                case Platform.Xbox360:
                    return "FFA16XBX";
                case Platform.XboxOne:
                    return "FFA16XBO";
                case Platform.Pc:
                    return "FFA16PCC";
                default:
                    throw new ArgumentOutOfRangeException(/*nameof(platform)*/platform.ToString(), platform, null);
            }
        }

        public static string GetNucleusPersonaPlatform(Platform platform)
        {
            switch (platform)
            {
                case Platform.Ps3:
                case Platform.Ps4:
                    return "ps3";
                case Platform.Xbox360:
                case Platform.XboxOne:
                    return "360";
                case Platform.Pc:
                    return "pc";
                default:
                    throw new ArgumentOutOfRangeException("platform");
            }
        }

        private async Task<UserAccounts> GetUserAccountsAsync(Platform platform)
        {
            //HttpClient.RemoveRequestHeader(NonStandardHttpHeaders.Route);
            var route = string.Format("https://utas.{0}.fut.ea.com:443", platform == Platform.Xbox360 || platform == Platform.XboxOne ? "s3" : "s2");
            //HttpClient.AddRequestHeader(NonStandardHttpHeaders.Route, route);
            _route = route;
            var accountInfoResponseMessage = await HttpClient.GetAsync(string.Format("{0}/ut/game/fifa16/user/accountinfo?sku=FUT16AND&_={1}", route, CreateTimestamp()));

            return await Deserialize<UserAccounts>(accountInfoResponseMessage);
        }

        private async Task<Shards> GetShardsAsync(string nucleusId)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.NucleusId, nucleusId);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            //HttpClient.AddRequestHeader(NonStandardHttpHeaders.Route, "https://utas.fut.ea.com");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.RequestedWith, "com.ea.fifaultimate_row");
            AddAcceptHeader("application/json, text/javascript");
            AddAcceptLanguageHeader();
            AddReferrerHeader(Resources.BaseShowoff);
            var shardsResponseMessage = await HttpClient.GetAsync(string.Format("https://utas.mob.v2.fut.ea.com/ut/shards/v2?_={0}", CreateTimestamp()));

            return await Deserialize<Shards>(shardsResponseMessage);
        }

        private async Task<string> GetNucleusIdAsync(string code)
        {
            var prenucleusResponse = await HttpClient.PostAsync(string.Format("https://accounts.ea.com/connect/token?grant_type=authorization_code&code={0}&client_id=FIFA-16-MOBILE-COMPANION&client_secret=KrEoFK9ssvXKRWnTMgAu1OAMn7Y37ueUh1Vy7dIk2earFDUDABCvZuNIidYxxNbhwbj3y8pq6pSf8zBW", code), new FormUrlEncodedContent(
                new[]{
                    new KeyValuePair<string, string>("","")
                }));
            prenucleusResponse.EnsureSuccessStatusCode();

            GatewayResponse response = await Deserialize<GatewayResponse>(prenucleusResponse);
            var token_type = response.token_type;
            var access_token = response.access_token;
            _accessToken = access_token;

            AddGatewayHeaders(token_type, access_token);

            var nucleusResponseMessage = await HttpClient.GetAsync("https://gateway.ea.com/proxy/identity/pids/me");
            nucleusResponseMessage.EnsureSuccessStatusCode();
            var nucleusId = ((await Deserialize<GatewayModel>(nucleusResponseMessage)).pid.pidId).ToString();

            return nucleusId;
        }


        private async Task<string> LoginAsync(LoginDetails loginDetails, HttpResponseMessage mainPageResponseMessage)
        {
            var loginResponseMessage = await HttpClient.PostAsync(mainPageResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                                         new[]
                                                                                                                         {
                                                                                                                             new KeyValuePair<string, string>("email", loginDetails.Username),
                                                                                                                             new KeyValuePair<string, string>("password", loginDetails.Password),
                                                                                                                             new KeyValuePair<string, string>("_rememberMe", "on"),
                                                                                                                             new KeyValuePair<string, string>("rememberMe", "on"),
                                                                                                                             new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                             new KeyValuePair<string, string>("facebookAuth", "")
                                                                                                                         }));
            loginResponseMessage.EnsureSuccessStatusCode();

            string code = "";

            try
            {
                code = (await loginResponseMessage.Content.ReadAsStringAsync());
            }
            catch
            {
                code = "";
            }

            //check if twofactorcode is required
            var contentData = await loginResponseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("We sent a security code to your") || contentData.Contains("Your security code was sent to"))
                code = await SetTwoFactorCodeAsync(loginResponseMessage);

            return code;
        }

        private async Task<string> SetTwoFactorCodeAsync(HttpResponseMessage loginResponse)
        {
            var tfCode = await _twoFactorCodeProvider.GetTwoFactorCodeAsync();

            var responseContent = await loginResponse.Content.ReadAsStringAsync();

            AddReferrerHeader(loginResponse.RequestMessage.RequestUri.ToString());

            var codeResponseMessage = await HttpClient.PostAsync(loginResponse.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                              new[]
                                                                                                              {
                                                                                                                  new KeyValuePair<string, string>(responseContent.Contains("twofactorCode") ? "twofactorCode" : "twoFactorCode", tfCode),
                                                                                                                  new KeyValuePair<string, string>("_eventId", "submit"),
                                                                                                                  new KeyValuePair<string, string>("_trustThisDevice", "on"),
                                                                                                                  new KeyValuePair<string, string>("trustThisDevice", "on")
                                                                                                              }));

            codeResponseMessage.EnsureSuccessStatusCode();

            _twoFactorCodeProvider.StoreNxCookie(HttpClient.MessageHandler.CookieContainer, _loginDetails.Username);

            var contentData = await codeResponseMessage.Content.ReadAsStringAsync();

            if (contentData.Contains("Incorrect code entered"))
                throw new FutException("Incorrect TwoFactorCode entered.");
            else if (contentData.Contains("Authenticator"))
            {
                codeResponseMessage = await HttpClient.PostAsync(codeResponseMessage.RequestMessage.RequestUri, new FormUrlEncodedContent(
                                                                                                              new[]
                                                                                                              {
                                                                                                                  new KeyValuePair<string, string>("_eventId", "cancel"),
                                                                                                                  new KeyValuePair<string, string>("appDevice", "IPHONE")
                                                                                                              }));
            }

            return (await codeResponseMessage.Content.ReadAsStringAsync());
        }

        private async Task<HttpResponseMessage> GetMainPageAsync()
        {
            AddUserAgent();
            AddAcceptEncodingHeader();
            AddXRequestedWith();
            AddXWapProfile();
            var mainPageResponseMessage = await HttpClient.GetAsync("https://accounts.ea.com/connect/auth?client_id=FIFA-16-MOBILE-COMPANION&response_type=code&display=web2/login&scope=basic.identity+offline+signin&locale=en_US&prompt=login&machineProfileKey=f4fc1ca4da8ded7f");
            mainPageResponseMessage.EnsureSuccessStatusCode();

            //check if twofactorcode is required
            var contentData = await mainPageResponseMessage.Content.ReadAsStringAsync();
            if (contentData.Contains("We sent a security code to your") || contentData.Contains("Your security code was sent to"))
                await SetTwoFactorCodeAsync(mainPageResponseMessage);

            return mainPageResponseMessage;
        }

        private static long CreateTimestamp()
        {
            var duration = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);

            return ((long)(1000 * duration.TotalSeconds));
        }
    }
}