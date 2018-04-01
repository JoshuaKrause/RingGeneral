using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral
{
    // Used to cast GameObjects as exportable.
    public interface IExportable
    {
        string Export();
    }
}
