using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : ScriptableObject
{

    /// <summary>
    /// Name der Welt
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Die einzelnen Biome / Levels
    /// </summary>
    public List<Biome> Biomes { get; set; }

}
