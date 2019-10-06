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
    //Checks if the player is using an interactive
    private bool isActing = false;
    //Keeps track of button presses
    private bool[] buttons = {false, false};
    private GameObject consist;
    // bool that disables player movement
    private bool isDisabled = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        animator = GetComponent<Animator>();
        lastMove = movement.south;
    }
    
    private void Update () {
        if (!isDisabled) {
            Move(); // Player Movement 
            Buttons(); // Player Input
        }
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

        // player animations:
        HandleAnimations(horizontal, vertical);

        if(isActing && consist.GetComponent<MainSpotlight>() != null){
            consist.GetComponent<MainSpotlight>().rotate(horizontal);
            
        }
        else{
            this.transform.position += new Vector3(horizontal, vertical, 0) * playerSpeed;
            
            if(isActing && consist.GetComponent<Light_Toggle>()!=null){
                consist.GetComponent<Light_Toggle>().warp(this.transform.position);
                if (buttons[0] == true) {
                    consist.GetComponent<Light_Toggle>().toggle();
                }
            }
        }
        if(buttons[1] == true){
                isActing = false;
                consist = null;
            }
    }

    void HandleAnimations(float h, float v) {
        if (h == 0 || v == 0 && !isIdle) {
            isIdle = true;
            if (lastMove == movement.side) {
                animator.Play("idle-side");
            }
            else if (lastMove == movement.north) {
                animator.Play("idle.north");
            }

        }
    }

    void Buttons() {
        // A button input
        if (inputManager.GetA(playerNumber)) {
            Debug.Log("player "+ playerNumber +" pressed A\n");
            buttons[0] = true;
        }else
        {
            buttons[0] = false;
        }
        // B button input
        if (inputManager.GetB(playerNumber)) {
            Debug.Log("player "+ playerNumber +" pressed B\n");
            buttons[1] = true;
        }else
        {
            buttons[1] = false;
        }
    }

    void Flip(bool b) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = b;
    }

    public void SetPlayerNum(int num) {
        playerNumber = num;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(isActing) return;
        GameObject cur = col.gameObject;
        if(cur.GetComponent<Fireworks>() != null && buttons[0]){
            cur.GetComponent<Fireworks>().BlastStart();
        }
        if(cur.GetComponent<TermScript>() != null && buttons[0]){
            cur.GetComponent<TermScript>().updateJobs();
        }
        if((cur.GetComponent<MainSpotlight>() != null ||  cur.GetComponent<Light_Toggle>() != null) && buttons[0]){
            isActing = true;
            consist = cur;
        }
        if((cur.GetComponent<Proj_Button>() != null && buttons[0])) {
            cur.GetComponent<Proj_Button>().use();
        }
    }

    public void DisablePlayer() {
        isDisabled = true;
    }

    public void ReEnablePlayer() {
        isDisabled = false;
    }
}
