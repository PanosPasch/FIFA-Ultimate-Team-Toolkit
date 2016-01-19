using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class WatchlistResponse
    {
        public List<AuctionInfo> AuctionInfo { get; set; }

        public uint Credits { get; set; }

        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public ushort Total { get; set; }

        public uint? Code { get; set; }

        public string Debug { get; set; }

        public string Reason { get; set; }

        public string String { get; set; }
    }
}
