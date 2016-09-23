using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.GameObjects.Attibutes;
using Assets.GameObjects.Blocks;

namespace Assets.GameObjects.Blocks
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [ExecuteInEditMode]
    public class ForegroundBlock : BaseBlock
    {


        /// <summary>
        /// De Kollisionsabfrage
        /// </summary>
        private BoxCollider2D _boxCollider;

        /// <summary>
        /// Der Renderer für das Bild
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// der vorige Typ des Blocks
        /// </summary>
        private Enums.BlockTypes prevBlocktype;

        /// <summary>
        /// Der aktuelle Typ des Blocks
        /// </summary>
        public Enums.BlockTypes BlockType;

        protected override void Awake()
        {
            base.Awake();
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._boxCollider = this.GetComponent<BoxCollider2D>();
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            if (this.BlockType == Enums.BlockTypes.Nothing)
            {
                this._boxCollider.enabled = false;
            }

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
                if (this.BlockType != Enums.BlockTypes.Nothing)
                {
                    this._boxCollider.enabled = true;
                }

                if (string.IsNullOrEmpty(ApplicationModel.ImagePaths[this.BlockType]))
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
                    this._spriteRenderer.sprite = Resources.Load<Sprite>(ApplicationModel.ImagePaths[this.BlockType]);
                }
            }
        }

        void OnMouseOver()
        {
            if (this._isInRange && this._spriteRenderer.color.a > 0.7f)
            {
                this._spriteRenderer.color = new Color(1.55f, 1.55f, 1.55f, 0.7f);
                ApplicationModel.CurrentSelectedForegroundBlock = this;
            }
        }

        void OnMouseExit()
        {
            this._spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            ApplicationModel.CurrentSelectedForegroundBlock = null;
        }
    }
}