using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    //type: training
    //method: playerTraining

    public class PlayerTrainingCard : SearchParameterBase<uint>
    {
        public const uint PlayerPaceBronze = 5003022;
        public const uint PlayerPaceSilver = 5003023;
        public const uint PlayerPaceGold = 5003024;
        public const uint PlayerShootingBronze = 5003025;
        public const uint PlayerShootingSilver = 5003026;
        public const uint PlayerShootingGold = 5003027;
        public const uint PlayerPassingBronze = 5003028;
        public const uint PlayerPassingSilver = 5003029;
        public const uint PlayerPassingGold = 5003030;
        public const uint PlayerDribblingBronze = 5003031;
        public const uint PlayerDribblingSilver = 5003032;
        public const uint PlayerDribblingGold = 5003033;
        public const uint PlayerHeadingBronze = 5003034;
        public const uint PlayerHeadingSilver = 5003035;
        public const uint PlayerHeadingGold = 5003036;
        public const uint PlayerDefendBronze = 5003037;
        public const uint PlayerDefendSilver = 5003038;
        public const uint PlayerDefendGold = 5003039;
        public const uint PlayerAllBronze = 5003040;
        public const uint PlayerAllSilver = 5003041;
        public const uint PlayerAllGold = 5003042;
        
        private PlayerTrainingCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<PlayerTrainingCard> GetAll()
        {
            yield return new PlayerTrainingCard("Player Pace Bronze +5", PlayerPaceBronze);
            yield return new PlayerTrainingCard("Player Pace Silver +10", PlayerPaceSilver);
            yield return new PlayerTrainingCard("Player Pace Gold +15", PlayerPaceGold);
            yield return new PlayerTrainingCard("Player Shooting Bronze +5", PlayerShootingBronze);
            yield return new PlayerTrainingCard("Player Shooting Silver +10", PlayerShootingSilver);
            yield return new PlayerTrainingCard("Player Shooting Gold +15", PlayerShootingGold);
            yield return new PlayerTrainingCard("Player Passing Bronze +5", PlayerPassingBronze);
            yield return new PlayerTrainingCard("Player Passing Silver +10", PlayerPassingSilver);
            yield return new PlayerTrainingCard("Player Passing Gold +15", PlayerPassingGold);
            yield return new PlayerTrainingCard("Player Dribbling Bronze +5", PlayerDribblingBronze);
            yield return new PlayerTrainingCard("Player Dribbling Silver +10", PlayerDribblingSilver);
            yield return new PlayerTrainingCard("Player Dribbling Gold +15", PlayerDribblingGold);
            yield return new PlayerTrainingCard("Player Heading Bronze +5", PlayerHeadingBronze);
            yield return new PlayerTrainingCard("Player Heading Silver +10", PlayerHeadingSilver);
            yield return new PlayerTrainingCard("Player Heading Gold +15", PlayerHeadingGold);
            yield return new PlayerTrainingCard("Player Defend Bronze +5", PlayerDefendBronze);
            yield return new PlayerTrainingCard("Player Defend Silver +10", PlayerDefendSilver);
            yield return new PlayerTrainingCard("Player Defend Gold +15", PlayerDefendGold);
            yield return new PlayerTrainingCard("Player All Bronze +3", PlayerAllBronze);
            yield return new PlayerTrainingCard("Player All Silver +6", PlayerAllSilver);
            yield return new PlayerTrainingCard("Player All Gold +10", PlayerAllGold);
        }
    }
}
