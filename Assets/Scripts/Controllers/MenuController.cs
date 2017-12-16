using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MenuController : MonoBehaviour {

    public static EventHandler<EventArgs> on_reset_event;

    public PlayerController pc;
    public BoardCreator board_creator;
    public GameManager gm;
    public GameObject panel;
    public TimeController tc;
    public ScoreController sc;

    public GameObject[] objects_to_active;
    public Animator menu_animator;
    public GameObject menu_panel;
    public GameObject vowel_panel;
    public GameObject current_word;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void replay()
    {

        if (MenuController.on_reset_event != null)
            MenuController.on_reset_event(this, EventArgs.Empty);

        CharacterBlock.selected_block_stack.Clear();
        CharacterBlock.last_clicked_on_character = null;
        panel.SetActive(false);
        vowel_panel.SetActive(false);
        current_word.SetActive(false);

        pc.Start();
        board_creator.Start();
        gm.Start();
        tc.Start();
        sc.Start();


    }

    public void menu()
    {
        menu_panel.SetActive(true);
        panel.SetActive(false);
    }
    
    public void go_to_scene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void start_game()
    {
        menu_animator.SetTrigger("fade in");
        menu_panel.SetActive(false);
        foreach (GameObject g in objects_to_active)
            g.SetActive(true);

        replay();
    }

}