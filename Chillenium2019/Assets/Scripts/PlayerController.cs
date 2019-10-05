using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNumber;
    public float playerSpeed = 10; //speed player moves
    private Rigidbody2D rb;
    private InputManager inputManager;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }
    
    private void Update () {
        Move(); // Player Movement 
        Buttons(); // Player Input
    }
    
    void Move() {
        float horizontal = inputManager.GetHoriz(playerNumber);
        float vertical = inputManager.GetVert(playerNumber);

        this.transform.position += new Vector3(horizontal, vertical, 0) * playerSpeed;
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
}
