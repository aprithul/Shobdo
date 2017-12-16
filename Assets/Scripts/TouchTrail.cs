using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTrail : MonoBehaviour {
    private TrailRenderer trail_renderer;
    private Rect touch_area;

	// Use this for initialization
	void Awake () {
        trail_renderer = GetComponent<TrailRenderer>();
    }
	
    public void set_touch_zone(Rect area)
    {
        this.touch_area = area;
    }

	// Update is called once per frame
	void Update () {


       /* if (InputHandler.GetMouseButton(0))
        {
            trail_renderer.enabled = true;
            Vector3 pos = Input.mousePosition;
            pos.z = 1f;
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
        else
            trail_renderer.enabled = false;
*/	}
}
