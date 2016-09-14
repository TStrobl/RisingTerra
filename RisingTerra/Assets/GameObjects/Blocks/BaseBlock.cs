using UnityEngine;
using System.Collections;

public class BaseBlock : MonoBehaviour {

    /// <summary>
    /// Position auf der X-Achse
    /// </summary>
    public ushort PosX;

    /// <summary>
    /// Position auf der Y-Achse
    /// </summary>
    public ushort PosY;

    /// <summary>
    /// Relative Position auf der X-Achse (im sichtbaern BIOM-Feld)
    /// </summary>
    public ushort RelativePosX;

    /// <summary>
    /// Relative Position auf der Y-Achse (im sichtbaern BIOM-Feld)
    /// </summary>
    public ushort RelativePosY;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
