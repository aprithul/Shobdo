using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour {

    public static EventHandler<EventArgs> on_time_over;

    public float max_time;
    public TextMesh time;
    private float time_in_sec;
    private bool is_game_over;

	// Use this for initialization
	public void Start () {
        //on_time_over = null;
        is_game_over = false;
        time_in_sec = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (is_game_over)
            return;

        time_in_sec += Time.deltaTime;
        var remaining = (max_time - time_in_sec);
        if ((int)remaining <= 0)
        {
            if (TimeController.on_time_over != null)
                TimeController.on_time_over(this, EventArgs.Empty);
            time.text = "0:00";
            is_game_over = true;

        }
        else
            time.text = ((int)(remaining / 60)).ToString() + ":" + ((int)(remaining % 60)).ToString();
	}
}
