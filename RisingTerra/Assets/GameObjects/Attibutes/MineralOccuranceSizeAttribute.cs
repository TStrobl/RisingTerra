using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.Attibutes
{
    public class MineralOccuranceSizeAttribute : Attribute
    {
        /// <summary>
        /// Die Anzahl der Blöcke, die zusammen ein Mineralienvorkommen darstellen
        /// </summary>
        public int CountOfBlocks { get; set; }

        public MineralOccuranceSizeAttribute(int countOfBlocks)
        {
            this.CountOfBlocks = countOfBlocks;
        }
    }
}
