using UnityEngine;
using System.Collections;
using Assets.GameObjects.Blocks;
using System.Collections.Generic;

public class ApplicationModel
{
    /// <summary>
    /// Der Name der Datei, des aktuell zu ladenden Bioms
    /// </summary>
    public static string CurrentBiomeFileName { get; set; }

    /// <summary>
    /// Die Pfade zu den einzelnen Images
    /// </summary>
    public static Dictionary<Enums.BlockTypes, string> ImagePaths;

    /// <summary>
    /// Die Anzahl der im Speiceher gehaltenen Blöcke in der Horizontalen
    /// </summary>
    public const int VisibleBlocksHorizontal = 80;

    /// <summary>
    /// Die Anzahl im Speiceher gehaltenen Blöcke in der Vertikalen
    /// </summary>
    public const int VisibleBlocksVertical = 50;

    /// <summary>
    /// Höhe eines Blocks
    /// </summary>
    public const int BlockHeight = 25;

    /// <summary>
    /// Breite eines Blocks
    /// </summary>
    public const int BlockWidth = 25;

    /// <summary>
    /// Der aktuell "selektierte ForegroundBlock"
    /// </summary>
    public static ForegroundBlock CurrentSelectedForegroundBlock { get; set; }

    /// <summary>
    /// Gibt die Breite einer Welt zurück
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static ushort GetWorldWidth(Enums.WorldSize size)
    {
        switch (size)
        {
            case Enums.WorldSize.Big:
                return 4000;
            case Enums.WorldSize.Normal:
                return 2000;
        }
        return 0;
    }

    /// <summary>
    /// Gibt die Höhe einer Welt zurück
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static ushort GetWorldHeight(Enums.WorldSize size)
    {
        switch (size)
        {
            case Enums.WorldSize.Big:
                return 3800;
            case Enums.WorldSize.Normal:
                return 1800;
        }
        return 0;
    }
}
