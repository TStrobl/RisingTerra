using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    private Transform _groundCheck;

    private RaycastHit2D _grounded;

    private Rigidbody2D _rigiBody;

    private Transform _hero;

    // Use this for initialization
    void Start()
    {
        this._groundCheck = transform.FindChild("groundCheck");
        this._rigiBody = this.GetComponent<Rigidbody2D>();
        this._hero = transform.FindChild("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        this._grounded = Physics2D.Linecast(transform.position, this._groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButton("Horizontal") && _grounded)
        {
            var axis = Input.GetAxis("Horizontal");

            if (axis > 0)
            {
                this._rigiBody.velocity = Vector2.right * 2;

                var stateInfo = this._hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
                this._hero.GetComponent<Animator>().enabled = true;
                this._hero.GetComponent<SpriteRenderer>().flipX = true;
                this._hero.GetComponent<Animator>().Play("Hero_Walk");
            }
            else
            {
                this._rigiBody.velocity = Vector2.left * 2;
                this._hero.GetComponent<Animator>().enabled = true;
                this._hero.GetComponent<SpriteRenderer>().flipX = false;
                this._hero.GetComponent<Animator>().Play("Hero_Walk");
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            this._hero.GetComponent<Animator>().Play("Hero_Idle");
            //this._hero.GetComponent<Animator>().enabled = false;
        }
    }
}
