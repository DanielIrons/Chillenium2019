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
        props.SetFloat("on",0.0f);
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        use = false;
        _renderer.GetPropertyBlock(props);
        props.SetFloat("on", 1.0f);
        _renderer.SetPropertyBlock(props);
    }
    public void inRange() {
        use = true;
        props.SetFloat("on", 1.0f);
    }

    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            inRange();
        }
    }
}
