using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TextMeshProUGUI jobsDoneText;
    public TextMeshProUGUI jobsQueueText;

    void Update()
    {
        jobsDoneText.text = "Jobs Done: " + Event_Script.jobsDone.ToString();
        jobsQueueText.text = "Jobs in Queue: " + Event_Script.jobsLeft.ToString();
    }
}
