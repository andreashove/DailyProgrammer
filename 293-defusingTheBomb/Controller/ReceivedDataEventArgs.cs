using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _293_defusingTheBomb
{
    public class ReceivedDataEventArgs : EventArgs
    {
        int userInput;

        public int UserInput
        {
            get;
            set;
        }
    }
}
