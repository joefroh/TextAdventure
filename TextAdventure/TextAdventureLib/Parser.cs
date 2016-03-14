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
        private List<Node> nodes;
        public Parser(string filename)
        {
            file = new FileInfo(filename);
            nodes = new List<Node>();
            if (!file.Exists)
            {
                throw new ArgumentException(String.Format("The file {0} to the Parser contructor does not exist.",file.FullName));
            }
        }

        public List<Node> Parse()
        {
            using (StreamReader reader = new StreamReader(file.FullName))
            {
                NodeFactory factory = new NodeFactory(reader);
                while (!factory.EndOfStream)
                {
                    nodes.Add(factory.GetNextNode());
                }
            }
            return nodes;
        }
    }
}
