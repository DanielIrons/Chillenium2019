using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpotlight : MonoBehaviour {
    private float centerAngle;
    private float relAngle;
    private int curState;
    // Start is called before the first frame update
    void Start()
    {
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        transform.rotation = Quaternion.Euler(0, 0, centerAngle);
        relAngle = 0.0f;
        curState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Event_Script.jobDone((int)Job.MainLightC + getState());
    }

    public int getState() {
        return curState;
    }

    //Not a jump rotation, adds to current value and restricts within a range.
    public void rotate(float dir) {
        centerAngle = (Mathf.Atan2(transform.position.y, transform.position.x) * 180 / Mathf.PI) - 90;
        relAngle += dir;        
        relAngle = Mathf.Clamp(relAngle, -40, 40);

        if (relAngle < -20) curState = -1;
        else if (relAngle > 20) curState = 1;
        else curState = 0;

        transform.rotation = Quaternion.Euler(0, 0, centerAngle + relAngle);
    }
}
