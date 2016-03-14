using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TextAdventureLib
{
    public class NodeFactory
    {
        private readonly StreamReader _reader;

        public NodeFactory(StreamReader reader)
        {
            _reader = reader;
        }

        public bool EndOfStream
        {
            get { return _reader.EndOfStream; }
        }

        public Node GetNextNode()
        {
            var nodeText = new StringBuilder();
            var newNode = new Node();
            var line = "";
            while (line != "#end")
            {
                line = _reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    if (line[0] == '#') //Must be a command line
                    {
                        var command = GetCommandFromLine(line);
                        ApplyCommand(command, newNode);
                    }
                    else //must be node text then
                    {
                        nodeText.AppendLine(line);
                    }
                }
            }
            newNode.Text = nodeText.ToString();
            return newNode;
        }

        private void ApplyCommand(Command command, Node newNode)
        {
            switch (command.Type)
            {
                case CommandType.end:
                    break;
                case CommandType.input:
                    var input = new UserInput();
                    input.MatchText = command.Attributes["input"];
                    input.Target = command.Attributes["target"];
                    newNode.Inputs.Add(input);
                    break;
                case CommandType.label:
                    newNode.Label = command.Attributes["label"];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            HandleModifiers(command, newNode);
        }

        private void HandleModifiers(Command command, Node newNode)
        {
            if (command.Type == CommandType.label)
            {
                foreach (var commandModifyer in command.Modifyers)
                {
                    if (commandModifyer == CommandModifyer.start || commandModifyer == CommandModifyer.finish)
                    {
                        newNode.Type = commandModifyer;
                    }
                }
            }
        }

        private Command GetCommandFromLine(string line)
        {
            var chunks = line.Split(' ');
            var type = GetCommandTypeFromChunk(chunks[0]);

            var mods = new List<CommandModifyer>();
            for (var i = 1; i < chunks.Length; i++)
            {
                if (chunks[i][0] == '%')
                {
                    mods.Add(GetCommandModifyerFromChunk(chunks[i]));
                }
                else
                {
                    throw new ParserInputException(string.Format("Unexpected input in line {0}.", chunks[i]));
                }
            }

            var attrs = GetAttributesFromChunks(chunks);

            var command = new Command(attrs) {Type = type, Modifyers = mods};

            return command;
        }

        private CommandType GetCommandTypeFromChunk(string chunk)
        {
            var typeString = chunk.Substring(1).Split('=')[0];
            try
            {
                return (CommandType) Enum.Parse(typeof (CommandType), typeString);
            }
            catch
            {
                throw new ArgumentException(string.Format("Command {0} is not a valid command", typeString));
            }
        }

        private CommandModifyer GetCommandModifyerFromChunk(string chunk)
        {
            var typeString = chunk.Substring(1).Split('=')[0];
            try
            {
                return (CommandModifyer) Enum.Parse(typeof (CommandModifyer), typeString);
            }
            catch
            {
                throw new ArgumentException(string.Format("Command {0} is not a valid command", typeString));
            }
        }

        private Dictionary<string, string> GetAttributesFromChunks(IEnumerable<string> chunks)
        {
            var dict = new Dictionary<string, string>();

            foreach (var chunk in chunks)
            {
                var split = chunk.Split('=');
                if (split.Length == 2)
                {
                    dict.Add(split[0].Substring(1), split[1].Substring(1, split[1].Length - 2)); //remove the quotes
                }
            }

            return dict;
        }
    }
}