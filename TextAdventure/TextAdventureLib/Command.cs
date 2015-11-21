using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureLib
{
    internal class Command
    {
        public CommandType Type { get; set; }

        public List<CommandModifyer> Modifyers { get; set; }
    }
}
