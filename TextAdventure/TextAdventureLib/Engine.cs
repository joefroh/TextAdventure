using System;
using System.Collections.Generic;

namespace TextAdventureLib
{
    public class Engine
    {
        public Engine(List<Node> nodes)
        {
            Nodes = nodes;
        }

        public List<Node> Nodes { get; }

        public void Play()
        {
            var currentNode = Nodes.Find(o => o.Type == CommandModifyer.start);
            while (currentNode != null && currentNode.Type != CommandModifyer.finish)
            {
                DisplayNode(currentNode);
                var input = GetUserInput();
                currentNode = HandleInput(currentNode, input);
            }

            if (currentNode.Type == CommandModifyer.finish)
            {
                DisplayNode(currentNode);
            }
        }

        private Node HandleInput(Node currentNode, string input)
        {
            var nodeLabel = currentNode.Inputs.Find(o => o.MatchText == input);
            if (nodeLabel == null)
            {
                return currentNode;
            }

            var nextNode = Nodes.Find(o => o.Label == nodeLabel.Target);
            if (nextNode == null)
            {
                return null;
            }

            return nextNode;
        }

        private string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        private void DisplayNode(Node node)
        {
            Console.WriteLine(node.Text);
            foreach (var input in node.Inputs)
            {
                Console.Write(input.MatchText+"\t");
            }
            Console.WriteLine();
        }
    }
}