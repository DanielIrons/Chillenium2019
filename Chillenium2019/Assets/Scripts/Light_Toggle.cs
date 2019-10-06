using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Light_Toggle : MonoBehaviour {
    public Sprite[] lightSprites;
    bool on;
    private float centerAngle;
    private float relAngle;
    private int curState;
    
    [Range(1,2)]
    public int lightID;
    // Start is called before the first frame update
    void Start()
    {
        on = false;
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, centerAngle);
        relAngle = 0.0f;
        curState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Redundant code to stop the generation of events that we're doing.
        switch (lightID) {
            case 1:
                switch (curState) {
                    case 1:
                        Event_Script.jobDone((int)Job.L1P1);
                        break;
                    case 2:
                        Event_Script.jobDone((int)Job.L1P2);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (curState) {
                    case 1:
                        Event_Script.jobDone((int)Job.L2P1);
                        break;
                    case 2:
                        Event_Script.jobDone((int)Job.L2P2);
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }

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

    public void warp(Vector3 pos){
        transform.position = pos;
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        rotate(0.0f);
    }

    public void toggle() {
        on = !on;

        //Change the art asset
        //this.GetComponent<SpriteRenderer>().sprite = lightSprites[Convert.ToInt32(on)];
        transform.GetChild(0).gameObject.SetActive(on);
    }


    //Setup to trigger if we're inside of an area where the light should be placed
    void OnTriggerEnter2D(Collider2D col) {
        LightPlant cur = col.gameObject.GetComponent<LightPlant>();
        if (cur == null) return;

        switch (lightID) {
            case 1:
                switch (cur.spotID) {
                    case 1:
                        curState = 1;
                        break;
                    case 2:
                        curState = 2;
                        break;
                }
                break;
            case 2:
                curState = 2;
                switch (cur.spotID) {
                    case 1:
                        curState = 1;
                        break;
                    case 2:
                        curState = 2;
                        break;
                }
                break;

            default:
                break;
        }
        return;
    }

    void OnTriggerExit2D(Collider2D col) {
        curState = 0;
    }
}
