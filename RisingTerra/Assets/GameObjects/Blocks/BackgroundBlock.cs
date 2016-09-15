using UnityEngine;
using System.Collections;
using UnityEditor;
using Assets.GameObjects.Attibutes;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(SpriteRenderer))]
[ExecuteInEditMode]
public class BackgroundBlock : MonoBehaviour
{
    /// <summary>
    /// Die Pfade zu den einzelnen Images
    /// </summary>
    private Dictionary<Enums.BlockTypes, string> _imagePaths;


    private SpriteRenderer _spriteRenderer;

    private Enums.BlockTypes prevBlocktype;

    public Enums.BlockTypes BlockType;

    void Awake()
    {
        this._spriteRenderer = this.GetComponent<SpriteRenderer>();
        this._imagePaths = new Dictionary<Enums.BlockTypes, string>();
    }

    // Use this for initialization
    void Start()
    {
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
            if (!this._imagePaths.ContainsKey(this.BlockType))
            {
                var imageAttr = this.BlockType.GetMemberAttribute<BlockImageAttribute>();
                if (imageAttr != null)
                {
                    this._imagePaths.Add(this.BlockType, imageAttr.BackgroundImagePath);
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
