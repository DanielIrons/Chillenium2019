using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour {
    private bool use;
    private MaterialPropertyBlock props;
    private Renderer _renderer;
    // Start is called before the first frame update
    void Start() {
        use = false;


        props = new MaterialPropertyBlock();
        props.SetFloat("_on",0.0f);
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (use) {
            _renderer.GetPropertyBlock(props);
            props.SetFloat("_on", 1.0f);
            _renderer.SetPropertyBlock(props);
        }
        else {
            _renderer.GetPropertyBlock(props);
            props.SetFloat("_on", 0.0f);
            _renderer.SetPropertyBlock(props);
        }
       
        use = false;
    }
    public void inRange() {
        use = true;
    }

    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            inRange();
        }
    }
}
