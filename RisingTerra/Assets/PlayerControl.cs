using UnityEngine;
using System.Collections;

namespace RisingTerra.Assets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerControl : MonoBehaviour
    {
        private Rigidbody2D _rigiBody;

        private Animator _hero;

        /// <summary>
        /// Ist die Figur auf dem Boden
        /// </summary>
        private bool _grounded;

        private bool _isJumping;

        void Awake()
        {
            this._hero = this.GetComponent<Animator>();
            this._hero.Play("Hero_Idle");
            this._rigiBody = this.GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButton("Horizontal"))
            {
                var axis = Input.GetAxis("Horizontal");

                if (axis > 0)
                {
                    if (this._rigiBody.velocity.x < 10)
                    {
                        this._rigiBody.velocity += Vector2.right;
                    }

                    var stateInfo = this._hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                    this._hero.enabled = true;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                    this._hero.Play("Hero_Walk");
                }
                else
                {
                    if (this._rigiBody.velocity.x > -10)
                    {
                        this._rigiBody.velocity += Vector2.left;
                    }
                    this._hero.enabled = true;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                    this._hero.Play("Hero_Walk");
                }
            }

            if (Input.GetButtonUp("Horizontal"))
            {
                this._hero.Play("Hero_Idle");
                this._rigiBody.velocity -= new Vector2(this._rigiBody.velocity.x, 0);
                //this._hero.GetComponent<Animator>().enabled = false;
            }

            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                this._isJumping = true;
            }

            if (Input.GetButton("Vertical") && this._grounded)
            {
                var axis = Input.GetAxis("Vertical");

                if (axis > 0)
                {
                    if (this._isJumping)
                    {
                        if (this._rigiBody.velocity.y < 25)
                        {
                            this._rigiBody.velocity += Vector2.up * 8;
                        }
                        else
                        {
                            this._isJumping = false;   
                        }
                    }

                    var stateInfo = this._hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                    this._hero.enabled = true;
                    this._hero.Play("Hero_Idle");
                }
            }
            if (Input.GetButtonUp("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                this._isJumping = false;
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "ForegroundBlock")
            {
                this._grounded = true;
            }
        }

        void OnCollisionExit2D(Collision2D coll)
        {
            this._grounded = false;
        }
    }
}
