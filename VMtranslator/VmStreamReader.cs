using System;
using System.Collections.Generic;
using System.IO;

namespace VMtranslator
{
    public class VmStreamReader
    {

        static public List<string> ReadStream()
        {
            List<string> result = new List<string>();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using StreamReader sr = new StreamReader("../../../vm.txt");
                string line;

                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    var indexCheck = line.IndexOf("/");
                    if (indexCheck > -1)
                        line = line.Substring(0, indexCheck);

                    if (line != "")
                        result.Add(line);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return result;
        }
    }
}
