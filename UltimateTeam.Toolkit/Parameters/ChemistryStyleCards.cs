using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    //type: training
    //method: playStyle

    public class ChemistryStyleCard : SearchParameterBase<uint>
    {
        //-2147483648
        public const uint BASIC = 5003095;
        public const uint SNIPER = 5003096;
        public const uint FINISHER = 5003097;
        public const uint DEADEYE = 5003098;
        public const uint MARKSMAN = 5003099;
        public const uint HAWK = 5003100;
        public const uint ARTIST = 5003101;
        public const uint ARCHITECT = 5003102;
        public const uint POWERHOUSE = 5003103;
        public const uint MAESTRO = 5003104;
        public const uint ENGINE = 5003105;
        public const uint SENTINEL = 5003106;
        public const uint GUARDIAN = 5003107;
        public const uint GLADIATOR = 5003108;
        public const uint BACKBONE = 5003109;
        public const uint ANCHOR = 5003110;
        public const uint HUNTER = 5003111;
        public const uint CATALYST = 5003112;
        public const uint SHADOW = 5003113;
        public const uint WALL = 5003114;
        public const uint SHIELD = 5003115;
        public const uint CAT = 5003116;
        public const uint GLOVE = 5003117;
        public const uint GKBASIC = 5003118;

        private ChemistryStyleCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<ChemistryStyleCard> GetAll()
        {
            yield return new ChemistryStyleCard("BASIC", BASIC);
            yield return new ChemistryStyleCard("SNIPER", SNIPER);
            yield return new ChemistryStyleCard("FINISHER", FINISHER);
            yield return new ChemistryStyleCard("DEADEYE", DEADEYE);
            yield return new ChemistryStyleCard("MARKSMAN", MARKSMAN);
            yield return new ChemistryStyleCard("HAWK", HAWK);
            yield return new ChemistryStyleCard("ARTIST", ARTIST);
            yield return new ChemistryStyleCard("ARCHITECT", ARCHITECT);
            yield return new ChemistryStyleCard("POWERHOUSE", POWERHOUSE);
            yield return new ChemistryStyleCard("MAESTRO", MAESTRO);
            yield return new ChemistryStyleCard("ENGINE", ENGINE);
            yield return new ChemistryStyleCard("SENTINEL", SENTINEL);
            yield return new ChemistryStyleCard("GUARDIAN", GUARDIAN);
            yield return new ChemistryStyleCard("GLADIATOR", GLADIATOR);
            yield return new ChemistryStyleCard("BACKBONE", BACKBONE);
            yield return new ChemistryStyleCard("ANCHOR", ANCHOR);
            yield return new ChemistryStyleCard("HUNTER", HUNTER);
            yield return new ChemistryStyleCard("CATALYST", CATALYST);
            yield return new ChemistryStyleCard("SHADOW", SHADOW);
            yield return new ChemistryStyleCard("WALL", WALL);
            yield return new ChemistryStyleCard("SHIELD", SHIELD);
            yield return new ChemistryStyleCard("CAT", CAT);
            yield return new ChemistryStyleCard("GLOVE", GLOVE);
            yield return new ChemistryStyleCard("GK BASIC", GKBASIC);
        }
    }
}
