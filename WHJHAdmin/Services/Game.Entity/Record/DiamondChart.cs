using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entity.Record
{
    public class DiamondChart
    {
        public string time { get; set; }
        public long diamond { get; set; }
        public string type { get; set; }
    }

    public class StatisticsChart
    {
        public string time { get; set; }
        public long count { get; set; }
        public string type { get; set; }
    }

    public class StatisticsRevenue
    {
        public string TimeDate { get; set; }
        public long Revenue { get; set; }
    }

    public class StatisticsWaste
    {
        public string TimeDate { get; set; }
        public int KindId { get; set; }
        public int ServerId { get; set; }
        public long Waste { get; set; }
    }

    public class StatisticsWealth
    {
        public string name { get; set; }
        public long value { get; set; }
    }

    public class TotalDiamondExch
    {
        public long ExchDiamond { get; set; }
        public long PresentGold { get; set; }
    }

    public class StatisticsOnline
    {
        public DateTime DTime { get; set; }
        public int RUser { get; set; }
        public int AUser { get; set; }
    }
}
