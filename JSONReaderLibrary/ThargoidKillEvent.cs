using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKC
{
    /// <summary>
    /// Inherited Class from EDEvent which saves data about thargoid kills
    /// </summary>
    internal class ThargoidKillEvent : EDEvent
    {
        //dont change name of properties, causes Errors with Newtonsoft JSON reading method
        /// <summary>
        /// Reward -- in Credits, tels us which thargoid we killed 10 k Scout, 2 mil Cyclops, 6 mil Basillisk, 10 mil Medusa, 15 mil Hydra
        /// </summary>
        public int reward { get; set; }
        /// <summary>
        /// Faction that gave us Credits (Pilots Federation)
        /// </summary>
        public string awardingFaction_Localised { get; set; }
        /// <summary>
        /// Faction that we killed (Thargoids)
        /// </summary>
        public string victimFaction_Localised { get; set; }

        /// <summary>
        /// Writes Event to string 
        /// </summary>
        /// <returns>Event in string</returns>
        public override string ToString()
        {
            //(" Timestamp: {0} Event: {1} reward: {2} awardingFaction: {3} victimFaction: {4}", timestamp, @event, reward, awardingFaction_Localised, victimFaction_Localised);
            return $"Timestamp: {timestamp} Event: {@event} reward: {reward} awardingFaction: {awardingFaction_Localised} victimFaction: {victimFaction_Localised}";
        }
    }
}
