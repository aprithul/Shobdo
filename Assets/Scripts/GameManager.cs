using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public ScoreController score_controller;
    public GameObject end_menu;
    public Text score_text;
    //InputHandler input_handler = new InputHandler();

    public void Awake()
    {
        TimeController.on_time_over += game_over;
    }

    public void Start()
    {
        score_text.text = "0000";
    }


    void game_over(object o, EventArgs e)
    {
        print("game_over");
        if(InputHandler.on_input_disabled!=null)
            InputHandler.on_input_disabled(this, EventArgs.Empty);
        end_menu.SetActive(true);
        score_text.text = @"Avcbvi   †¯‹vi 
" + score_controller.main_score.text;

    }


    void Update()
    {
    }


}
