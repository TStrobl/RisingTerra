using UnityEngine;
using System.Collections;
using Boo.Lang;

public class Biome : MonoBehaviour
{
    /// <summary>
    /// Das Level, also wie stark z.B. die Gegner sind
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// Größe der Welt
    /// </summary>
    public Enums.WorldSize WorldSize { get; set; }

    /// <summary>
    /// Der Typ
    /// </summary>
    public Enums.BiomeTypes BiomeType { get; set; }

    /// <summary>
    /// Höhe des Bioms in Blöcken
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Breite des Bioms in Blöcken
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Die einzelnen Blöcke des Biomes
    /// </summary>
    public List<Block> Blocks { get; set; }
}
