using UnityEngine;
using System.Collections;
using System;

namespace RisingTerra.Assets.MyAssets
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class TileRenderer : MonoBehaviour
    {

        public Sprite sprite;

        // Use this for initialization
        void Start()
        {
            try
            {
                var sprite = Resources.Load<Sprite>("Earth_1");
                this.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}