﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingGeneral_console.GameObjects
{
    class Gimmick : Modifier
    {
        public Gimmick()
        {
            HasPrerequisites = true;
        }
    }
}
