using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    public class Position : SearchParameterBase<string>
    {
        public const string Defenders = "defense";

        public const string Midfielders = "midfield";

        public const string Attackers = "attacker";

        public const string GoalKeeper = "GK";

        public const string RightWingBack = "RWB";

        public const string RightBack = "RB";

        public const string CenterBack = "CB";

        public const string LeftBack = "LB";

        public const string LeftWingBack = "LWB";

        public const string CentralDefensiveMidfielder = "CDM";

        public const string RightMidfielder = "RM";

        public const string CentralMidfielder = "CM";

        public const string LeftMidfielder = "LM";

        public const string CentralAttackingMidfielder = "CAM";

        public const string RightForward = "RF";

        public const string CentralForward = "CF";

        public const string LeftForward = "LF";

        public const string RightWinger = "RW";

        public const string Striker = "ST";

        public const string LeftWinger = "LW";

        private Position(string descripton, string value)
        {
            Description = descripton;
            Value = value;
        }

        public static IEnumerable<Position> GetAll()
        {
            yield return new Position("Defenders", Defenders);
            yield return new Position("Midfielders", Midfielders);
            yield return new Position("Attackers", Attackers);
            yield return new Position("GK", GoalKeeper);
            yield return new Position("RWB", RightWingBack);
            yield return new Position("RB", RightBack);
            yield return new Position("CB", CenterBack);
            yield return new Position("LB", LeftBack);
            yield return new Position("LWB", LeftWingBack);
            yield return new Position("CDM", CentralDefensiveMidfielder);
            yield return new Position("RM", RightMidfielder);
            yield return new Position("CM", CentralMidfielder);
            yield return new Position("LM", LeftMidfielder);
            yield return new Position("CAM", CentralAttackingMidfielder);
            yield return new Position("RF", RightForward);
            yield return new Position("CF", CentralForward);
            yield return new Position("LF", LeftForward);
            yield return new Position("RW", RightWinger);
            yield return new Position("ST", Striker);
            yield return new Position("LW", LeftWinger);
        }
    }
}