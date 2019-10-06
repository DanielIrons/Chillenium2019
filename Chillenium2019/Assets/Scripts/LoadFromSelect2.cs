using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
using UnityEngine.SceneManagement;

public class LoadFromSelect2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().currentSong = 2;
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
