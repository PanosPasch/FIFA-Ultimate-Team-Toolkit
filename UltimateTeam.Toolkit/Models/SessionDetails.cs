using UltimateTeam.Toolkit.Extensions;

namespace UltimateTeam.Toolkit.Models
{
    public class SessionDetails
    {
        public string SessionID { get; private set; }
        
        public string PhishingToken { get; private set; }

        public Platform Platform { get; set; }

        public SessionDetails(string sessionid, string phishingtoken, Platform platform)
        {
            sessionid.ThrowIfInvalidArgument();
            phishingtoken.ThrowIfInvalidArgument();
            SessionID = sessionid;
            PhishingToken = phishingtoken;
            Platform = platform;
        }
    }
}