using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// Reads a JSON file and returns string
        /// </summary>
        public class JSONReader
        {

            bool firstClick = false;
            int read = 0;
            int index = 0;
            Dictionary<int, EDEvent> eventsDictionary = new Dictionary<int, EDEvent>();

            public string ReadJsonFile(String filePath)
            {
                try
                {
                    if (firstClick == false)
                    {
                        String path = filePath; //Path of a file
                        String JSONStringLine = ""; //Variable for JSON text line


                        EDEvent e1;
                        System.IO.StreamReader file = new System.IO.StreamReader(path); //Streamreader which reads file line by line
                        while ((JSONStringLine = file.ReadLine()) != null)
                        {
                            //JSON convertor which deserializes JSON to class variables
                            e1 = JsonConvert.DeserializeObject<EDEvent>(JSONStringLine, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                            eventsDictionary[index] = e1;
                            index++;
                        }
                        firstClick = true;
                        return "Nacteno stisknete znovu pro prochazeni";

                    }
                    else
                    {
                        string returnString = "";
                        if (read != index)
                        {
                            returnString = eventsDictionary[read].ToString();
                            read++;
                        }
                        else
                        {
                            returnString = "End of file";
                        }
                        return returnString;

                    }

                }
                catch (FileNotFoundException)
                {
                    return "File not found";
                }
            }

        }
}
