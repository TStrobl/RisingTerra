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

        protected override void Awake()
        {
            base.Awake();
            this._mainCamera = FindObjectOfType<Camera>();
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.BlocksHorizontal, this.BlocksVertical);
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
        public void AddBlock(BaseBlock block)
        {
            block.transform.SetParent(this.transform);
            var pos = new Vector2(block.RelativePosX, (block.RelativePosY * (-1)));
            block.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}