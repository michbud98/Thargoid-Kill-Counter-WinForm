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
        internal int Interceptors { get; set; }
        internal int Unknown { get; set; }
        internal int AllTypesKills { get; set; }

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
            Interceptors = 0;
            AllTypesKills = 0;
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
                $"\r\nMedusa: {Medusa}\r\nHydra: {Hydra}\r\n------------------------\r\nInterceptors: {CountTotalInterceptorKills()}\r\nUnknown: {Unknown}\r\n------------------------\r\nTotal: {CountTotalKills()}";
        }


        public int LastAllTypesKills { get; set; }
        /// <summary>
        /// Checks if kill count changed
        /// </summary>
        /// <returns>bool true or false</returns>
        public bool CheckKillChange()
        {
            AllTypesKills = CountTotalKills();
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
        /// <summary>
        /// Checks if kills are 0
        /// </summary>
        /// <returns>true on 0, false on erything higher then 0</returns>
        public bool CheckIfKillsZero()
        {
            AllTypesKills = CountTotalKills();
            if (AllTypesKills == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Counts total number of all types killed
        /// </summary>
        /// <returns>Total whole number of all kills</returns>
        private int CountTotalKills()
        {
            return Scout + Cyclops + Basillisk + Medusa + Hydra + Unknown;
        }

        /// <summary>
        /// Counts total number of Interceptor kills
        /// </summary>
        /// <returns>Total whole number of interceptor kills</returns>
        private int CountTotalInterceptorKills()
        {
            return Cyclops + Basillisk + Medusa + Hydra;
        }



    }
}
