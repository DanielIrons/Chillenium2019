using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource ThemeSong;
    public AudioSource TutorialSong;
    public AudioSource AndrewsSong;

    public void PlayTheme() {
        StopAllSongs();
        ThemeSong.Play();
    }

    public void PlayAndrews() {
        StopAllSongs();
        AndrewsSong.Play();
    }

    public void PlayTutorial() {
        StopAllSongs();
        TutorialSong.Play();
    }

    public void PlayCurrSong() {
        int song = GameObject.Find("GameManager").GetComponent<GameManager>().currentSong;
        switch(song) {
            case 0:
                PlayTheme();
                break;
            case 1:
                PlayTutorial();
                break;
            case 2:
                PlayAndrews();
                break;
            default:
                PlayTheme();
                break;
        }
    }

    private void StopAllSongs() {
        ThemeSong.Stop();
        AndrewsSong.Stop();
    }
}
