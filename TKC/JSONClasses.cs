using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TKC
{
    //TODO zjistit co je internal

    /// <summary>
    /// Class for saving Timestamp and name of events from JSON file
    /// </summary>
    internal class EDEvent
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
    internal class ThargoidKillEvent : EDEvent
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
    /// Singleton class that Reads a Elite dangerous log files and prints thargoid kills
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

        /// <summary>
        /// Method which resets all thargoid kills
        /// </summary>
        private void resetThargoidKills()
        {
            scout = 0;
            cyclops = 0;
            basillisk = 0;
            medusa = 0;
            hydra = 0;
            unknown = 0;
        }
        


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
        public string printAllKills()
        {
            return "Kills from all Journals:" + "\r\n" +"Scouts: " + scout + "\r\nCyclops: " + cyclops + "\r\nBasillisk: " + basillisk + "\r\nMedusa: " + 
                medusa + "\r\nHydra: " + hydra + "\r\nUnknown " + unknown;
        }

        /// <summary>
        /// Detects thargoid kill from EDEvent class
        /// </summary>
        /// <param name="e1"> converted JSON text to class </param>
        /// <param name="JSONStringLine"> JSON text </param>
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
                                unknown++;
                                ErrorLogging.LogUnknownThargoidType(JSONStringLine);
                                break;
                        }
                    }
                }
            }catch(Exception e)
            {
                MessageBox.Show("Error: Unknown in method DetectThargoidKill");
                ErrorLogging.LogError(e, JSONStringLine);
                throw;
            }
            
        }

        
        /// <summary>
        /// Method which reads JSON file 
        /// </summary>
        /// <param name="filePath"> - a path to a file</param>
        private void ReadJsonFile(string filePath)
        {
            //debug integer for current line of reading
            int line = 1;
            //debug string for current JSONStringLine
            string JSONStringLine = "";
            StreamReader fileReader = null;
            try
            {
                string path = filePath; //Path of a file
                EDEvent currentEvent;
                fileReader = new System.IO.StreamReader(path); //Streamreader which reads file line by line
                while ((JSONStringLine = fileReader.ReadLine()) != null)
                {
                    //JSON convertor which converts JSON to class variables
                    currentEvent = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                    line++;
                    DetectThargoidKill(currentEvent, JSONStringLine);
                }
            }
            catch (JsonReaderException e)
            {
                ErrorLogging.LogError(e, JSONStringLine);
            }
            catch (FileNotFoundException e)
            {

                MessageBox.Show("Error: File " + filePath + " not found");
                ErrorLogging.LogError(e, filePath);
            }
            catch (IOException e)
            {
                ErrorLogging.LogError(e);
                throw;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: Unknown error in ReadJsonFile");
                ErrorLogging.LogError(e,filePath);
                throw;
            }
            finally
            {
                fileReader.Close();
            }
        }

        /// <summary>
        /// Reads last Log in real time while game is running(reads until user closes Elite dangerous)
        /// </summary>
        public void ReadLastJsonFileInRealTime()
        {
            //integer for current line of reading
            int line = 1;
            //integer which indicates last line that have been read (reader wont read same lines every cycle)
            int endLine = 0;
            //debug string for current JSONStringLine
            string JSONStringLine = "";
            StreamReader fileReader = null;
            try
            {
                //last log which will be read in realtime
                FileInfo lastLog = sortedJournalsList[sortedJournalsList.Count - 1];
                //string for file path
                string path = lastLog.FullName;
                EDEvent currentEvent = null;
                fileReader = new StreamReader(path);
                //boolean which ends whole reading cycle ends if EDEvent.@event == "Shutdown" 
                bool endOfTheCycle = true;
                //boolean which states if file has been read for a first time

                while (endOfTheCycle == true)
                {
                    if ((JSONStringLine = fileReader.ReadLine()) != null)
                    {
                        try
                        {
                            currentEvent = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        }
                        catch (JsonReaderException e)
                        {
                            ErrorLogging.LogError(e, JSONStringLine);
                        }
                        //true if reader read line which he already read
                        if (line < endLine)
                        {
                            line++;
                        }
                        //true if reader read line which he didnt read before
                        else if (line >= endLine )
                        {
                            
                            try
                            {
                                line++;
                                DetectThargoidKill(currentEvent, JSONStringLine);
                            }catch(NullReferenceException)
                            {
                                //catches NullReference exception so that rest of the program can continue
                                //do nothing
                            }
                        }
                        //true if reader finds shutdown event at the end of file
                        if (currentEvent.@event.Equals("Shutdown") == true)
                        {
                            fileReader.Close();
                            endOfTheCycle = false;
                            break;
                        }
                        //true if reader reaches end of file
                        if (fileReader.EndOfStream == true)
                        {
                            //bool which controls if file have changed if true restarts the reading cycle above
                            bool fileChanged = false;
                            //value which saves last time file changed
                            DateTime currentLastTimeWritten = DateTime.Now;
                            endLine = line;
                            while (fileChanged == false)
                            {
                                lastLog = new FileInfo(path);
                                //true if file changed
                                if (lastLog.LastWriteTime > currentLastTimeWritten)
                                {
                                    currentLastTimeWritten = lastLog.LastWriteTime;
                                    fileReader = new StreamReader(path);
                                    fileChanged = true;
                                    line = 1;
                                }
                                //else sleeps thread and restarts fileChanged cycle 5 sec later
                                else
                                {
                                    lastLog = null;
                                    fileReader.Close();
                                    System.Threading.Thread.Sleep(5000);
                                }
                            }
                            //restarts the reading cycle
                            continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                ErrorLogging.LogError(e);
                throw;
            }finally
            {
                fileReader.Close();
            }
        }

        /// <summary>
        /// Reads all Journal files in selected directory
        /// </summary>
        public void ReadDirectory()
        {
            const int numberOfRetries = 20;
            const int delay = 3000;
            try
            {
                resetThargoidKills();
                List<FileInfo> journalsList = JSONReaderInstance.GetJournalsInDirectory();
                string path = "";
                //reads all Journal files except last one (the one which Elite Dangerous writes in real-time)
                for (int i = 0; i < journalsList.Count - 1; i++)
                {
                    path = journalsList[i].FullName;
                    //Console.WriteLine("Reading: " + path);
                    for (int j = 0; j <= numberOfRetries; ++j)
                    {
                        try
                        {
                            JSONReaderInstance.ReadJsonFile(path);
                            break;
                        }
                        catch (IOException) when (j <= numberOfRetries)
                        {
                            System.Threading.Thread.Sleep(delay);
                        }
                    }
                }
                
                //Initiates garbage collection at the end
                GC.Collect();
            }
            catch (DirectoryNotFoundException e)
            {
                ErrorLogging.LogError(e);
                MessageBox.Show("Error: Journals directory not found");
            }
            
        }
        
        //debug string for current directory path
        string directoryPath;
        //List of sorted Journals
        List<FileInfo> sortedJournalsList = null;
        /// <summary>
        /// Method which find all Elite dangerous Journals in selected directory
        /// </summary>
        /// <returns>List of Journals </returns>
        private List<FileInfo> GetJournalsInDirectory()
        {
            try
            {
                //finds path to Users folder ("C:\Users\<user>)"
                directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    directoryPath = Directory.GetParent(directoryPath).ToString();
                }
                //regex that matches ED journals names
                Regex regex = new Regex(@"(Journal)\.(\d{12})\.(\d{2})\.log"); //matches with Journal.123456789109.01.log
                DirectoryInfo directory = new DirectoryInfo(directoryPath + @"\Saved Games\Frontier Developments\Elite Dangerous");
                //field of unsorted files from directory above
                FileInfo[] unsortedJournals = directory.GetFiles("*.log");
                //List for sorted ED Journal files from directory
                //List<FileInfo>
                sortedJournalsList = new List<FileInfo>();

                //matches filenames with regex and sorts ED Journal files from others
                int index = 0;
                while (index < unsortedJournals.Length)
                {
                    if (regex.IsMatch(unsortedJournals[index].Name))
                    {
                        sortedJournalsList.Add(unsortedJournals[index]);
                    }
                    index++;
                }
                //Orders list by Last time it was written into (descending)
                sortedJournalsList = sortedJournalsList.OrderBy(x => x.LastWriteTime).ToList();
                
                return sortedJournalsList;
            }
            catch (DirectoryNotFoundException e)
            {
                ErrorLogging.LogError(e, directoryPath);
                return SelectDirectory();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error:  " + e.Message);
                ErrorLogging.LogError(e);
                throw;
            }

        }
       
        /// <summary>
        /// Overloaded method GetJournals which gets all Journals from input
        /// </summary>
        /// <param name="directoryPathInput"> - directory path </param>
        /// <returns>List of Journals</returns>
        private List<FileInfo> GetJournalsInDirectory(string directoryPathInput)
        {
            try
            {
                //finds path to Users folder ("C:\Users\<user>)"
                directoryPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    directoryPath = Directory.GetParent(directoryPath).ToString();
                }
                //regex that matches ED journals names
                Regex regex = new Regex(@"(Journal)\.(\d{12})\.(\d{2})\.log"); //matches with Journal.123456789109.01.log
                DirectoryInfo directory = new DirectoryInfo(directoryPathInput);
                //field of unsorted files from directory above
                FileInfo[] unsortedFiles = directory.GetFiles("*.log");
                //List for sorted ED log files from directory
                List<FileInfo> sortedFilesList = new List<FileInfo>();

                //matches filenames with regex and sorts ED log files from others
                int index = 0;
                while (index < unsortedFiles.Length)
                {
                    if (regex.IsMatch(unsortedFiles[index].Name))
                    {
                        sortedFilesList.Add(unsortedFiles[index]);
                        Console.WriteLine(unsortedFiles[index].Name);
                    }
                    index++;
                }
                //Orders list by Last time it was written into (descending)
                sortedFilesList = sortedFilesList.OrderBy(x => x.LastWriteTime).ToList();
                return sortedFilesList;
            }
            catch (DirectoryNotFoundException e)
            {
                ErrorLogging.LogError(e, directoryPath);
                throw;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error:  " + e.Message);
                ErrorLogging.LogError(e);
                throw;
            }

        }

        /// <summary>
        /// Shows folder browser to user which selects ED journals directory
        /// </summary>
        /// <returns>List of Journals</returns>
        private List<FileInfo> SelectDirectory()
        { 
            //If app cant find default ED log directory. Alerts user to select directory
            MessageBox.Show("Error: Cant find directory. Please select directory where ED journals are located.");
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                return GetJournalsInDirectory(folderBrowserDialog1.SelectedPath);
            }
            else
            {
                return GetJournalsInDirectory("");
            }
        }
    }
}
