using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureLib
{
    public class NodeFactory
    {
        private StreamReader _reader;

        public NodeFactory(StreamReader reader)
        {
            _reader = reader;
        }

        public bool EndOfStream
        {
            get
            {
                return _reader.EndOfStream;
            }
        }

        public Node GetNextNode()
        {
            StringBuilder nodeText = new StringBuilder();
            Node newNode = new Node();
            var line = _reader.ReadLine();
            if (line[0] == '#') //Must be a command line
            {
                var command = GetCommandFromLine(line);
            }
            else //must be node text then
            {
                nodeText.AppendLine(line);
            }

            return newNode;
        }

        private Command GetCommandFromLine(string line)
        {
            var chunks = line.Split(' ');
            CommandType type = GetCommandTypeFromChunk(chunks[0]);

            var mods = new List<CommandModifyer>();
            for( int i = 1; i < chunks.Length; i++)
            {
                if (chunks[i][0] == '%')
                {
                    mods.Add(GetCommandModifyerFromChunk(chunks[i]));
                }
                else
                {
                    throw new ParserInputException(string.Format("Unexpected input in line {0}.",chunks[i]));
                }
            }

            return new Command { Type = type, Modifyers = mods };
        }

        private CommandType GetCommandTypeFromChunk(string chunk)
        {
            var typeString = chunk.Substring(1).Split('=')[0];
            try
            {
                return (CommandType)Enum.Parse(typeof(CommandType), typeString);
            }
            catch
            {
                throw new ArgumentException(String.Format("Command {0} is not a valid command", typeString));
            }
        }

        private CommandModifyer GetCommandModifyerFromChunk(string chunk)
        {
            var typeString = chunk.Substring(1).Split('=')[0];
            try
            {
                return (CommandModifyer)Enum.Parse(typeof(CommandModifyer), typeString);
            }
            catch
            {
                throw new ArgumentException(String.Format("Command {0} is not a valid command", typeString));
            }
        }

    }
}
