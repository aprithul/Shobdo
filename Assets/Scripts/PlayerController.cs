using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Language;
using System;

public class PlayerController : MonoBehaviour {

    public static EventHandler<EventArgs> on_blocks_reset;
    
    private List<CharacterBlock> current_word;
    private UnicodeAnsiHandler unicode_ansi_handler;
    public GameObject current_word_go;
    public TextMesh current_word_text;
    public Animator current_word_animator;
    public Animator board_animator;
    public TextMesh debug_mesh;
    public DatabaseManager player_db_manager;
    public ScoreController score_controller;

    private char[] vowels = {'v', 'w', 'z', ',', '…', '†', 'ˆ', 'Š', '‚','x' };
    private List<string> created_words = new List<string>();

    public void Awake()
    {
        CharacterBlock.onBlockEvent += onBlockSelected;
        ButtonController.on_button_click_event += on_button_clicked;

        //on_blocks_reset = null;
        // initialize

        //print(('ছ'.ToString() + 'ো'.ToString()));
    }

    // Use this for initialization
    public void Start()
    {
        //player_db_manager = new DatabaseManager("dictionary.db");

        // subscribe to onSelectBlockEvent

        created_words.Clear();
        current_word_go.SetActive(true);
        print("activated");
        current_word_text.text = "";
        current_word = new List<CharacterBlock>();
        unicode_ansi_handler = new UnicodeAnsiHandler();
        unicode_ansi_handler.prepare_ansi_to_unicode_dict();
    }


    private void onBlockSelected(object o, CharacterBlockEventArgs ea)
    {
        if (CharacterBlock.selected_block_stack.Count > 0 && ea.index == CharacterBlock.selected_block_stack.Peek().index)
        {
            current_word.Add(ea.character_block);
        }
        else
        {
            current_word.Remove(ea.character_block);
        }

        update_word();
    }

    private void on_button_clicked(object o, ButtonClickEventArgs bcea)
    {
        if (bcea.button_name == "word submit")
        {
            var unicode_word = unicode_ansi_handler.ansi_to_unicode(bcea.message);
            debug_mesh.text = unicode_word;
            if (!created_words.Contains(unicode_word) && player_db_manager.has_word(unicode_word))
            {
                score_controller.update_score(CharacterBlock.selected_block_stack.Count);
                current_word_animator.SetTrigger("bounce");
                created_words.Add(unicode_word);
                print("word " + unicode_word + " in db");
            }
            else
            {
                current_word_animator.SetTrigger("vibrate");

            }
            
        }
        else if (bcea.button_name == "vowel select")
        {
            print("vowel added");
            //current_word[current_word.Count - 1].text_mesh.text = add_vowel_to_end(current_word[current_word.Count - 1].text_mesh.text, bcea.message);
            CharacterBlock.last_clicked_on_character.text_mesh.text = modify_vowel(CharacterBlock.last_clicked_on_character.text_mesh.text, bcea.message);
            update_word();
        }
        else if (bcea.button_name == "clear button")
        {
            clear_current_word();
        }

    }

    public void clear_current_word()
    {
        current_word_text.text = "";
        reset_word_stack();
        current_word.Clear();
        update_word();
    }

    private void reset_word_stack()
    {
        CharacterBlock.last_clicked_on_character = null;
        CharacterBlock.selected_block_stack.Clear();
        if(on_blocks_reset!=null)
            on_blocks_reset(this, EventArgs.Empty);
    }

    private void update_word()
    {
        string word = "";
        foreach (CharacterBlock cb in current_word)
        {

            word += cb.text_mesh.text;
        }
        current_word_text.text = word;
        board_animator.SetTrigger("bounce score");
    }

    private string modify_vowel(string character, string vowel)
    {
        character = character.Trim(vowels);

        switch (vowel)
        {
            case "† v":
                character = "†" + character + "v";
                return character;
            case "† Š":
                character = "†" + character + "Š";
                return character;
            case "w":
                character = vowel + character;
                return character;
            case "†":
                character = vowel + character;
                return character;
            case "ˆ":
                character = vowel + character;
                return character;
            default:
                character = character + vowel;
                return character;
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*if (InputHandler.GetMouseButtonUp(0))
        {
			//foreach (CharacterBlock cb in current_word)
			//	cb.reset_state ();
			
            //current_word_text.text = "";
            //current_word.Clear();
        }*/
    }

}
