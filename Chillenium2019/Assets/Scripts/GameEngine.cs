using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform playerParent;
    
    void Awake() {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < 4; i++) {
            GameObject player = Instantiate(playerPrefabs[gm.GetPlayerChar(i)], 
                                            transform.position, 
                                            Quaternion.identity, 
                                            playerParent);
            player.GetComponent<PlayerController>().SetPlayerNum(i + 1); 
        }
    }
}
