using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.GameObjects.Attibutes;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class ForegroundBlock : MonoBehaviour
{
    /// <summary>
    /// Die Pfade zu den einzelnen Images
    /// </summary>
    private Dictionary<Enums.BlockTypes, string> _imagePaths;

    /// <summary>
    /// De Kollisionsabfrage
    /// </summary>
    private BoxCollider2D _boxCollider;

    private SpriteRenderer _spriteRenderer;

    private Enums.BlockTypes prevBlocktype;

    public Enums.BlockTypes BlockType;

    void Awake()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._imagePaths = new Dictionary<Enums.BlockTypes, string>();
        this._boxCollider = this.GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    void Start()
    {
        if(this.BlockType == Enums.BlockTypes.Nothing)
        {
            this._boxCollider.enabled = false;
        }

        this.ChangeSpriteForBlockType();
    }

    // Update is called once per frame
    void Update()
    {
        this.ChangeSpriteForBlockType();
    }

    private void ChangeSpriteForBlockType()
    {
        if (prevBlocktype != this.BlockType)
        {
            prevBlocktype = this.BlockType;
            if(this.BlockType != Enums.BlockTypes.Nothing)
            {
                this._boxCollider.enabled = true;
            }

            if (!this._imagePaths.ContainsKey(this.BlockType))
            {
                var imageAttr = this.BlockType.GetMemberAttribute<BlockImageAttribute>();
                if (imageAttr != null)
                {
                    this._imagePaths.Add(this.BlockType, imageAttr.ForegroundImagePath);
                }

            }
            if (string.IsNullOrEmpty(this._imagePaths[this.BlockType]))
            {
                if (Application.isPlaying)
                {
                    this._spriteRenderer.sprite = null;
                }
                else
                {
                    this._spriteRenderer.sprite = Resources.Load<Sprite>("Tiles/Placeholder");
                }
            }
            else
            {
                this._spriteRenderer.sprite = Resources.Load<Sprite>(this._imagePaths[this.BlockType]);
            }
        }
    }
}
