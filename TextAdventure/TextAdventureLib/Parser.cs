using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureLib
{
    public class Parser
    {
        FileInfo file;
        public Parser(string filename)
        {
            file = new FileInfo(filename);

            if (!file.Exists)
            {
                throw new ArgumentException(String.Format("The file {0} to the Parser contructor does not exist.",file.FullName));
            }
        }

        public void Parse()
        {
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                NodeFactory factory = new NodeFactory(reader);
                factory.GetNextNode();
            }
        }
    }
}
