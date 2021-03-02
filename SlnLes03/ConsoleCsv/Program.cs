using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCsv
{
    class Program
    {
        static void Main(string[] args)
        {
            // prepare
            List<string> lines = new List<string>();
            lines.Add("Dit is lijn 1");
            lines.Add("Dit is lijn 2");
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(folderPath, "myfile.txt");
            // open stream and start writing
            using (StreamWriter writer = File.CreateText(filePath))
            {
                foreach (string line in lines)
                {
                    writer.WriteLine(line);
                }
            } // stream closes automatically
        }
    }
}
