using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   public float GetHoriz(int num) {
       return Input.GetAxis("Horiz_" + num);
   }

   public float GetVert(int num) {
       return Input.GetAxis("Vert_" + num);
   }

   public bool GetA(int num) {
       return Input.GetButtonUp("Abutton_" + num);
   }

   public bool GetB(int num) {
       return Input.GetButtonUp("Bbutton_" + num);
   }

   public bool GetStart(int num) {
       return Input.GetButtonUp("Sbutton_" + num);
   }

    public bool GetR() {
        return Input.GetKeyDown(KeyCode.R);
    }

    public bool GetM() {
        return Input.GetKeyDown(KeyCode.M);
    }
}
