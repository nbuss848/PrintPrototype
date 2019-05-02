using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PrintPrototype
{
    public class Test
    {
        public string name { get; set; }
        public int value { get; set; }
        public bool in_ca { get; set; }
        public int taxed_value { get; set; }
        public string barcode { get; internal set; }
    }
}