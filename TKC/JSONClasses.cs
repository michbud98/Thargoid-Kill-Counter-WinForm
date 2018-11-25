using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
        /// <param name="scout"´> - variable for number of scout kills</param>
        /// <param name="cyclops"´> - variable for number of cyclops kills</param>
        /// <param name="basillisk"´> - variable for number of basillisk kills</param>
        /// <param name="medusa"´> - variable for number of medusa kills</param>
        /// <param name="hydra"´> - variable for number of hydra kills</param>
        /// <param name="unknown"´> - variable which stores kills of unknown thargoid types</param>
        /// </summary>
        int scout { get; set; }
        int cyclops { get; set; }
        int basillisk { get; set; }
        int medusa { get; set; }
        int hydra { get; set; }
        int unknown { get; set; }

        


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
        /// Prints thargoid kills to text
        /// </summary>
        /// <returns>String of thargoid kills</returns>
        public string printKills()
        {
            return " Scouts: " + scout + "\r\n Cyclops: " + cyclops + "\r\n Basillisk: " + basillisk + "\r\n Medusa: " + 
                medusa + "\r\n hydra: " + hydra + "\r\n unknown " + unknown;
        }
        
        /// <summary>
        /// Detects thargoid kill from EDEvent class with its variablie @event
        /// </summary>
        /// <param name="e1"> converted JSON text </param>
        private void DetectThargoidKill(EDEvent e1, string JSONStringLine)
        {            
            try
            {
                ThargoidKillEvent kill;
                if (e1.@event.Equals("FactionKillBond"))
                {

                    kill = JsonConvert.DeserializeObject<ThargoidKillEvent>(JSONStringLine,
                        new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                    if (kill.awardingFaction_Localised != null && kill.victimFaction_Localised != null &&
                        kill.awardingFaction_Localised.Equals("Pilots Federation") && kill.victimFaction_Localised.Equals("Thargoids"))

                    {
                        int caseSwitch = kill.reward;
                        switch (caseSwitch)
                        {
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

                                //TODO //error logging
                                break;
                        }
                    }
                }
            }catch(Exception)
            {
                MessageBox.Show("Error: Unknown in method DetectThargoidKill");
            }
            
        }

        /// <summary>
        /// <param name="line"> - debug integer for current line number</param>>
        /// </summary>
        int line = 1;

        /// <summary>
        /// Method which reads JSON file 
        /// </summary>
        /// <param name="filePath"> - a path to a file</param>
        private void ReadJsonFile(String filePath)
        {
            try
            {
                String path = filePath; //Path of a file
                String JSONStringLine = ""; //Variable for JSON text line
                EDEvent e1;
                System.IO.StreamReader file = new System.IO.StreamReader(path); //Streamreader which reads file line by line
                while ((JSONStringLine = file.ReadLine()) != null)
                {
                    //JSON convertor which converts JSON to class variables
                    e1 = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    line++;
                    DetectThargoidKill(e1, JSONStringLine);
                }
            }
            catch (JsonReaderException)
            {
                //error logging
            }
            catch (FileNotFoundException)
            {

                MessageBox.Show("Error: File " + filePath + " not found");
                //error logging
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Unknown error in ReadJsonFile");
                //error logging
            }
        }
        /// <summary>
        /// Reads all log files in selected directory
        /// </summary>
        public void readDirectory()
        {
            List<FileInfo> filesList = JSONReaderInstance.GetJournals();
            string path = "";
            for (int i = 0; i < filesList.Count; i++)
            {
                path = filesList[i].FullName;
                Console.WriteLine("Reading: " + path);
                JSONReaderInstance.ReadJsonFile(path);
            }
        }

        String directoryPath;
        /// <summary>
        /// Method which find all Elite dangerous Journals in selected directory
        /// </summary>
        /// <returns>List of Journals </returns>
        private List<FileInfo> GetJournals()
        { 
           try
           {
                //finds path to Users folder ("C:\Users\<user>)"
                directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    directoryPath = Directory.GetParent(directoryPath).ToString();
                }
                Regex regex = new Regex(@"(Journal)\.(\d{12})\.(\d{2})\.log"); //matches with Journal.123456789109.01.log
                DirectoryInfo directory = new DirectoryInfo(directoryPath + @"\Saved Games\Frontier Developments\Elite Dangerous");
            FileInfo[] files = directory.GetFiles("*.log");
            List<FileInfo> sortedFilesList = new List<FileInfo>();
            int index = 0;

            while(index < files.Length)
            {
                if(regex.IsMatch(files[index].Name))
                {
                    sortedFilesList.Add(files[index]);
                }
                index++;
            }

                
            return sortedFilesList;
            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("Error: Directory " + directoryPath + " not found.");
                throw new Exception();
                
            }
            catch(Exception)
            {
                MessageBox.Show("Error: Unknown in method getJournals");
                throw new Exception();
            }

        }
    }
}
