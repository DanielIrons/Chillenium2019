using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    private InputManager inputManager;
    private bool[] playerConnected;
    private bool[] playerReady;
    private bool[] characterPicked;
    private bool[] playerWait;
    private int[] playerCurrChar;

    public Transform[] characterNodes;
    public GameObject[] playerArrows;
    public Image[] playerImgs;
    public Color notSelectedColor;
    public Color selectedColor;
    public Color[] readyColor;
    public float arrowSpeed;
    public float playerDelay;

    private void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        playerConnected = new bool[4];
        for (int i = 0; i < 4; i++) {
            playerConnected[i] = false;
        }
        playerReady = new bool[4];
        for (int i = 0; i < 4; i++) {
            playerReady[i] = false;
        }
        characterPicked = new bool[4];
        for (int i = 0; i < 4; i++) {
            characterPicked[i] = false;
        }
        playerCurrChar = new int[4];
        for (int i = 0; i < 4; i++) {
            playerCurrChar[i] = 0;
        }
        playerWait = new bool[4];
        for (int i = 0; i < 4; i++) {
            playerWait[i] = false;
        }

        // deactivate all player arrows
        for (int i = 0; i < 4; i++) {
            playerArrows[i].SetActive(false);
        }

        // set player icon to dark
        for (int i = 0; i < 4; i++) {
            playerImgs[i].color = notSelectedColor;
        }
    }

    void Update()
    {
        // chack to see if all players are ready
        int readyCount = 0;
        for (int i = 0; i < 4; i++) {
            if (playerReady[i]) {
                readyCount++;
            }
        }
        if (readyCount == 4) {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            for (int i = 0; i < 4; i++) {
                gm.SetPlayerChar(i, playerCurrChar[i]);
            }
            SceneManager.LoadScene("GameScene");
        }

        // check all controllers for input
        for (int i = 0; i < 4; i++) {
            // connect player
            if (inputManager.GetA(i + 1) && !playerConnected[i] && !playerWait[i]) {
                ConnectPlayer(i);
                StartCoroutine("DelayPlayer", i);
                Debug.Log("Player " + i + " connected");
            }
            // disconnect player
            else if (inputManager.GetB(i + 1) && playerConnected[i] && !playerReady[i] && !playerWait[i]) {
                DisconnectPlayer(i);
                StartCoroutine("DelayPlayer", i);
                Debug.Log("Player " + i + " disconnected");
            }
            // ready player
            else if (inputManager.GetA(i + 1) && playerConnected[i] && !playerReady[i] && !playerWait[i]) {
                ReadyPlayer(i);
                StartCoroutine("DelayPlayer", i);
                Debug.Log("Player " + i + " ready");
            }
            // unready player
            else if (inputManager.GetB(i + 1) && playerConnected[i] && playerReady[i] && !playerWait[i]) {
                UnreadyPlayer(i);
                StartCoroutine("DelayPlayer", i);
                Debug.Log("Player " + i + " unready");
            }
            // player choose character
            else if (inputManager.GetHoriz(i + 1) != 0 && playerConnected[i] && !playerReady[i] && !playerWait[i]) {
                MovePlayerArrow(inputManager.GetHoriz(i + 1), i);
                StartCoroutine("DelayPlayer", i);
                Debug.Log("Player " + i + " moved");
            }


            // move player arrows to appropriate locations
            if (playerConnected[i]) {
                float step = arrowSpeed * Time.deltaTime;
                            playerArrows[i].transform.position = Vector3.MoveTowards(
                            playerArrows[i].transform.position, 
                            characterNodes[playerCurrChar[i]].position, 
                            step);
            }  
        }
    }

    private IEnumerator DelayPlayer(int num)
    {
        playerWait[num] = true;
        yield return new WaitForSeconds(playerDelay);
        playerWait[num] = false;

    }

    int ReturnNextAvailableCharacter() {
        for (int i = 0; i < 4; i++) {
            if (!characterPicked[i]) {
                return i;
            }
        }
        return 0;
    }

    void ConnectPlayer(int num) {
        playerConnected[num] = true;
        int character = ReturnNextAvailableCharacter();
        playerCurrChar[num] = character;
        playerArrows[num].transform.position = characterNodes[character].position;
        playerImgs[playerCurrChar[num]].color = selectedColor;
        playerArrows[num].SetActive(true);
    }

    void DisconnectPlayer(int num) {
        playerConnected[num] = false;
        playerImgs[playerCurrChar[num]].color = notSelectedColor;
        playerArrows[num].SetActive(false);

    }

    void MovePlayerArrow(float input, int num) {
        playerImgs[playerCurrChar[num]].color = notSelectedColor;
        // left
        int x = playerCurrChar[num];
        if (input < 0) {
            do  {
                x--;
                if (x < 0) {
                    x = 3;
                }
            } while (characterPicked[x]); 
        }
        // right
        else if (input > 0) {
           do  {
                x++;
                if (x > 3) {
                    x = 0;
                }
            } while (characterPicked[x]);
        }

        playerCurrChar[num] = x;
        playerImgs[playerCurrChar[num]].color = selectedColor;
    }

    void ReadyPlayer(int num) {
        playerReady[num] = true;
        playerArrows[num].SetActive(false);
        playerImgs[playerCurrChar[num]].color = readyColor[playerCurrChar[num]];
        if (characterPicked[playerCurrChar[num]] == false)
            characterPicked[playerCurrChar[num]] = true;
    }

    void UnreadyPlayer(int num) {
        playerReady[num] = false;
        playerArrows[num].SetActive(true);
        playerImgs[playerCurrChar[num]].color = selectedColor;
        characterPicked[playerCurrChar[num]] = false;
     }    
}
