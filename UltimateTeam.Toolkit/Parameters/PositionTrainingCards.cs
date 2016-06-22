using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    //type: training
    //method: playerTraining

    public class PositionTrainingCard : SearchParameterBase<uint>
    {
        //-2147483648
        public const uint LWB_LB = 5003059;
        public const uint LB_LWB = 5003060;
        public const uint RWB_RB = 5003061;
        public const uint RB_RWB = 5003062;
        public const uint LM_LW = 5003063;
        public const uint RM_RW = 5003064;
        public const uint LW_LM = 5003065;
        public const uint RW_RM = 5003066;
        public const uint LW_LF = 5003067;
        public const uint RW_RF = 5003068;
        public const uint LF_LW = 5003069;
        public const uint RF_RW = 5003070;
        public const uint CM_CAM = 5003071;
        public const uint CAM_CM = 5003072;
        public const uint CDM_CM = 5003073;
        public const uint CM_CDM = 5003074;
        public const uint CAM_CF = 5003075;
        public const uint CF_CAM = 5003076;
        public const uint CF_ST = 5003077;
        public const uint ST_CF = 5003078;
        
        private PositionTrainingCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<PositionTrainingCard> GetAll()
        {
            yield return new PositionTrainingCard("LWB >> LB", LWB_LB);
            yield return new PositionTrainingCard("LB >> LWB", LB_LWB);
            yield return new PositionTrainingCard("RWB >> RB", RWB_RB);
            yield return new PositionTrainingCard("RB >> RWB", RB_RWB);
            yield return new PositionTrainingCard("LM >> LW", LM_LW);
            yield return new PositionTrainingCard("RM >> RW", RM_RW);
            yield return new PositionTrainingCard("LW >> LM", LW_LM);
            yield return new PositionTrainingCard("RW >> RM", RW_RM);
            yield return new PositionTrainingCard("LW >> LF", LW_LF);
            yield return new PositionTrainingCard("RW >> RF", RW_RF);
            yield return new PositionTrainingCard("LF >> LW", LF_LW);
            yield return new PositionTrainingCard("RF >> RW", RF_RW);
            yield return new PositionTrainingCard("CM >> CAM", CM_CAM);
            yield return new PositionTrainingCard("CAM >> CM", CAM_CM);
            yield return new PositionTrainingCard("CDM >> CM", CDM_CM);
            yield return new PositionTrainingCard("CM >> CDM", CM_CDM);
            yield return new PositionTrainingCard("CAM >> CF", CAM_CF);
            yield return new PositionTrainingCard("CF >> CAM", CF_CAM);
            yield return new PositionTrainingCard("CF >> ST", CF_ST);
            yield return new PositionTrainingCard("ST >> CF", ST_CF);

        }
    }
}
