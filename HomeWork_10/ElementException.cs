﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10
{
    public class ElementException : Exception
    {
        public ElementException(string str):base(str) { }
    }
}
