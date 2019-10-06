using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEngine : MonoBehaviour
{
    private InputManager inputManager;
    private int pausedPlayer = 1;
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject EndMenu;

    private bool wait = false;
    private float waitTimer = 0;
    public float waitTime = 0.5f;

    public GameObject[] playerPrefabs;
    public Transform playerParent;
    public bool DeveloperVersion = false;

    public float startTimer = 120;
    private float currTime;
    public TextMeshProUGUI timerText;
    
    void Awake() {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        GameObject.Find("MusicManager").GetComponent<MusicManager>().PlayCurrSong();
        currTime = startTimer;
        pauseMenu.SetActive(false);
        EndMenu.SetActive(false);
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
        if(!isPaused && !wait) {
            currTime -= Time.deltaTime;
            int sec = (int)(currTime % 60);
            string sec_s = sec.ToString();
            if (sec <= 9) {
                sec_s = "0" + sec_s;
            }
            timerText.text = (int)(currTime / 60) + ":" + sec_s;

            if (currTime <= 0) {
                EndGame();
            }
        } 
        else {
            if (inputManager.GetStart(pausedPlayer)) {
                Unpause();
                Delay();
            }
            if (inputManager.GetR()) {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
            if (inputManager.GetM()) {
                SceneManager.LoadScene(0);
            }
        } 

        // check for pause
        for (int i = 0; i < 4; i++) {
            if (inputManager.GetStart(i + 1) && !wait) {
                Pause(i + 1);
                Delay();
            }
        }

        // delay for pause
        if (wait) {
            waitTimer += Time.deltaTime;
            if (waitTimer > waitTime) {
                wait = false;
            }
        }
    }

    public void Pause(int player) {
        Debug.Log("Game paused by player: " + player);
        pauseMenu.SetActive(true);
        pausedPlayer = player;
        isPaused = true;
        DisableAllPlayers();
    }

    public void Unpause() {
        Debug.Log("Game unpaused by player: " + pausedPlayer);
        pauseMenu.SetActive(false);
        isPaused = false;
        ReEnableAllPlayers();
    }

    private void DisableAllPlayers() {
        int players = playerParent.childCount;
        for (int i = 0; i < players; i++)
            playerParent.transform.GetChild(i).GetComponent<PlayerController>().DisablePlayer();
    }

    private void ReEnableAllPlayers() {
        int players = playerParent.childCount;
        for (int i = 0; i < players; i++)
            playerParent.transform.GetChild(i).GetComponent<PlayerController>().ReEnablePlayer();
    }

    private void Delay() {
        wait = true;
        waitTimer = 0;
    }

    private void EndGame() {
        DisableAllPlayers();
        isPaused = true;
        EndMenu.SetActive(true);
    }
}
