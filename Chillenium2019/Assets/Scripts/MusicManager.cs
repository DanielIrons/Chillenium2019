using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource ThemeSong;
    public AudioSource AndrewsSong;

    public void PlayTheme() {
        StopAllSongs();
        ThemeSong.Play();
    }

    public void PlayAndrews() {
        StopAllSongs();
        AndrewsSong.Play();
    }

    private void StopAllSongs() {
        ThemeSong.Stop();
        AndrewsSong.Stop();
    }
}
