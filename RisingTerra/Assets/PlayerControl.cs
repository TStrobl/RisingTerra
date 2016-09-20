using UnityEngine;
using System.Collections;

namespace RisingTerra.Assets
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class PlayerControl : MonoBehaviour
    {
        private Rigidbody2D _rigiBody;

        private Animator _hero;

        private BoxCollider2D _collider;

        private Animator _toolAnimator;

        /// <summary>
        /// Die maximale Höhe, die der Charakter springen kann
        /// </summary>
        private int _maxVerticalForce;

        /// <summary>
        /// blickt der Charakter nach rechts
        /// </summary>
        private bool _isFacingRight;

        /// <summary>
        /// Aktuell ablaufende Animation
        /// </summary>
        private string _currentAnimation;

        /// <summary>
        /// Linke Maustaste wird gehalten
        /// </summary>
        private bool _isTriggeringLeftMouse;

        void Awake()
        {
            this._maxVerticalForce = 20;
            this._hero = this.GetComponent<Animator>();
            this._hero.Play("Hero_Idle_Left");
            this._rigiBody = this.GetComponent<Rigidbody2D>();
            this._collider = this.GetComponent<BoxCollider2D>();
            this._toolAnimator = GetComponentsInChildren<Animator>()[1];
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0 && this.IsGrounded())
            {
                this._rigiBody.velocity += Vector2.up * this._maxVerticalForce;
            }

            if (Input.GetButtonUp("Vertical") && Input.GetAxis("Vertical") > 0)
            {
                if (this._rigiBody.velocity.y > 0)
                {
                    this._rigiBody.velocity -= new Vector2(0, this._rigiBody.velocity.y);
                }
            }

            if (Input.GetButton("Horizontal"))
            {
                var axis = Input.GetAxis("Horizontal");

                if (axis > 0)
                {
                    if (this._rigiBody.velocity.x < 10)
                    {
                        this._rigiBody.velocity += Vector2.right;
                    }
                    this._isFacingRight = true;
                }
                else
                {
                    if (this._rigiBody.velocity.x > -10)
                    {
                        this._rigiBody.velocity += Vector2.left;
                    }
                    this._isFacingRight = false;
                }
            }
            if (Input.GetButtonUp("Horizontal"))
            {
                this._rigiBody.velocity -= new Vector2(this._rigiBody.velocity.x, 0);
            }

            this.SetAnimation();

            if (Input.GetButton("Fire1"))
            {
                this._isTriggeringLeftMouse = true;
            }
            else
            {
                this._isTriggeringLeftMouse = false;
            }
        }

        void FixedUpdate()
        {

        }

        void SetAnimation()
        {
            var isWalking = this._rigiBody.velocity.x != 0;

            string nextAnimation = string.Empty;
            string nextToolAnimation = string.Empty;
            if (isWalking)
            {
                if (this._isFacingRight)
                {
                    nextToolAnimation = "Tool_Walk_Right";
                    nextAnimation = "Hero_Walk_Right";
                }
                else
                {
                    nextToolAnimation = "Tool_Walk_Left";
                    nextAnimation = "Hero_Walk_Left";
                }
            }
            else
            {
                if (this._isFacingRight)
                {
                    nextToolAnimation = "Tool_Idle_Right";
                    nextAnimation = "Hero_Idle_Right";
                }
                else
                {
                    nextToolAnimation = "Tool_Idle_Left";
                    nextAnimation = "Hero_Idle_Left";
                }
            }

            if (this._isTriggeringLeftMouse)
            {
                if (this._isFacingRight)
                {
                }
                else
                {
                    nextToolAnimation = "Swing_Tool_Left";
                }
                this._toolAnimator.GetComponent<Animation>().wrapMode = WrapMode.Loop;
            }
            this._toolAnimator.Play(nextToolAnimation);

            if (nextAnimation != this._currentAnimation)
            {
                this._hero.enabled = false; //Aktuelle Animation abbrechen und nächste setzen
                this._hero.Play(nextAnimation);
                this._currentAnimation = nextAnimation;
                this._hero.enabled = true;
            }
        }

        void OnCollisionEnter2D(Collision2D coll)
        {
        }

        void OnCollisionExit2D(Collision2D coll)
        {
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(this.transform.position, Vector2.down, this._collider.bounds.extents.y + 0.1f, 1 << LayerMask.NameToLayer("Foreground"));
        }
    }
}
