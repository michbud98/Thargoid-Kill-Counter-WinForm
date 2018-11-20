using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TKC
{

  
    /// <summary>
    /// Class for saving Timestamp and events from JSON file
    /// </summary>
    public class EDEvent
    {

        public DateTime timestamp { get; set; }
        public string @event { get; set; }

        public override string ToString()
        {
            return string.Format(" Timestamp {0} Event: {1} ", timestamp, @event);
        }


    }

    /// <summary>
    /// Inherited Class from EDEvent which saves data about thargoid kills
    /// </summary>
    public class ThargoidKillEvent : EDEvent
    {
        /// <summary>
        /// Reward -- in Credits, tels us which thargoid we killed 10 k Scout, 2 mil Cyclops, 6 mil Basillisk, 10 mil Medusa, 15 mil Hydra
        /// </summary>
        public int reward { get; set; }
        public string awardingFaction_Localised { get; set; }
        public string victimFaction_Localised { get; set; }

        public override string ToString()
        {
            return string.Format(" Timestamp: {0} Event: {1} reward: {2} awardingFaction: {3} victimFaction: {4}",
                timestamp, @event, reward, awardingFaction_Localised, victimFaction_Localised);
        }
    }

    

    /// <summary>
    /// Singleton class that Reads a JSON file and returns string
    /// </summary>
    public class JSONReaderSingleton
    {
        /// <summary>
        /// Thargoid types kill counts
        /// </summary>
        int scout { get; set; }
        int cyclops { get; set; }
        int basillisk { get; set; }
        int medusa { get; set; }
        int hydra { get; set; }
        int unknown { get; set; }

        int index = 0;
        //Dictionary<int, EDEvent> eventsDictionary = new Dictionary<int, EDEvent>();
        Dictionary<int, EDEvent> killsDictionary = new Dictionary<int, EDEvent>();

        //Storage variable for singleton
        private static JSONReaderSingleton JSONReaderInstance;
        /// <summary>
        /// JSONReaderSingleton constructor
        /// </summary>
        /// <returns>Single class of JSONReaders</returns>
        public static JSONReaderSingleton getInstance()
        {
            
            if (JSONReaderInstance == null)
            {
                JSONReaderInstance = new JSONReaderSingleton();
            }
            
            return JSONReaderInstance;
        }

        /// <summary>
        /// Detects thargoid kill from EDEvent class with its variablie @event
        /// </summary>
        /// <param name="e1"> converted JSON text </param>
        private void DetectThargoidKill(ThargoidKillEvent kill)
        {
            if (kill.awardingFaction_Localised.Equals("Pilots Federation") && kill.victimFaction_Localised.Equals("Thargoids"))
            {
                int caseSwitch = kill.reward;
                switch (caseSwitch) {
                    case 10000:
                        scout++;
                        break;
                    case 2000000:
                        cyclops++;
                        break;
                    case 6000000:
                        basillisk++;
                        break;
                    case 10000000:
                        medusa++;
                        break;
                    case 15000000:
                        hydra++;
                        break;
                    default:
                        MessageBox.Show("Error: Thargoid kill cant resolve thargoid type. Logging to unknown type for later resolution.");
                        //TODO dodělat zapsání chyby do logu
                        break;
                }
            }
        }
        bool firstRead = false;
        /// <summary>
        /// Method which reads JSON file 
        /// </summary>
        /// <param name="filePath"> - a path to a file</param>
        public void ReadJsonFile(String filePath)
        {
            try
            {

                String path = filePath; //Path of a file
                String JSONStringLine = ""; //Variable for JSON text line

                if(firstRead == false)
                {
                    EDEvent e1;
                    ThargoidKillEvent t1;
                    System.IO.StreamReader file = new System.IO.StreamReader(path); //Streamreader which reads file line by line
                    while ((JSONStringLine = file.ReadLine()) != null)
                    {
                        //JSON convertor which converts JSON to class variables
                        e1 = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        if (e1.@event.Equals("FactionKillBond"))
                        {
                            t1 = JsonConvert.DeserializeObject<ThargoidKillEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            killsDictionary[index] = t1;
                            index++;
                        }


                    }

                    firstRead = true;
                }
                
                else
                {
                    string vysledek = "";
                    for(int i = 0; i < index; i++)
                    {
                        vysledek += killsDictionary[i] + "\r\n";
                        
                    }
                    MessageBox.Show(vysledek);
                }
                

            }
            catch (FileNotFoundException)
            {

                MessageBox.Show("Error: File not found");
            }
        }



    }
}
