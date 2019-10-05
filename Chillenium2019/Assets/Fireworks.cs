using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;
using System.Diagnostics;

public class Fireworks : MonoBehaviour
{
    [Range(1, 2)]
    public int fireworkID;

    //Timer for how long fireworks go off for
    private System.Timers.Timer spawnTimer;
    public static bool boom;

    // Start is called before the first frame update
    void Start() {
        boom = false;
    }

    void BlastStart() {
        //Set to spawn events every 5 seconds
        spawnTimer = new System.Timers.Timer(1 * 1000);
        spawnTimer.Elapsed += OnTimedEvent;
        spawnTimer.AutoReset = false;
        spawnTimer.Enabled = true;
        GetComponent<ParticleSystem>().Play();
        boom = true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        //UnityEngine.Debug.Log("SPAWN: EVENT");
        boom = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ParticleSystem>().isPlaying) {
            Event_Script.jobDone((int)Job.Fireworks);
        }
        if(!boom && GetComponent<ParticleSystem>().isPlaying) {
            GetComponent<ParticleSystem>().Stop();
        }
    }
}
