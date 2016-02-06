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
        string en = "";
        bool status = false;

        public PinEventRequest(bool _mobile, int _s, string _fromid, string _toid, string _menu, string _en, bool _status)
        {
            mobile = _mobile;
            s = _s;
            en = _en;
            fromid = _fromid;
            toid = _toid;
            menu = _menu;
            status = _status;
        }

        public async Task<byte> PerformRequestAsync()
        {
            string content = status ? string.Format(@"{{""v"":""{12}"",""taxv"":1.1,""tidt"":""sku"",""tid"":""{0}"",""rel"":""prod"",""et"":""client"",""sid"":""{1}"",""plat"":""{2}"",""custom"":{{{11}}},""loc"":""en_GB"",""events"":[{{""core"":{{""pidm"":{{""nucleus"":""{3}""}},""s"":{4},""pid"":""{5}"",""en"":""{13}"",""pidt"":""persona"",""ts_event"":""{6}""}},""{7}"":""{8}"",""type"":""{9}"",""custom"":{{}}}}],""ts_post"":""{10}""}}",
                mobile ? "859051" : "FUT16WEB", SessionId, mobile ? "android" : "win", NucleusId, s.ToString(), PersonaId, GetTimeSpecial(), toid, fromid, menu, GetTimeSpecial(), mobile ? "\"networkAccess\":\"W\"" : "", mobile ? "16.2.0.155106" : "v16.0.155438", en) : string.Format(@"{{""v"":""{12}"",""taxv"":1.1,""tidt"":""sku"",""tid"":""{0}"",""rel"":""prod"",""et"":""client"",""sid"":""{1}"",""plat"":""{2}"",""custom"":{{{11}}},""loc"":""en_GB"",""events"":[{{""core"":{{""pidm"":{{""nucleus"":""{3}""}},""s"":{4},""pid"":""{5}"",""en"":""{13}"",""pidt"":""persona"",""ts_event"":""{6}""}},""toid"":""{7}"",""fromid"":""{8}"",""type"":""{9}"",""custom"":{{}}}}],""ts_post"":""{10}""}}",
                    mobile ? "859051" : "FUT16WEB", SessionId, mobile ? "android" : "win", NucleusId, s.ToString(), PersonaId, GetTimeSpecial(), toid, fromid, menu, GetTimeSpecial(), mobile ? "\"networkAccess\":\"W\"" : "", mobile ? "16.2.0.155106" : "v16.0.155438", en);
            if (mobile) { AddPinHeadersMobile(); } else { AddPinHeaders(); }
            var tradePileResponseMessage = await HttpClient.PostAsync(string.Format("https://pin-river.data.ea.com/pinEvents"),
                new StringContent( content, System.Text.Encoding.UTF8, "application/json"
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