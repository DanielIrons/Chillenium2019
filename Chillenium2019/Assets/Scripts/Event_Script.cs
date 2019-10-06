using UnityEngine;
using System.Collections;
using System;
using System.Timers;
using System.Diagnostics;
using System.Collections.Generic;

//enums for the different events
public enum Job { L1Off, L1On, L2Off, L2On, L1P1, L1P2, L2P1, L2P2, MainLightL, MainLightC, MainLightR, Fireworks };

//The general Event Handler
public class Event_Script : MonoBehaviour{
    public static List<Game_Event> eventList = new List<Game_Event>();
    private System.Timers.Timer spawnTimer;

    //Total jobs on queue
    public static int jobsLeft = 0;
    //Jobs that have gone over but haven't been done.
    public static int jobsDrain = 0;

    private static bool newJob = false;

    //UI updating list to be returned
    public static List<Event_Brief> jobsBrief() {
        List<Event_Brief> toRet = new List<Event_Brief>();

        foreach (Game_Event g in eventList) {
            UnityEngine.Debug.Log(g.getType());
            toRet.Add(new Event_Brief(g.getType(), g.timeRatio()));
        }
        newJob = false;
        return toRet;
    }
    //Other scripts tell us when a job has been completed and we remove all jobs of that type.
    public static void jobDone(int event_type) {
        for(int i = 0; i < jobsLeft; i++) {
            if(eventList[i].getType() == event_type) {
                //Decrement drain if job was overtime
                if (eventList[i].isDrain()) jobsDrain--;
                eventList.RemoveAt(i);
                //correct our positioning
                i = Math.Max(0, i-1);
                jobsLeft--;
                UnityEngine.Debug.Log("Job Type: " + ((Job)event_type).ToString() + " Done");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TimerStart() {
        //Set to spawn events every 5 seconds
        spawnTimer = new System.Timers.Timer(1 * 1000);

        spawnTimer.Elapsed += OnTimedEvent;
        spawnTimer.AutoReset = true;
        spawnTimer.Enabled = true;
    }

    public static bool moreJobs() {
        return newJob;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        //UnityEngine.Debug.Log("SPAWN: EVENT");

        jobsLeft++;
        newJob = true;
        Event_Script.eventList.Add(new Game_Event());
    }
}

// Handles the things that the player needs to do before the time runs out
public class Game_Event {
    private static System.Random rand = new System.Random();
    private int event_type;
    private int timeTotal;
    private System.Timers.Timer timeLeft;
    private Stopwatch timeElapsed;

    public Game_Event() {
        event_type = rand.Next(Enum.GetNames(typeof(Job)).Length);
        switch (event_type) {
            default: timeTotal = 10 * 1000;
                break;
        }
        TimerStart(timeTotal);
        timeElapsed = new Stopwatch();
        timeElapsed.Start();
    }

    public int getType() {
        return event_type;
    }

    private void TimerStart(int tot) {
        //Spawned Events last for 
        timeLeft = new System.Timers.Timer(tot);

        timeLeft.Elapsed += OnTimedEvent;
        timeLeft.AutoReset = false;
        timeLeft.Enabled = true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        //UnityEngine.Debug.Log("EVENT: TIME UP");
        Event_Script.jobsDrain++;
    }

    public float timeRatio() {
        return Math.Min((float)timeElapsed.ElapsedMilliseconds / (float)timeTotal, 1.0f);
    }

    public bool isDrain() {
        if ((float)timeElapsed.ElapsedMilliseconds / (float)timeTotal >= 1.0f) return true;
        return false;
    }
}

// The type that the UI Script will be returned with information on what the unfinished jobs are.
public class Event_Brief {
    private int type;
    private float ratio;
    
    public Event_Brief(int t, float r) {
        type = t;
        ratio = r;
    }

    public int getType() {
        return type;
    }
    public float getRatio() {
        return ratio;
    }
}