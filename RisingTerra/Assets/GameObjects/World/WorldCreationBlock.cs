
/// <summary>
/// Ein reines Datenhaltungsobjekt ohne Overhaed, damit die Algorithmik für die Welterstellung darauf laufen kann
/// Dabei soll möglichst wenig RAM nötig sein.
/// </summary>
public struct WorldCreationBlock
{
    /// <summary>
    /// Typ des Blocks im Vordergrund
    /// </summary>
    public ushort ForegroundType { get; set; }

    /// <summary>
    /// Typ des Blocks im Hintergrund
    /// </summary>
    public ushort BackgroundType { get; set; }

    /// <summary>
    /// Ist hier eine Höhle
    /// </summary>
    public bool IsCave { get; set; }

    /// <summary>
    /// Ist hier irgendeine Flüssigkeit, also z.B. Wasser
    /// </summary>
    public bool IsFluid { get; set; }

    /// <summary>
    /// Ist hier ein Mineralienvorkommen, also z.b. Kohle
    /// </summary>
    public bool IsMineral { get; set; }

    /// <summary>
    /// Die Größe des Objekts zum Berechnen des BinaryReaders
    /// </summary>
    public static ushort ByteSize = 4;
}
