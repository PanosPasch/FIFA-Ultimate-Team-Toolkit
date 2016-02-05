using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeam.Toolkit.Constants;
using UltimateTeam.Toolkit.Models;

namespace UltimateTeam.Toolkit.Requests
{
    internal class PinEventRequest : FutRequestBase, IFutRequest<byte>
    {

        bool mobile = false;
        int s = 0;
        string fromid = "";
        string toid = "";
        string menu = "";

        public PinEventRequest(bool _mobile, int _s, string _fromid, string _toid, string _menu)
        {
            mobile = _mobile;
            s = _s;
            fromid = _fromid;
            toid = _toid;
            menu = _menu;
        }

        public async Task<byte> PerformRequestAsync()
        {
            AddMethodOverrideHeader(HttpMethod.Get);
            AddCommonHeaders();
            var tradePileResponseMessage = await HttpClient.PostAsync(string.Format("https://pin-river.data.ea.com/pinEvents"),
                new StringContent(
                    string.Format(@"{{""v"":""{12}"",""taxv"":1.1,""tidt"":""sku"",""tid"":""{0}"",""rel"":""prod"",""et"":""client"",""sid"":""{1}"",""plat"":""{2}"",""custom"":{{{11}}},""loc"":""en_GB"",""events"":[{{""core"":{{""pidm"":{{""nucleus"":""{3}""}},""s"":{4},""pid"":""{5}"",""en"":""page_view"",""pidt"":""persona"",""ts_event"":""{6}""}},""toid"":""{7}"",""fromid"":""{8}"",""type"":""{9}"",""custom"":{{}}}}],""ts_post"":""{10}""}}",
                    mobile ? "859051" : "FUT16WEB", SessionId, mobile ? "android" : "win", NucleusId, s.ToString(), PersonaId, GetTimeSpecial(), toid, fromid, menu, GetTimeSpecial(), mobile ? "\"networkAccess\":\"W\"" : "", mobile ? "16.2.0.155106" : "v16.0.155438")
                    )
                )
                .ConfigureAwait(false);

            return 0;
        }

        private static string GetTimeSpecial()
        {
            return System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }
    }
}