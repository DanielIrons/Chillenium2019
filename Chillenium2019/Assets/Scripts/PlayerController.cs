using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public float playerSpeed = 10; //speed player moves
   private Rigidbody2D rb;

   private void Awake()
   {
       rb = GetComponent<Rigidbody2D>();
   }
   
   private void Update () 
   {
   
       Move(); // Player Movement 
   }
   
   void Move()
   {
       float horizontal = Input.GetAxis("Horizontal");
       float vertical = Input.GetAxis("Vertical");
       rb.velocity = new Vector2(horizontal * playerSpeed, vertical * playerSpeed);
   }
}
