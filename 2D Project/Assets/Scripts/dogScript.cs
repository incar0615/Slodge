using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogScript : MonoBehaviour {

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    bool useDash;
    Vector2 moveDir;
	// Use this for initialization
	void Start () {
        animator.SetBool("isWalk", true);
        useDash = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Walk(Vector2.right);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Walk(Vector2.left);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Walk(Vector2.up);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Walk(Vector2.down);
        }
        else
        {
            StopMove();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            useDash = true;

            if(moveDir != Vector2.zero)
            {
                animator.SetBool("isRun", true);    
            }

        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            useDash = false;
            animator.SetBool("isRun", false); 
        }

        gameObject.transform.Translate(moveDir * Time.deltaTime * (useDash ? 7.0f : 5.0f) );


	}

    void Walk(Vector2 _moveDir)
    {
        if (moveDir == _moveDir) return;

        if (_moveDir == Vector2.right) spriteRenderer.flipX = true;
        else if(_moveDir == Vector2.left) spriteRenderer.flipX = false;

        animator.SetBool("isWalk", true);

        moveDir = _moveDir;

    }

    void StopMove()
    {
        animator.SetBool("isWalk", false);

        moveDir = Vector2.zero;

    }
}
