using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Models
{
    public class PurchasedItemsResponse
    {
        public List<DuplicateItem> DuplicateItemIdList { get; set; }

        public List<ItemData> ItemData { get; set; }

        public uint? Code { get; set; }

        public string Debug { get; set; }

        public string Reason { get; set; }

        public string String { get; set; }
    }
}
