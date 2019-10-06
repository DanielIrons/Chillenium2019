using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movement {
    north, south, side
}

public class PlayerController : MonoBehaviour
{
    private int playerNumber;
    public float playerSpeed = 10; //speed player moves
    private Rigidbody2D rb;
    private InputManager inputManager;
    private Animator animator;
    private movement lastMove;
    private bool isIdle = false;
    private bool isMove = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        lastMove = movement.south;
    }
    
    private void Update () {
        Move(); // Player Movement 
        Buttons(); // Player Input
    }
    
    void Move() {
        float horizontal = inputManager.GetHoriz(playerNumber);
        float vertical = inputManager.GetVert(playerNumber);

        // flip player depending on horiz movement
        if (horizontal > 0) {
            Flip(true);
        }
        else if (horizontal < 0) {
            Flip(false);
        }

        //HandleAnimations(horizontal, vertical);

        this.transform.position += new Vector3(horizontal, vertical, 0) * playerSpeed;
    }

    void HandleAnimations(float h, float v) {
     if (h == 0 && v == 0 && !isIdle) {
           isIdle = true;
           isMove = false;
           if (lastMove == movement.side) {animator.Play("idle-side"); }
           else if (lastMove == movement.north) { animator.Play("idle-north"); }
           else if (lastMove == movement.south) { animator.Play("idle-south"); }
       }

       else if (h != 0 || v != 0 && !isMove) {
           isMove = true;
           isIdle = false;
           if (v > 0 && lastMove != movement.north || lastMove != movement.side) {
                animator.Play("walk-north");
                lastMove = movement.north;
           }
           else if (v < 0 && lastMove != movement.south || lastMove != movement.side) {
                animator.Play("walk-south");
                lastMove = movement.south;
           }
           else if (h != 0 && lastMove != movement.side) {
                animator.Play("walk-side");
                lastMove = movement.side;
            }        
        }
    }

    void Buttons() {
        // A button input
        if (inputManager.GetA(playerNumber)) {
            Debug.Log("player "+ playerNumber +" pressed A\n");
        }
        // B button input
        if (inputManager.GetB(playerNumber)) {
            Debug.Log("player "+ playerNumber +" pressed B\n");
        }
    }

    void Flip(bool b) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = b;
    }

    public void SetPlayerNum(int num) {
        playerNumber = num;
    }
}
