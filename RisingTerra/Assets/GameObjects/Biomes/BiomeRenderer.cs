using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Assets.GameObjects.Blocks;

namespace Assets.GameObjects.Biomes
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    [RequireComponent(typeof(RectTransform))]
    public class BiomeRenderer : UIBehaviour
    {
        /// <summary>
        /// Anzahl der Blöcke in der Horizontalen
        /// </summary>
        public int BlocksHorizontal;

        /// <summary>
        /// Anzahl der Blöcke in der Vertikale
        /// </summary>
        public int BlocksVertical;

        /// <summary>
        /// Die Kamera
        /// </summary>
        private Camera _mainCamera;

        /// <summary>
        /// Höchster Punkt in der Mitte
        /// </summary>
        private Vector2 _highestMiddlePoint;

        /// <summary>
        /// Die momentan sichtbaren Blöcke des Renderers
        /// </summary>
        private BaseBlock[,] _visibleForegroundBlocks;

        protected override void Awake()
        {
            base.Awake();
            this._mainCamera = FindObjectOfType<Camera>();
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.BlocksHorizontal, this.BlocksVertical);

            this._visibleForegroundBlocks = new ForegroundBlock[ApplicationModel.VisibleBlocksHorizontal, ApplicationModel.VisibleBlocksVertical];
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.BlocksHorizontal, this.BlocksVertical);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Fügt einen Block abhängig von seiner Position hinzu
        /// </summary>
        /// <param name="block">Der hinzuzufügende Block</param>
        public void AddBlock(BaseBlock block, ushort column, ushort row)
        {
            block.transform.SetParent(this.transform);
            block.GetComponent<RectTransform>().position = block.WorldPosition;


            this._visibleForegroundBlocks[column, row] = block;
        }

        internal void SetInitialPlayerPosition()
        {
            var middleX = ApplicationModel.VisibleBlocksHorizontal / 2;
            for (int i = 0; i < ApplicationModel.VisibleBlocksVertical; i++)
            {
                if (this._visibleForegroundBlocks[middleX, i] != null)
                {
                    var middleHighestBlock = this._visibleForegroundBlocks[middleX, i];
                    var localPos = middleHighestBlock.transform.position;
                    ApplicationModel.Player.transform.localPosition = new Vector3(localPos.x + 0.5f, localPos.y + 2, 10);
                    break;
                }
            }
        }
    }
}