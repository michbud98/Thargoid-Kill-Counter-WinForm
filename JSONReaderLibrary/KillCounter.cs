namespace TKC
{
    /// <summary>
    /// Handles kill counting
    /// </summary>
    public class KillCounter
    {
        internal int Scout { get; set; }
        internal int Cyclops { get; set; }
        internal int Basillisk { get; set; }
        internal int Medusa { get; set; }
        internal int Hydra { get; set; }
        internal int Unknown { get; set; }
        internal int AllTypesKills { get; set; }
        public int LastAllTypesKills { get; set; }

        /// <summary>
        /// Method which resets all thargoid kills
        /// </summary>
        internal void ResetThargoidKills()
        {
            Scout = 0;
            Cyclops = 0;
            Basillisk = 0;
            Medusa = 0;
            Hydra = 0;
            Unknown = 0;
            AllTypesKills = 0;
            LastAllTypesKills = 0;
        }

        /// <summary>
        /// Prints thargoid kills to text
        /// </summary>
        /// <returns>String of thargoid kills</returns>
        public string PrintAllKills()
        {
            /*"Thargoid Combat Kills\r\n" + "------------------------" + "\r\nScouts: " + scout + "\r\nCyclops: " + cyclops + "\r\nBasillisk: " + basillisk + "\r\nMedusa: " +
              medusa + "\r\nHydra: " + hydra + "\r\nUnknown: " + unknown + "\r\n------------------------" + "\r\nTotal: " + allTypesKills;*/
            return $"Thargoid Combat Kills\r\n------------------------\r\nScouts: {Scout}\r\nCyclops: {Cyclops}\r\nBasillisk: {Basillisk} " +
                $"\r\nMedusa: {Medusa}\r\nHydra: {Hydra}\r\nUnknown: {Unknown}\r\n------------------------\r\nTotal: {AllTypesKills}";
        }

        /// <summary>
        /// Checks if kill count changed
        /// </summary>
        /// <returns>bool true or false</returns>
        public bool CheckKillChange()
        {

            if (AllTypesKills > LastAllTypesKills)
            {
                LastAllTypesKills = AllTypesKills;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckIfKillsZero()
        {

            if (AllTypesKills == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
