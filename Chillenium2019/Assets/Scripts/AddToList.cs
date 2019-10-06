using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddToList : MonoBehaviour
{
    public GameObject template;
    public GameObject content;
    private static bool getNext;

    public void add_Click() {
        var copy = Instantiate(template);
        copy.transform.parent = content.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        getNext = false;
    }

    void Update() {
        if (getNext) {
            if (Event_Script.moreJobs()) {
                List<Event_Brief> toAdd = Event_Script.jobsBrief();
                foreach (Transform child in content.transform) {
                    Destroy(child.gameObject);
                }
                foreach (Event_Brief ev in toAdd) {
                    UnityEngine.Debug.Log(ev.getType());
                    GameObject cur = Instantiate(template);
                    cur.transform.parent = content.transform;
                    string message = "";
                    switch (ev.getType()) {
                        case 0:
                            message = "Light 1: Off";
                            break;
                        case 1:
                            message = "Light 1: On";
                            break;
                        case 2:
                            message = "Light 2: Off";
                            break;
                        case 3:
                            message = "Light 2: On";
                            break;
                        case 4:
                            message = "Light 1: Red";
                            break;
                        case 5:
                            message = "Light 1: Blue";
                            break;
                        case 6:
                            message = "Light 2: Red";
                            break;
                        case 7:
                            message = "Light 2: Blue";
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
    }

    // Called from the outside telling us to update the list
    public static void showNew()
    {
        getNext = true;
    }
}
