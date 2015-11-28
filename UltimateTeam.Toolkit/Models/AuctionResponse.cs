using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class AuctionResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public BidTokens BidTokens { get; set; }

        public uint? Credits { get; set; }

        public List<Currency> Currencies { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public uint? Code { get; set; }

        public string Debug { get; set; }

        public string Reason { get; set; }

        public string String { get; set; }

        // TODO: I have no idea what type/structure this has, only seen null
        public string ErrorState { get; set; }
    }
}