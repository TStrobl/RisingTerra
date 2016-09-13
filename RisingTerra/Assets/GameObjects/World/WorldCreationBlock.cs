
/// <summary>
/// Ein reines Datenhaltungsobjekt ohne Overhaed, damit die Algorithmik für die Welterstellung darauf laufen kann
/// Dabei soll möglichst wenig RAM nötig sein.
/// </summary>
public struct WorldCreationBlock
{
    /// <summary>
    /// X-Koordinate im Biom
    /// </summary>
	public ushort X { get; set; }

    /// <summary>
    /// Y-Koordinate im Biom
    /// </summary>
    public ushort Y { get; set; }

    /// <summary>
    /// Typ des Blocks im Vordergrund
    /// </summary>
    public ushort ForegroundType { get; set; }

    /// <summary>
    /// Typ des Blocks im Hintergrund
    /// </summary>
    public ushort BackgroundType { get; set; }
}
