using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadMusicManager : MonoBehaviour
{
    public void loadSong(int num)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().currentSong = num;
        SceneManager.LoadScene("GameScene");
    }
}
