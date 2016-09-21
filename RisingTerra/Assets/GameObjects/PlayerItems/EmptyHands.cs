using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.PlayerItems
{
    public class EmptyHands : PlayerItem, IPlayerItem
    {
        /// <summary>
        /// Der Renderer für das Bild
        /// </summary>
        private SpriteRenderer _spriteRenderer;
    
        public Enums.ItemClass ItemClass
        {
            get
            {
                return Enums.ItemClass.Nothing;
            }
        }

        public Enums.ItemType ItemType
        {
            get
            {
                return Enums.ItemType.Nothing;
            }
        }

        public void LoadAnimator()
        {
        }

        public override void Awake()
        {
            base.Awake();
            this._spriteRenderer = this.GetComponent<SpriteRenderer>();
            this._spriteRenderer.sprite = null;
        }
    }
}
