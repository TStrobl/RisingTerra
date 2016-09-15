using UnityEngine;
using System.Collections;
using Assets.GameObjects.Attibutes;

public class Enums : MonoBehaviour
{
    public enum WorldSize
    {
        Normal,
        Big
    }

    public enum BiomeTypes
    {
        Home,
        Gras,
        Snow
    }

    /// <summary>
    /// Die möglichen Blocktypen
    /// </summary>
    public enum BlockTypes
    {
        [BlockImage("", "")]
        Nothing,
        [BlockImage("Tiles/Earth_BG_1", "Tiles/Earth_FG_1")]
        Earth,
        [BlockImage("Tiles/Stone_BG_1", "Tiles/Stone_FG_1")]
        Stone,
        [BlockImage("", "Tiles/Water_FG")]
        Water,
        [BlockImage("Tiles/Iron_BG_1", "Tiles/Iron_FG_1")]
        Iron,
        [BlockImage("Tiles/Coal_BG_1", "Tiles/Coal_FG_1")]
        Coal
    }

    /// <summary>
    /// Die möglichen Blöcke, die sich im Hintergrund befinden können
    /// </summary>
    public enum BlockLevel
    {
        Background,
        Foreground
    }
}
