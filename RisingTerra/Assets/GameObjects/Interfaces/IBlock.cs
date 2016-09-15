using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects
{
    interface IBlock
    {
        /// <summary>
        /// Block-Klasse, also je nachdem was gerendert werden soll
        /// </summary>
        Enums.BlockLevel BlockClass { get; }

        /// <summary>
        /// Name des Layers
        /// </summary>
        string LayerName { get; }

        /// <summary>
        /// Lädt das Bild
        /// </summary>
        /// <returns></returns>
        Sprite LoadSprite();
    }
}
