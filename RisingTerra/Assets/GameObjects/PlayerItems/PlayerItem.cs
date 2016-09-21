using RisingTerra.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.PlayerItems
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class PlayerItem : MonoBehaviour
    {
        /// <summary>
        /// Das Script des Spielers
        /// </summary>
        protected PlayerControl player;

        /// <summary>
        /// Der Animator, der das Item steuert
        /// </summary>
        protected Animator _itemAnimator;

        public virtual void Awake()
        {
            this.player = this.GetComponentInParent<PlayerControl>();
            this.GetComponent<SpriteRenderer>().sortingLayerName = "Player_Tool";
            this._itemAnimator = this.GetComponent<Animator>();
        }

        public virtual void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                if (this.player.IsFacingRight)
                {

                }
                else
                {
                    this._itemAnimator.Play("Swing_Left");
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                this._itemAnimator.Play("Idle");
            }
        }
    }
}
