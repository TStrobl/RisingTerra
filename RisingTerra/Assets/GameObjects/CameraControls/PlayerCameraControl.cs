using RisingTerra.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameObjects.CameraControls
{
    public class PlayerCameraControl : MonoBehaviour
    {
        /// <summary>
        /// Die aktuelle Position des Spielers
        /// </summary>
        private Transform _player;

        /// <summary>
        /// Maximale Horizontale Akzeptanz, bis sich die Camera bewegt
        /// </summary>
        private float _maxHorizontalAcceptance = 5;

        /// <summary>
        /// Maximale Horizontale Akzeptanz, bis sich die Camera bewegt
        /// </summary>
        private float _maxVerticalAcceptance = 10;

        /// <summary>
        /// Der Offset
        /// </summary>
        private Vector3 _offset;

        /// <summary>
        /// Hintergrundbild1
        /// </summary>
        private Transform _background1;

        /// <summary>
        /// Hintergrundbild2
        /// </summary>
        private Transform _background2;

        /// <summary>
        /// Aktueller Hintergrund
        /// </summary>
        private Transform _currentBackground;

        /// <summary>
        /// Größe des Hintergrunds in Einheiten
        /// </summary>
        private Vector2 _backgroundSize;

        void Awake()
        {
            this._player = FindObjectOfType<PlayerControl>().transform;
            this._background1 = UnityEngine.GameObject.FindGameObjectWithTag("BackgroundImage1").transform;
            this._background2 = UnityEngine.GameObject.FindGameObjectWithTag("BackgroundImage2").transform;
            this._currentBackground = this._background1;
            this._backgroundSize = this._background1.GetComponent<Renderer>().bounds.size;
        }

        void Start()
        {
            this._offset = this.transform.position - this._player.position;
        }

        void LateUpdate()
        {
            this._offset = this.transform.position - this._player.position;
            //this.transform.position = new Vector3(this._player.position.x, this._player.position.y, -30);
            if (this._offset.x > this._maxHorizontalAcceptance)
            {
                this.transform.position += Vector3.left * (this._offset.x - this._maxHorizontalAcceptance);

                if (!this._background2.GetComponent<Renderer>().isVisible)
                {
                    var nextBgPosx = this._background1.position.x - this._backgroundSize.x;
                    this._background2.position = new Vector2(nextBgPosx, this._background2.position.y);
                }
            }
            else if (this._offset.x < (-1) * this._maxHorizontalAcceptance)
            {
                this.transform.position += Vector3.right * ((this._offset.x + this._maxHorizontalAcceptance) * -1);

                if (!this._background2.GetComponent<Renderer>().isVisible)
                {
                    var nextBgPosx = this._background1.position.x + this._backgroundSize.x;
                    this._background2.position = new Vector2(nextBgPosx, this._background2.position.y);
                }
            }
        }
    }
}
