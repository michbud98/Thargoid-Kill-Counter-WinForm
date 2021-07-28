using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScreenShotLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TKC
{
    /// <summary>
    /// Singleton class that Reads a Elite dangerous log files and prints thargoid kills
    /// </summary>
    /// <exception cref="ArgumentException">User didn't selected directory.</exception>
    /// <exception cref="DirectoryNotFoundException">Reader didnt found logs dir.</exception>
    public class JSONReaderSingleton
    {
        //Error Logger
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string JournalsDirPath;
        private readonly ScreenShoter ScreenShoter = new ScreenShoter();
        public KillCounter Counter { get; set; } = new KillCounter();
        public bool ScreenShotBool { get; set; }

        //Storage variable for singleton
        private static JSONReaderSingleton JSONReaderInstance;

        /// <summary>
        /// Constructor of JSORReaderSingleton Class
        /// </summary>
        /// <param name="JournalsDirPath">Path where journals are located</param>
        /// <param name="JournalsDirPathOutput">Output of constructor which has Journals dir path inside method completition</param>
        /// <param name="ScreenShotBool">Bool that tells JSONReader if he can produce screenshots</param>
        private JSONReaderSingleton(string JournalsDirPath, out string JournalsDirPathOutput, bool ScreenShotBool)
        {
            int numberOfRetries = 0;
            do
            {
                if (CheckIfLogDirExists(JournalsDirPath) == true && CheckIfDirContainsLogs(JournalsDirPath) == true)//Checks if directory passed in arguments exists and contains logs
                {
                    this.JournalsDirPath = JournalsDirPath;
                    break;
                }
                else if (numberOfRetries >= 3) //User selected wrong directory more than 3 times, app exits
                {
                    MessageBox.Show("Error: U selected directory with no log files too many times. Program now terminates.");
                    throw new ArgumentException("User selected wrong directory too many times");
                }
                else if (CheckIfLogDirExists(JournalsDirPath) == false)
                {
                    MessageBox.Show("Error: Cant find directory. Please select directory where ED journals are located.");
                    log.Debug($"Directory not found.\r\nPath: {JournalsDirPath}");
                    numberOfRetries++;
                }
                else if (CheckIfDirContainsLogs(JournalsDirPath) == false)
                {
                    MessageBox.Show($"Error: No logs detected in directory. Select valid directory.\r\nPath: {JournalsDirPath}");
                    log.Debug($"Directory doesnt contain logs.\r\nPath: {JournalsDirPath}");
                    numberOfRetries++;
                }
                JournalsDirPath = SelectDirectory();
            } while (true);
            //sends directory path out so that it can be saved to config
            JournalsDirPathOutput = JournalsDirPath;
            this.ScreenShotBool = ScreenShotBool;
        }

        /// <summary>
        /// JSONReaderSingleton method that creates only one object of JSONReaderSingleton
        /// </summary>
        /// <param name="JournalsDirPath">Journals directory path</param>
        /// <param name="JournalsDirPathOutput">Output of constructor which has Journals dir path inside method completition</param>
        /// <param name="ScreenShotBool">Bool that tells JSONReader if he can produce screenshots</param>
        /// <returns></returns>
        public static JSONReaderSingleton GetInstance(string JournalsDirPath, out string JournalsDirPathOutput, bool ScreenShotBool)
        {
            string temp = null;
            if (JSONReaderInstance == null)
            {
                JSONReaderInstance = new JSONReaderSingleton(JournalsDirPath, out temp, ScreenShotBool);
            }
            else
            {
                MessageBox.Show("Warning: Cant create more then one object of JSONReaderSingleton");
                log.Warn("Warning: Tried to create more then one object of JSONReaderSingleton");
            }
            JournalsDirPathOutput = temp;
            return JSONReaderInstance;
        }

        /// <summary>
        /// Checks if directory exists
        /// </summary>
        /// <param name="JournalsDirPath">Path to selected directory</param>
        /// <returns>true - dir existe, false - dir doesnt exist</returns>
        private Boolean CheckIfLogDirExists(string JournalsDirPath)
        {
            if (Directory.Exists(JournalsDirPath) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if selected directory contains logs
        /// </summary>
        /// <param name="JournalsDirPath">Path to selected directory</param>
        /// <returns>true - contains logs, false - doesnt contain logs</returns>
        private Boolean CheckIfDirContainsLogs(string JournalsDirPath)
        {
            DirectoryInfo directory = new DirectoryInfo(JournalsDirPath);//directoryPath + @"\Saved Games\Frontier Developments\Elite Dangerous"
            FileInfo[] unsortedJournals = directory.GetFiles("*.log");
            if (unsortedJournals.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Detects thargoid kill from EDEvent class
        /// </summary>
        /// <param name="e1">converted JSON text to class</param>
        /// <param name="JSONStringLine">JSON text</param>
        private void DetectThargoidKill(EDEvent e1, string JSONStringLine)
        {
            ThargoidKillEvent kill;
            if (e1.@event.Equals("FactionKillBond"))
            {
                kill = JsonConvert.DeserializeObject<ThargoidKillEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                if (kill.AwardingFaction == null || kill.VictimFaction == null)
                {
                    log.Debug($"Faulty line passed to DetectThargoidKill. AwardingFaction and VictimFaction are null\r\n Text of line - {JSONStringLine}");
                }
                else if (kill.AwardingFaction.Equals(@"$faction_PilotsFederation;") || kill.VictimFaction.Equals(@"$faction_Thargoid;"))
                {
                    int caseSwitch = kill.Reward;
                    switch (caseSwitch)
                    {
                        case 10000:
                            Counter.Scout++;
                            break;
                        case 2000000:
                            Counter.Cyclops++;
                            break;
                        case 6000000:
                            Counter.Basillisk++;
                            break;
                        case 10000000:
                            Counter.Medusa++;
                            break;
                        case 15000000:
                            Counter.Hydra++;
                            break;
                        default:
                            Counter.Unknown++;
                            log.Info($"NEW THARGOID TYPE - Found unknown new type of thargoid. Credits for kill: {kill.Reward}");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if Json String line contains thargoid kill, returns name of Thargoid killed if true
        /// </summary>
        /// <param name="e1">converted JSON text to class</param>
        /// <param name="JSONStringLine">JSON text</param>
        /// <param name="ThargoidType">name of thargoid type</param>
        /// <returns>true on thargoid kill, false on no kill</returns>
        private bool DetectThargoidKill(EDEvent e1, string JSONStringLine, out string ThargoidType)
        {
            bool killDetected = false;
            ThargoidType = "No kill";
            ThargoidKillEvent kill;
            if (e1.@event.Equals("FactionKillBond"))
            {
                kill = JsonConvert.DeserializeObject<ThargoidKillEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                if (kill.AwardingFaction == null || kill.VictimFaction == null)
                {
                    log.Debug($"Faulty line passed to DetectThargoidKill. AwardingFaction and VictimFaction are null\r\n Text of line - {JSONStringLine}");
                }
                else if (kill.AwardingFaction.Equals(@"$faction_PilotsFederation;") || kill.VictimFaction.Equals(@"$faction_Thargoid;"))
                {
                    int caseSwitch = kill.Reward;
                    switch (caseSwitch)
                    {
                        case 10000:
                            Counter.Scout++;
                            //Method wont change boolean killDetected, because Scout screenshots are not interesting but it is counted
                            break;
                        case 80000: //Price for scouts in journals since Oddysey
                            Counter.Scout++;
                            //Method wont change boolean killDetected, because Scout screenshots are not interesting but it is counted
                            break;
                        case 2000000:
                            Counter.Cyclops++;
                            killDetected = true;
                            ThargoidType = $"Cyclops{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}"; //format of datetime day-month-hour-minutes-seconds
                            break;
                        case 8000000: //Price for Cyclops in journals since Oddysey
                            Counter.Cyclops++;
                            killDetected = true;
                            ThargoidType = $"Cyclops{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}"; //format of datetime day-month-hour-minutes-seconds
                            break;
                        case 6000000:
                            Counter.Basillisk++;
                            killDetected = true;
                            ThargoidType = $"Basillisk{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        case 24000000: //Price for Basillisks in journals since Oddysey
                            Counter.Basillisk++;
                            killDetected = true;
                            ThargoidType = $"Basillisk{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        case 10000000:
                            Counter.Medusa++;
                            killDetected = true;
                            ThargoidType = $"Medusa{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        case 40000000: //Price for Medusaws in journals since Oddysey
                            Counter.Medusa++;
                            killDetected = true;
                            ThargoidType = $"Medusa{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        case 15000000:
                            Counter.Hydra++;
                            killDetected = true;
                            ThargoidType = $"Hydra{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        case 60000000: //Price for Hydras in journals since Oddysey
                            Counter.Hydra++;
                            killDetected = true;
                            ThargoidType = $"Hydra{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                        default:
                            Counter.Unknown++;
                            log.Info($"NEW THARGOID TYPE - Found unknown new type of thargoid. Credits for kill: {kill.Reward}");
                            killDetected = true;
                            ThargoidType = $"Unknown{DateTime.UtcNow.ToString("dd-MM-HH-mm-ss")}";
                            break;
                    }
                }
            }
            return killDetected;
        }

        /// <summary>
        /// Method which reads JSON file 
        /// </summary>
        /// <param name="filePath"> - a path to a file</param>
        private void ReadJsonFile(string filePath)
        {
            //debug string for current JSONStringLine
            string JSONStringLine = "";
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fileStream);
                EDEvent currentEvent = null;

                while (!reader.EndOfStream)
                {
                    try
                    {
                        JSONStringLine = reader.ReadLine();
                        if (JSONStringLine.Equals("")) //if line contains nothing skips line and reads next
                        {
                            continue;
                        }
                        currentEvent = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        if (currentEvent != null)
                        { //reads only if corverter corverts JSON FILE
                            DetectThargoidKill(currentEvent, JSONStringLine);
                        }

                    }
                    catch (JsonReaderException ex)
                    {
                        log.Debug("JSONReader exception occured", ex);
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                log4net.GlobalContext.Properties["Prop1"] = JSONStringLine;
                log.Error($"Unknown error while reading file {filePath}");
                throw;
            }
            finally
            {
                //closes stream and reader in case of some unknown error
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Reads JSON file from selected line
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="line">Line number from which reader starts reading</param>
        /// <param name="firstRun">Line number from which reader starts reading</param>
        /// <returns>Line number where reader ended</returns>
        private int ReadJsonFile(string filePath, int line, bool firstRun)
        {
            //debug string for current JSONStringLine
            string JSONStringLine = "";
            FileStream fileStream = null;
            StreamReader reader = null;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = new StreamReader(fileStream);
                EDEvent currentEvent = null;

                //skips the lines that previous readers have red
                for (int i = 1; i < line; i++)
                {
                    reader.ReadLine();
                }

                while (!reader.EndOfStream)
                {
                    try
                    {
                        JSONStringLine = reader.ReadLine();
                        if (JSONStringLine.Equals("")) //if line contains nothing skips line and reads next
                        {
                            continue;
                        }
                        currentEvent = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        if (currentEvent != null)
                        { //reads only if corverter corverts JSON FILE                                                                                      
                            if (DetectThargoidKill(currentEvent, JSONStringLine, out string ThargoidType) && ScreenShotBool == true && firstRun == false)
                            {
                                ScreenShoter.MakeScreenShot(ThargoidType);
                            }
                        }
                        line++;
                    }
                    catch (JsonReaderException ex)
                    {
                        log.Debug("JSONReader exception occured", ex);
                        continue;
                    }
                }
                return line;
            }
            catch (Exception)
            {
                log4net.GlobalContext.Properties["Prop1"] = JSONStringLine;
                log.Error($"Unknown error while reading file {filePath}");
                throw;
            }
            finally
            {
                //closes stream and reader in case of some unknown error
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Reads last Log while game is running(reads or tries to find a new file to read until user closes application) REQUIRES another thread
        /// </summary>
        public void ReadLastJsonWhilePlaying()
        {
            //gets last Journal from ED directory
            List<FileInfo> sortedJournalsList = GetJournalsInDirectory();
            FileInfo lastLog = sortedJournalsList[sortedJournalsList.Count - 1];
            byte[] lastLogHash = GetFileHash(lastLog.FullName);
            int line = ReadJsonFile(lastLog.FullName, 1, true);

            //Control variables
            FileInfo controlLog;
            byte[] controlHash;
            DateTime currentLastTimeWritten = DateTime.Now;
            do
            {
                sortedJournalsList = GetJournalsInDirectory();
                controlLog = sortedJournalsList[sortedJournalsList.Count - 1];
                controlHash = GetFileHash(controlLog.FullName);

                //true if file changed (compares hash value of lastLog a
                if (BitConverter.ToString(lastLogHash) != BitConverter.ToString(controlHash) && lastLog.Name.Equals(controlLog.Name) || lastLog.LastWriteTime > currentLastTimeWritten)
                {
                    currentLastTimeWritten = lastLog.LastWriteTime;
                    lastLogHash = controlHash;
                    line = ReadJsonFile(lastLog.FullName, line, false);
                }
                //True if at the end of file and directory has a newer log file
                else if (!controlLog.Name.Equals(lastLog.Name))
                {
                    line = 1;
                    lastLog = controlLog;
                    line = ReadJsonFile(lastLog.FullName, line, false);
                }
                //else sleeps thread and restarts fileChanged cycle 2 sec later
                else
                {
                    Thread.Sleep(2000);
                }

            } while (true);
        }

        /// <summary>
        /// Gets hash value of file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Hash value of file</returns>
        private byte[] GetFileHash(string filePath)
        {
            HashAlgorithm hashAlg = HashAlgorithm.Create();
            byte[] fileHash;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileHash = hashAlg.ComputeHash(fileStream);
                hashAlg.Clear();
                fileStream.Close();
            }
            return fileHash;
        }

        /// <summary>
        /// Reads all Journal files in selected directory
        /// </summary>
        public void ReadDirectory()
        {
            const int NUMBER_OF_RETRIES = 1;
            const int DELAY = 10;
            Counter.ResetThargoidKills();
            List<FileInfo> journalsList = JSONReaderInstance.GetJournalsInDirectory();
            string path = "";
            //reads all Journal files except last one (the one which Elite Dangerous writes in real-time)
            for (int i = 0; i < journalsList.Count - 1; i++)
            {
                path = journalsList[i].FullName;
                //Console.WriteLine("Reading: " + path);
                for (int j = 0; j <= NUMBER_OF_RETRIES; ++j)
                {
                    try
                    {
                        JSONReaderInstance.ReadJsonFile(path);
                        break;
                    }
                    catch (IOException) when (j < NUMBER_OF_RETRIES)
                    {
                        System.Threading.Thread.Sleep(DELAY);
                    }
                    catch (IOException ex)
                    {
                        log.Info($"Cant access file: {path}", ex);
                    }
                }
            }
            //Initiates garbage collection at the end
            GC.Collect();
        }

        /// <summary>
        /// Method which find all Elite dangerous Journals in selected directory
        /// </summary>
        /// <returns>List of Journals </returns>
        ///<exception cref="DirectoryNotFoundException">Logs Direcory not found.</exception>
        private List<FileInfo> GetJournalsInDirectory()
        {
            //regex that matches ED journals names
            Regex regex = new Regex(@"(Journal)\.(\d{12})\.(\d{2})\.log"); //matches with Journal.123456789109.01.log
            DirectoryInfo directory = new DirectoryInfo(JournalsDirPath);//directoryPath + @"\Saved Games\Frontier Developments\Elite Dangerou"
            FileInfo[] unsortedJournals = null;
            try
            {
                //field of unsorted files from directory above
                unsortedJournals = directory.GetFiles("*.log");
            }
            catch (DirectoryNotFoundException ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            //List for sorted ED Journal files from directory
            //List<FileInfo>
            List<FileInfo> sortedJournalsList = new List<FileInfo>();

            //matches filenames with regex and sorts ED Journal files from others
            for (int i = 0; i < unsortedJournals.Length; i++)
            {
                if (regex.IsMatch(unsortedJournals[i].Name))
                {
                    sortedJournalsList.Add(unsortedJournals[i]);
                }
            }
            //Orders list by Last time it was written into (descending)
            sortedJournalsList = sortedJournalsList.OrderBy(x => x.LastWriteTime).ToList();
            return sortedJournalsList;
        }

        /// <summary>
        /// Shows folder browser to user which selects ED journals directory
        /// </summary>
        /// <returns>List of Journals</returns>
        /// <exception cref="ArgumentException">User didn't selected directory.</exception>
        private string SelectDirectory()
        {

            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog1.SelectedPath;
            }
            else
            {
                throw new ArgumentException("User didnt selected directory.");
            }
        }
    }
}
