using System.Collections;
using System;
using System.Timers;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddToList : MonoBehaviour
{
    public GameObject template;
    public GameObject content;
    private static bool getNext;
    private System.Timers.Timer lifeOf;
    private static bool KILL;

    public void add_Click() {
        var copy = Instantiate(template);
        copy.transform.parent = content.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        KILL = false;
        getNext = false;
        lifeOf = new System.Timers.Timer(2000 * 1000);
    }

    void TimerStart() {
        //Set to spawn events every 5 seconds
        lifeOf.Stop();
        lifeOf.Close();
        lifeOf = new System.Timers.Timer(25 * 100);

        lifeOf.Elapsed += OnTimedEvent;
        lifeOf.AutoReset = false;
        lifeOf.Enabled = true;
    }

    void clear() {
        foreach (Transform child in content.transform) {
            Destroy(child.gameObject);
        }
        KILL = false;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        //UnityEngine.Debug.Log("SPAWN: EVENT");
        KILL = true;
    }

    void Update() {
        if (getNext) {
            TimerStart();
            if (Event_Script.moreJobs()) {
                List<Event_Brief> toAdd = Event_Script.jobsBrief();
                clear();
                foreach (Event_Brief ev in toAdd) {
                    UnityEngine.Debug.Log(ev.getType());
                    GameObject cur = Instantiate(template);
                    cur.transform.parent = content.transform;
                    string message = "";
                    switch (ev.getType()) {
                        case 0:
                            message = "Green Light: Off";
                            break;
                        case 1:
                            message = "Green Light: On";
                            break;
                        case 2:
                            message = "Pink Light: Off";
                            break;
                        case 3:
                            message = "Pink Light: On";
                            break;
                        case 4:
                            message = "Green Light: Red";
                            break;
                        case 5:
                            message = "Green Light: Blue";
                            break;
                        case 6:
                            message = "Pink Light: Red";
                            break;
                        case 7:
                            message = "Pink Light: Blue";
                            break;
                        case 8:
                            message = "Big Spotlight: Left";
                            break;
                        case 9:
                            message = "Big Spotlight: Center";
                            break;
                        case 10:
                            message = "Big Spotlight: Right";
                            break;
                        case 11:
                            message = "Hit the Fireworks";
                            break;
                        case 12:
                            message = "Fog Machine: Off";
                            break;
                        case 13:
                            message = "Fog Machine: On";
                            break;
                        case 14:
                            message = "Projector: COOL";
                            break;
                        case 15:
                            message = "Projector: STORM";
                            break;
                        case 16:
                            message = "Projector: FIRE";
                            break;
                        case 17:
                            message = "Projector: RAINBOW";
                            break;

                    }
                    cur.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = message;
                }
            }
            getNext = !getNext;
        }
        else if (KILL) {
            clear();
        }
    }

    // Called from the outside telling us to update the list
    public static void showNew()
    {
        getNext = true;
    }
}
