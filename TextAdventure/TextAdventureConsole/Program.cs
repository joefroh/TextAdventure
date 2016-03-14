using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureLib;

namespace TextAdventureConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("Sample1.taf");
            var nodes = parser.Parse();
            Engine engine = new Engine(nodes);
            engine.Play();

            Console.WriteLine("Done! ENTER to exit.");
            Console.ReadLine();
        }
    }
}
