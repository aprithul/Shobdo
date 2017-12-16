using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoringLayer : MonoBehaviour {

    public bool change_on_start;
    public int layer;
    public Renderer renderer;

	// Use this for initialization
	void Start () {
        if (change_on_start)
            change_layer(this.layer);
	}

    void change_layer(int target_layer)
    {
        renderer.sortingOrder = target_layer;
    }
	
}
