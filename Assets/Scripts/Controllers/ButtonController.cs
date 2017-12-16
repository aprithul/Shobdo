using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour {

    public string button_name;
    public TextMesh label;
    public bool clicked_on_released;
    public bool animate_by_own;
    private InputHandler input_handler;

    public static EventHandler<ButtonClickEventArgs> on_button_click_event;

    public void Awake()
    {
        //on_button_click_event = null;
    }

    // Use this for initialization
    public void Start()
    {
        MenuController.on_reset_event += on_reset;
        on_reset(this, EventArgs.Empty);
    }

    private void on_reset(object o, EventArgs e)
    {
        animate_by_own = false;
        input_handler = new InputHandler();
        InputHandler.on_input_disabled += input_handler.disable_input;
    }


	// Update is called once per frame
	void Update () {
	    // see if this block was clicked on
        if (GetComponent<BoxCollider2D>().bounds.Contains((Vector2)Camera.main.ScreenToWorldPoint(input_handler.GetMousePosition())))
        {
            if (clicked_on_released)
                print("clicked");

            if ((!clicked_on_released && input_handler.GetMouseButtonDown(0)) || (clicked_on_released && input_handler.GetMouseButtonUp(0)))
            {
                if (GetComponent<Animator>() != null && animate_by_own)    
                    GetComponent<Animator>().SetTrigger("bounce");
                if (on_button_click_event != null)
                    on_button_click_event(this, new ButtonClickEventArgs(button_name, label==null ? "":label.text));
            }
        }
	}
}

public class ButtonClickEventArgs : EventArgs
{
    public string button_name;
    public string message;
    public ButtonClickEventArgs(string button_name, string message)
    {
        this.button_name = button_name;
        this.message = message;
    }
}
