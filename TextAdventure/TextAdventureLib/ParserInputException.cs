using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventureLib
{
    class ParserInputException : ArgumentException
    {
        public ParserInputException(string message) : base(message)
        {

        }
    }
}
