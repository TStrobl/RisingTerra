using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.World
{
    public class MineralRate
    {
        /// <summary>
        /// Das betroffene Mineral
        /// </summary>
        public Enums.BlockTypes MineralType { get; set; }

        /// <summary>
        /// Die prozentuale Rate des Minerals basierend auf 1 = 100%
        /// </summary>
        public double Rate { get; set; }
    }
}
