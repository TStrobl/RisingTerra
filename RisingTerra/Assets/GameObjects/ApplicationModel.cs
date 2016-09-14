using UnityEngine;
using System.Collections;

public class ApplicationModel
{
    /// <summary>
    /// Der Name der Datei, des aktuell zu ladenden Bioms
    /// </summary>
    public static string CurrentBiomeFileName { get; set; }

    /// <summary>
    /// Die Anzahl der sichtbaren Blöcke in der Horizontalen
    /// </summary>
    public const int VisibleBlocksHorizontal = 200;

    /// <summary>
    /// Die Anzahl der sichtbaren Blöcke in der Vertikalen
    /// </summary>
    public const int VisibleBlocksVertical = 200;

    /// <summary>
    /// Höhe eines Blocks
    /// </summary>
    public const int BlockHeight = 25;

    /// <summary>
    /// Breite eines Blocks
    /// </summary>
    public const int BlockWidth = 25;

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
