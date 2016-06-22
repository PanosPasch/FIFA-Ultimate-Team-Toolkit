using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{

    //type: development
    //method: healing

    public class HealingCard : SearchParameterBase<uint>
    {
        public const uint HealthHeadBronze = 5002007;
        public const uint HealthHeadSilver = 5002008;
        public const uint HealthHeadGold = 5002009;
        public const uint HealthShoulderBronze = 5002010;
        public const uint HealthShoulderSilver = 5002011;
        public const uint HealthShoulderGold = 5002012;
        public const uint HealthArmBronze = 5002013;
        public const uint HealthArmSilver = 5002014;
        public const uint HealthArmGold = 5002015;
        public const uint HealthHipBronze = 5002019;
        public const uint HealthHipSilver = 5002020;
        public const uint HealthHipGold = 5002021;
        public const uint HealthLegBronze = 5002022;
        public const uint HealthLegSilver = 5002023;
        public const uint HealthLegGold = 5002024;
        public const uint HealthFootBronze = 5002025;
        public const uint HealthFootSilver = 5002026;
        public const uint HealthFootGold = 5002027;
        public const uint HealthAllBronze = 5002028;
        public const uint HealthAllSilver = 5002029;
        public const uint HealthAllGold = 5002030;

        private HealingCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<HealingCard> GetAll()
        {
            yield return new HealingCard("Health Head Bronze +1", HealthHeadBronze);
            yield return new HealingCard("Health Head Silver +2", HealthHeadSilver);
            yield return new HealingCard("Health Head Gold +5", HealthHeadGold);
            yield return new HealingCard("Health Shoulder Bronze +1", HealthShoulderBronze);
            yield return new HealingCard("Health Shoulder Silver +2", HealthShoulderSilver);
            yield return new HealingCard("Health Shoulder Gold +5", HealthShoulderGold);
            yield return new HealingCard("Health Arm Bronze +1", HealthArmBronze);
            yield return new HealingCard("Health Arm Silver +2", HealthArmSilver);
            yield return new HealingCard("Health Arm Gold +5", HealthArmGold);
            yield return new HealingCard("Health Hip Bronze +1", HealthHipBronze);
            yield return new HealingCard("Health Hip Silver +2", HealthHipSilver);
            yield return new HealingCard("Health Hip Gold +5", HealthHipGold);
            yield return new HealingCard("Health Leg Bronze +1", HealthLegBronze);
            yield return new HealingCard("Health Leg Silver +2", HealthLegSilver);
            yield return new HealingCard("Health Leg Gold +5", HealthLegGold);
            yield return new HealingCard("Health Foot Bronze +1", HealthFootBronze);
            yield return new HealingCard("Health Foot Silver +2", HealthFootSilver);
            yield return new HealingCard("Health Foot Gold +5", HealthFootGold);
            yield return new HealingCard("Health All Bronze +1", HealthAllBronze);
            yield return new HealingCard("Health All Silver +2", HealthAllSilver);
            yield return new HealingCard("Health All Gold +4", HealthAllGold);
        }
    }
}
