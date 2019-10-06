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
                    cur.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = Enum.GetNames(typeof(Job))[ev.getType()];
                }
            }
            getNext = !getNext;
        }
    }

    // Update is called once per frame
    static void showNew()
    {
        getNext = true;
    }
}
