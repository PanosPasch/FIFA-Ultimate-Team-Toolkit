using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Exceptions;
using UltimateTeam.Toolkit.Extensions;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    public abstract class FutRequestBase
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error };

        private string _nucleusId;

        private string _personaId;

        private string _phishingToken;

        private string _sessionId;

        private IHttpClient _httpClient;

        public string NucleusId
        {
            get
            {
                if (_nucleusId == null)
                {
                    return "0";
                }
                else
                {
                    return _nucleusId;
                }
            }
            set
            {
                value.ThrowIfInvalidArgument();
                _nucleusId = value;
            }
        }

        public string PersonaId
        {
            get
            {
                if (_personaId == null)
                {
                    return "";
                }
                else
                {
                    return _personaId;
                }
            }
            set
            {
                value.ThrowIfInvalidArgument();
                _personaId = value;
            }
        }

        public string PhishingToken
        {
            set
            {
                value.ThrowIfInvalidArgument();
                _phishingToken = value;
            }
        }

        public string SessionId
        {
            get
            {
                if (_sessionId == null)
                {
                    return "";
                }
                else
                {
                    return _sessionId;
                }
            }
            set
            {
                value.ThrowIfInvalidArgument();
                _sessionId = value;
            }
        }

        internal Resources Resources { get; set; }

        internal IHttpClient HttpClient
        {
            get { return _httpClient; }
            set
            {
                value.ThrowIfNullArgument();
                _httpClient = value;
            }
        }

        protected void AddCommonHeaders()
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.PhishingToken, _phishingToken);
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.EmbedError, "true");
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.SessionId, _sessionId);
            HttpClient.AddRequestHeader("X-Requested-With", "ShockwaveFlash/20.0.0.286");
            HttpClient.AddRequestHeader("Origin", "https://www.easports.com");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
            AddAcceptHeader("application/json");
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddReferrerHeader("https://www.easports.com/iframe/fut16/bundles/futweb/web/flash/FifaUltimateTeam.swf?cl=155438");
            AddUserAgent();
            HttpClient.AddConnectionKeepAliveHeader();
        }

        protected void AddPinHeaders()
        {
            HttpClient.AddConnectionKeepAliveHeader();
            HttpClient.AddRequestHeader("Origin", "https://www.easports.com");
            HttpClient.AddRequestHeader("x-ea-taxv", "1.1");
            HttpClient.AddRequestHeader("x-ea-game-type", "sku");
            AddUserAgent();
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            HttpClient.AddRequestHeader("X-Requested-With", "ShockwaveFlash/20.0.0.286");
            HttpClient.AddRequestHeader("x-ea-game-id", "fifa16");
            AddAcceptHeader("*/*");
            AddReferrerHeader("https://www.easports.com/iframe/fut16/bundles/futweb/web/flash/FifaUltimateTeam.swf?cl=155438");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
        }

        protected void AddPinHeadersMobile()
        {
            HttpClient.AddConnectionKeepAliveHeader();
            HttpClient.AddRequestHeader("Origin", "file://");
            HttpClient.AddRequestHeader("x-ea-taxv", "1");
            HttpClient.AddRequestHeader("CSP", "active");
            AddMobileUserAgent();
            HttpClient.AddRequestHeader(HttpHeaders.ContentType, "application/json");
            AddAcceptHeader("text/plain, */*; q=0.01");
            HttpClient.AddRequestHeader("x-ea-game-id-type", "sellid");
            HttpClient.AddRequestHeader("x-ea-game-id", "859051");
            AddAcceptEncodingHeader();
            AddAcceptLanguageHeader();
        }

        protected void AddUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.62 Safari/537.36");
        }

        protected void AddMobileUserAgent()
        {
            HttpClient.AddRequestHeader(HttpHeaders.UserAgent, "Mozilla/5.0 (Linux; Android 5.0; Elephone P7000 Build/LRX21M) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/37.0.0.0 Mobile Safari/537.36");
        }

        protected void AddGatewayHeaders(string token_type, string auth_string)
        {
            HttpClient.AddRequestHeader("Authorization", string.Format("{0} {1}", token_type, auth_string));
        }

        protected void AddXRequestedWith()
        {
            HttpClient.AddRequestHeader("X-Requested-With", "com.ea.fifaultimate_row");
        }

        protected void AddXWapProfile()
        {
            HttpClient.AddRequestHeader("x-wap-profile", "http://218.249.47.94/Xianghe/MTK_LTE_Phone_KK_UAprofile.xml");
        }

        protected void AddAcceptHeader(string value)
        {
            HttpClient.AddRequestHeader(HttpHeaders.Accept, value);
        }

        protected void AddReferrerHeader(string value)
        {
            HttpClient.SetReferrerUri(value);
        }

        protected void AddAcceptEncodingHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptEncoding, "gzip,deflate,sdch");
        }

        protected void AddAcceptLanguageHeader()
        {
            HttpClient.AddRequestHeader(HttpHeaders.AcceptLanguage, "en-US,en;q=0.8");
        }

        protected void AddMethodOverrideHeader(HttpMethod httpMethod)
        {
            HttpClient.AddRequestHeader(NonStandardHttpHeaders.MethodOverride, httpMethod.Method);
        }

        protected static async Task<T> Deserialize<T>(HttpResponseMessage message) where T : class
        {
            message.EnsureSuccessStatusCode();
            var messageContent = await message.Content.ReadAsStringAsync();
            T deserializedObject = null;

            try
            {
                deserializedObject = JsonConvert.DeserializeObject<T>(messageContent, JsonSerializerSettings);
            }
            catch (JsonSerializationException serializationException)
            {
                try
                {
                    var futError = JsonConvert.DeserializeObject<FutError>(messageContent, JsonSerializerSettings);
                    MapAndThrowException(serializationException, futError);
                }
                catch (JsonSerializationException)
                {
                    throw serializationException;
                }
            }

            return deserializedObject;
        }

        private static void MapAndThrowException(Exception exception, FutError futError)
        {
            // TODO: Should extract this to a separate class and keep them in a Dictionary<FutErrorCode, Func<FutError, Exception, FutErrorException>>

            switch (futError.Code)
            {
                case FutErrorCode.ExpiredSession:
                    throw new ExpiredSessionException(futError, exception);
                case FutErrorCode.NotFound:
                    throw new NotFoundException(futError, exception);
                case FutErrorCode.Conflict:
                    throw new ConflictException(futError, exception);
                case FutErrorCode.BadRequest:
                    throw new BadRequestException(futError, exception);
                case FutErrorCode.PermissionDenied:
                    throw new PermissionDeniedException(futError, exception);
                case FutErrorCode.NotEnoughCredit:
                    throw new NotEnoughCreditException(futError, exception);
                case FutErrorCode.NoSuchTradeExists:
                    throw new NoSuchTradeExistsException(futError, exception);
                case FutErrorCode.InternalServerError:
                    throw new InternalServerException(futError, exception);
                case FutErrorCode.ServiceUnavailable:
                    throw new ServiceUnavailableException(futError, exception);
                case FutErrorCode.InvalidDeck:
                    throw new InvalidDeckException(futError, exception);
                case FutErrorCode.DestinationFull:
                    throw new DestinationFullException(futError, exception);
                case FutErrorCode.CaptchaTriggered:
                    throw new CaptchaTriggeredException(futError, exception);
                default:
                    var newException = new FutErrorException(futError, exception);
                    throw new FutException(string.Format("Unknown EA error, please report on GitHub - {0}", newException.Message), newException);
            }
        }
    }
}
