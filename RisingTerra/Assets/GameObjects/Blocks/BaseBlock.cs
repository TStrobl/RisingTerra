using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.Blocks
{
    public class BaseBlock : MonoBehaviour
    {
        /// <summary>
        /// Die absolute Position x des Blocks im Biom
        /// </summary>
        public ushort PosX { get; set; }

        /// <summary>
        /// Die absolute Position Y des Blocks im BIOM
        /// </summary>
        public ushort PosY { get; set; }

        /// <summary>
        /// Die relative Position X des Blocks im gerenderten Bereich des Bioms
        /// </summary>
        public ushort RelativePosX { get; set; }

        /// <summary>
        /// Die relative Position Y des Blocks im gerenderten Bereich des Bioms
        /// </summary>
        public ushort RelativePosY { get; set; }

        /// <summary>
        /// Ist der Block in Reichweite des Spielers
        /// </summary>
        protected bool _isInRange;

        /// <summary>
        /// Constructor
        /// </summary>
        protected virtual void Awake()
        {

        }

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Start()
        {

        }

        /// <summary>
        /// Update
        /// </summary>
        protected virtual void Update()
        {

        }


        /// <summary>
        /// Wenn eine Collider triggert
        /// </summary>
        /// <param name="coll">Der Collider, der es auslöst</param>
        /// <returns></returns>
        protected virtual void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                this._isInRange = true;
            }
        }

        /// <summary>
        /// trigger entfernt sich
        /// </summary>
        /// <param name="coll"></param>
        protected virtual void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Player")
            {
                this._isInRange = false;
            }
        }
    }
}
