using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandler{

    public static EventHandler<EventArgs> on_input_disabled;
    public bool is_input_enabled;


    public InputHandler()
    {
        is_input_enabled = true;
    }

    public void disable_input(object o, EventArgs e)
    {
        is_input_enabled = false;
    }

    public void enable_input()
    {
        is_input_enabled = true;
    }

    public bool GetMouseButton(int button_id)
    {
        if (is_input_enabled)
            return Input.GetMouseButton(button_id);
        else
            return false;
    }

    public bool GetMouseButtonUp(int button_id)
    {
        if (is_input_enabled)
            return Input.GetMouseButtonUp(button_id);
        else
        {

            return false;
        }
    }

    public bool GetMouseButtonDown(int button_id)
    {
        if (is_input_enabled)
            return Input.GetMouseButtonDown(button_id);
        else
            return false;
    }

	public Vector3 GetMousePosition()
	{
		if (is_input_enabled)
			return Input.mousePosition;
		else
			return Vector3.zero;
	}

    public bool GetBackButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            return true;
        else
            return false;
    }
}
