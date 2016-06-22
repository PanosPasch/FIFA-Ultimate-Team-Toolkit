using System.Collections.Generic;

namespace UltimateTeam.Toolkit.Parameters
{
    //type: development
    //method: contract
    public class ContractCard : SearchParameterBase<uint>
    {
        public const uint ShinyBronzePlayerContract = 1615613740 - 1610612736;
        public const uint ShinySilverPlayerContract = 1615613741 - 1610612736;
        public const uint ShinyGoldPlayerContract = 1615613742 - 1610612736;
        public const uint NonShinyBronzePlayerContract = 1615613737 - 1610612736;
        public const uint NonShinySilverPlayerContract = 1615613738 - 1610612736;
        public const uint NonShinyGoldPlayerContract = 1615613739 - 1610612736;
        public const uint ShinyBronzeManagerContract = 1615613746 - 1610612736;
        public const uint ShinySilverManagerContract = 1615613747 - 1610612736;
        public const uint ShinyGoldManagerContract = 1615613748 - 1610612736;
        public const uint NonShinyBronzeManagerContract = 1615613743 - 1610612736;
        public const uint NonShinySilverManagerContract = 1615613744 - 1610612736;
        public const uint NonShinyGoldManagerContract = 1615613745 - 1610612736;
   

        private ContractCard(string description, uint value)
        {
            Description = description;
            Value = value;
        }

        public static IEnumerable<ContractCard> GetAll()
        {
            yield return new ContractCard("Shiny Bronze Player Contract", ShinyBronzePlayerContract);
            yield return new ContractCard("Shiny Silver Player Contract", ShinySilverPlayerContract);
            yield return new ContractCard("Shiny Gold Player Contract", ShinyGoldPlayerContract);
            yield return new ContractCard("Non-Shiny Bronze Player Contract", NonShinyBronzePlayerContract);
            yield return new ContractCard("Non-Shiny Silver Player Contract", NonShinySilverPlayerContract);
            yield return new ContractCard("Non-Shiny Gold Player Contract", NonShinyGoldPlayerContract);
            yield return new ContractCard("Shiny Bronze Manager Contract", ShinyBronzeManagerContract);
            yield return new ContractCard("Shiny Silver Manager Contract", ShinySilverManagerContract);
            yield return new ContractCard("Shiny Gold Manager Contract", ShinyGoldManagerContract);
            yield return new ContractCard("Non-Shiny Bronze Manager Contract", NonShinyBronzeManagerContract);
            yield return new ContractCard("Non-Shiny Silver Manager Contract", NonShinySilverManagerContract);
            yield return new ContractCard("Non-Shiny Gold Manager Contract", NonShinyGoldManagerContract);
            
        }
    }
}
