using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    /// <summary>
    /// X-Koordinate im Biom
    /// </summary>
	public int X { get; set; }

    /// <summary>
    /// Y-Koordinate im Biom
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Typ des Blocks im Vordergrund
    /// </summary>
    public ushort ForegroundType { get; set; }

    /// <summary>
    /// Typ des Blocks im Hintergrund
    /// </summary>
    public ushort BackgroundType { get; set; }
}
