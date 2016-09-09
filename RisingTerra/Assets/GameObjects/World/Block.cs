using UnityEngine;
using System.Collections;

public class Block : ScriptableObject
{
    public Block _block;

    public Block Create(int x, int y, ushort foreGroundType, ushort bakgroundType)
    {
        return Instantiate(_block);
    }


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
