using Assets.GameObjects.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameObjects.Interfaces
{
    public interface IBiome
    {
        /// <summary>
        /// Der Haupttyp des Bioms / z.b. Erde, Schnee, etc
        /// </summary>
        Enums.BlockTypes MainBlockType { get; }

        /// <summary>
        /// Alle Mineralien-Tyoen, die das Biom enthalten kann mit deren Verhältnissen zu einander
        /// </summary>
        List<MineralRate> AvailableMinerals { get; }
    }
}
