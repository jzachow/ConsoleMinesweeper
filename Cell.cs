using System;
using System.Collections.Generic;
using System.Text;

namespace GroupDMinefieldMidterm
{
    public class Cell
    {
        public GameValues CellValue { get; set; }
        public bool Revealed { get; set; }
        public bool Flagged { get; set; }
    }
}
