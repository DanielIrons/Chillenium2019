using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager i = null;
    public int currentSong = 0;
    
    void Awake() {
        if (!i) {
            i = this;
            DontDestroyOnLoad(gameObject);
            First();
        }
        else {
            Destroy(gameObject);
        }
    }
    void First() {
        Players = new int[4];
    }

    private int[] Players;

    public void SetPlayerChar(int controller, int character) {
        Players[controller] = character;
    }

    public int GetPlayerChar(int num) {
        return Players[num];
    }
}
