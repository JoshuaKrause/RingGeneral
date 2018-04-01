using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console
{
    class GameObjectException : SystemException
    {
        public GameObjectException(string message) : base (message)
        {
        }
    }
}
