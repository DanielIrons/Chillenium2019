using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform playerParent;
    public bool DeveloperVersion = false;
    
    void Awake() {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (DeveloperVersion) {
            for (int i = 0; i < 4; i++) {
            GameObject player = Instantiate(playerPrefabs[i], 
                                            transform.position, 
                                            Quaternion.identity, 
                                            playerParent);
            player.GetComponent<PlayerController>().SetPlayerNum(i + 1); 
            }
        }
        else {
            for (int i = 0; i < 4; i++) {
                        GameObject player = Instantiate(playerPrefabs[gm.GetPlayerChar(i)], 
                                                        transform.position, 
                                                        Quaternion.identity, 
                                                        playerParent);
                        player.GetComponent<PlayerController>().SetPlayerNum(i + 1); 
            }
        }
    }
}
