using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    //type: training
    //method: GKTraining

    public class GKTrainingCard : SearchParameterBase<uint>
    {
        public const uint GKDivingBronze = 5003001;
        public const uint GKDivingSilver = 5003002;
        public const uint GKDivingGold = 5003003;
        public const uint GKHandlingBronze = 5003004;
        public const uint GKHandlingSilver = 5003005;
        public const uint GKHandlingGold = 5003006;
        public const uint GKKickingBronze = 5003007;
        public const uint GKKickingSilver = 5003008;
        public const uint GKKickingGold = 5003009;
        public const uint GKSpeedBronze = 5003010;
        public const uint GKSpeedSilver = 5003011;
        public const uint GKSpeedGold = 5003012;
        public const uint GKPositionBronze = 5003013;
        public const uint GKPositionSilver = 5003014;
        public const uint GKPositionGold = 5003015;
        public const uint GKReflexesBronze = 5003016;
        public const uint GKReflexesSilver = 5003017;
        public const uint GKReflexesGold = 5003018;
        public const uint GKAllBronze = 5003019;
        public const uint GKAllSilver = 5003020;
        public const uint GKAllGold = 5003021;
        
        private GKTrainingCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<GKTrainingCard> GetAll()
        {
            yield return new GKTrainingCard("GK Diving Bronze +5", GKDivingBronze);
            yield return new GKTrainingCard("GK Diving Silver +10", GKDivingSilver);
            yield return new GKTrainingCard("GK Diving Gold +15", GKDivingGold);
            yield return new GKTrainingCard("GK Handling Bronze +5", GKHandlingBronze);
            yield return new GKTrainingCard("GK Handling Silver +10", GKHandlingSilver);
            yield return new GKTrainingCard("GK Handling Gold +15", GKHandlingGold);
            yield return new GKTrainingCard("GK Kicking Bronze +5", GKKickingBronze);
            yield return new GKTrainingCard("GK Kicking Silver +10", GKKickingSilver);
            yield return new GKTrainingCard("GK Kicking Gold +15", GKKickingGold);
            yield return new GKTrainingCard("GK Speed Bronze +5", GKSpeedBronze);
            yield return new GKTrainingCard("GK Speed Silver +10", GKSpeedSilver);
            yield return new GKTrainingCard("GK Speed Gold +15", GKSpeedGold);
            yield return new GKTrainingCard("GK Position Bronze +5", GKPositionBronze);
            yield return new GKTrainingCard("GK Position Silver +10", GKPositionSilver);
            yield return new GKTrainingCard("GK Position Gold +15", GKPositionGold);
            yield return new GKTrainingCard("GK Reflexes Bronze +5", GKReflexesBronze);
            yield return new GKTrainingCard("GK Reflexes Silver +10", GKReflexesSilver);
            yield return new GKTrainingCard("GK Reflexes Gold +15", GKReflexesGold);
            yield return new GKTrainingCard("GK All Bronze +3", GKAllBronze);
            yield return new GKTrainingCard("GK All Silver +6", GKAllSilver);
            yield return new GKTrainingCard("GK All Gold +10", GKAllGold);
        }
    }
}
