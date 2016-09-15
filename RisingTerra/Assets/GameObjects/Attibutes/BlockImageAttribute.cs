using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.Attibutes
{
    public class BlockImageAttribute : Attribute
    {
        public string BackgroundImagePath { get; set; }

        public string ForegroundImagePath { get; set; }

        public BlockImageAttribute(string bgImagePath, string fgImagePath)
        {
            this.BackgroundImagePath = bgImagePath;
            this.ForegroundImagePath = fgImagePath;
        }
    }
}
