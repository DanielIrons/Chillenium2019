using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjChange : MonoBehaviour {
    public Sprite[] projSprites;
    private int curState;
    // Start is called before the first frame update
    void Start()
    {
        curState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Event_Script.jobDone((int)Job.Proj1 + curState);
    }

    public void swap() {
        curState = (curState + 1) % 4;
        this.GetComponent<SpriteRenderer>().sprite = projSprites[curState];
    }
}
