using UnityEngine;
using System.Collections;
using System;

namespace Assets.GameObjects.PlayerItems.Tools
{
    public abstract class Pickaxe : PlayerItem, IPlayerItem
    {
        /// <summary>
        /// Der Renderer für das Bild
        /// </summary>
        protected SpriteRenderer _spriteRenderer;

        public Enums.ItemClass ItemClass
        {
            get
            {
                return Enums.ItemClass.Tool;
            }
        }

        public Enums.ItemType ItemType
        {
            get
            {
                return Enums.ItemType.Pickaxe;
            }
        }

        public abstract float Strength { get; } //1 ist die schwächste, also Holz

        public abstract Enums.Materials Material { get; }

        /// <summary>
        /// Die Geschwindigkeit der Pickaxe
        /// </summary>
        public abstract float SwingSpeed { get; }

        /// <summary>
        /// Name des zu verwendenden Bilds
        /// </summary>
        public abstract string ImageName { get; }

        public override void Awake()
        {
            base.Awake();
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._spriteRenderer.sortingLayerName = "Player_Tool";

            this._spriteRenderer.sprite = Resources.Load<Sprite>(this.ImageName);
            this._itemAnimator.speed = this.SwingSpeed;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if (Input.GetButton("Fire1") && ApplicationModel.CurrentSelectedForegroundBlock != null)
            {
                Destroy(ApplicationModel.CurrentSelectedForegroundBlock.gameObject);// ApplicationModel.CurrentSelectedForegroundBlock.DestroyComplete();
            }
        }

        public void LoadAnimator()
        {
            this.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<AnimatorOverrideController>("Animations/Pickaxe");
        }
    }
}
