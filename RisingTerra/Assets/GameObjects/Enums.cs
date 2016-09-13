using UnityEngine;
using System.Collections;

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

    public enum BlockClass
    {
        Foreground,
        Background,
        Item,
        Mine
    }

    /// <summary>
    /// Die möglichen Basis-Blöcke - Blöcke, die das grundlegende Layout eines Bioms darstellen, ohne Besonderheiten wie Dungeons, Minen, die Basis usw.
    /// </summary>
    public enum BaseBlockType
    {
        Nothing,
        Earth,
        Stone,
        Water,
        Iron,
        Coal
    }
}
