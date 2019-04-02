using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKC
{
    /// <summary>
    /// Class for saving Timestamp and name of events from JSON file
    /// </summary>
    internal class EDEvent
    {
        //dont change name of properties, causes Errors with Newtonsoft JSON reading method
        /// <summary>
        /// Time when event happened in Elite
        /// </summary>
        public DateTime timestamp { get; set; }
        /// <summary>
        /// Type of event that happened
        /// </summary>
        public string @event { get; set; }

        /// <summary>
        /// Writes Event to string 
        /// </summary>
        /// <returns>Event in string</returns>
        public override string ToString()
        {
            //string.Format(" Timestamp {0} Event: {1} ", timestamp, @event);
            return $"Timestamp {timestamp} Event: {@event}";
        }


    }
}
