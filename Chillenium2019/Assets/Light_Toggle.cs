using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Light_Toggle : MonoBehaviour {
    public Sprite[] lightSprites;
    bool on;
    private float centerAngle;
    private float relAngle;
    
    [Range(1,2)]
    public int lightID;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, centerAngle);
        relAngle = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Not a jump rotation, adds to current value and restricts within a range.
    public void rotate(float dir) {
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        relAngle += dir;
        relAngle = Mathf.Clamp(relAngle, -40, 40);
        transform.rotation = Quaternion.Euler(0, 0, centerAngle + relAngle);
    }

    public void move(float x, float y) {
        transform.Translate(x, y, 0, Space.World);
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        rotate(0.0f);
    }

    public void toggle() {
        on = !on;
        if (on) {
            switch (lightID) {
                case 1:
                    Event_Script.jobDone((int)Job.L1On);
                    break;
                case 2:
                    Event_Script.jobDone((int)Job.L2On);
                    break;
            }
        }
        else {
            switch (lightID) {
                case 1:
                    Event_Script.jobDone((int)Job.L1Off);
                    break;
                case 2:
                    Event_Script.jobDone((int)Job.L2Off);
                    break;
            }
        }

        //Change the art asset
        this.GetComponent<SpriteRenderer>().sprite = lightSprites[Convert.ToInt32(on)];
    }
}
