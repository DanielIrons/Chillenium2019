using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {
    private bool use;
    // Start is called before the first frame update
    void Start() {
        use = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (use) {
            Shader.SetGlobalFloat("on", 1.0f);
        }
        else {
            Shader.SetGlobalFloat("on", 0.0f);
        }
        use = false;
    }
    public void inRange() {
        use = true;
    }
    public void outRange(){
        use = false;
    }

    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            inRange();
        }
    }
}
