using UnityEngine;
using System.Collections;
using Assets.GameObjects;

[RequireComponent(typeof(SpriteRenderer))]
public class BaseBlock : MonoBehaviour
{
    /// <summary>
    /// Wurde der Block geladen / gerendert
    /// </summary>
    public bool Loaded { get; set; }

    /// <summary>
    /// x-Koordinate des Blocks
    /// </summary>
    public ushort X { get; set; }

    /// <summary>
    /// Y-Koordinate des Blocks
    /// </summary>
    public ushort Y { get; set; }

    /// <summary>
    /// Allgemeine Höhe eines Blocks
    /// </summary>
    public static ushort BlockHeight = 25;

    /// <summary>
    /// Allgemeine Breite eines Blocks
    /// </summary>
    public static ushort BlockWidth = 25;

    // Use this for initialization
    public void Start()
    {
        if (!this.Loaded)
        {
            var sprite = ((IBlock)this).LoadSprite();
            var spriteRenderer = this.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = ((IBlock)this).LayerName;



            this.transform.localPosition = new Vector3(0,0);

            this.Loaded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.Loaded)
        {
            var sprite = ((IBlock)this).LoadSprite();
            var spriteRenderer = this.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = ((IBlock)this).LayerName;
            this.Loaded = true;
        }
    }
}
