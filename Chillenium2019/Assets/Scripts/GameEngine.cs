using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEngine : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Transform playerParent;
    public bool DeveloperVersion = false;

    public float startTimer = 120;
    private float currTime;
    public TextMeshProUGUI timerText;
    
    void Awake() {
        currTime = startTimer;
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (DeveloperVersion) {
            for (int i = 0; i < 4; i++) {
            GameObject player = Instantiate(playerPrefabs[i], 
                                            playerParent.position, 
                                            Quaternion.identity, 
                                            playerParent);
            player.GetComponent<PlayerController>().SetPlayerNum(i + 1); 
            }
        }
        else {
            for (int i = 0; i < 4; i++) {
                        GameObject player = Instantiate(playerPrefabs[gm.GetPlayerChar(i)], 
                                                        playerParent.position, 
                                                        Quaternion.identity, 
                                                        playerParent);
                        player.GetComponent<PlayerController>().SetPlayerNum(i + 1); 
            }
        }
    }

    void Update() {
        currTime -= Time.deltaTime;
        timerText.text = (currTime / 60) + ":" + (currTime % 60);
    }
}
