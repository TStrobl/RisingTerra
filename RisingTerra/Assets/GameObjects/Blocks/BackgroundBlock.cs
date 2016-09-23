using UnityEngine;
using System.Collections;
using Assets.GameObjects.Attibutes;
using System.Collections.Generic;
using Assets.GameObjects.Blocks;

namespace Assets.GameObject.Blocks
{
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteInEditMode]
    public class BackgroundBlock : BaseBlock
    {
        /// <summary>
        /// Die Pfade zu den einzelnen Images
        /// </summary>
        private Dictionary<Enums.BlockTypes, string> _imagePaths;


        private SpriteRenderer _spriteRenderer;

        private Enums.BlockTypes prevBlocktype;

        public Enums.BlockTypes BlockType;

        protected override void Awake()
        {
            base.Awake();
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._imagePaths = new Dictionary<Enums.BlockTypes, string>();
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            this.ChangeSpriteForBlockType();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
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
}