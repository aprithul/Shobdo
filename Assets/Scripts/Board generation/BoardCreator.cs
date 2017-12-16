using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour {

    public DatabaseManager dbm;
    public CharacterBlock[] board = new CharacterBlock[16];
    private CharacterGenerator character_generator;
    List<CharacterFrequency> characters;

    public void Awake()
    {
    }

    // Use this for initialization
    public void Start()
    {

        // get all the character blocks
        //board = board_root.GetComponentsInChildren<CharacterBlock>();
        foreach (CharacterBlock cb in board)
            cb.gameObject.SetActive(false);

        character_generator = new CharacterGenerator();
        set_up_board();

    }
	
    private void set_up_board()
    {
        characters = character_generator.get_character_frequency(dbm);
        
        int total_char_count = 0;
        foreach (CharacterFrequency cf in characters)
        {
            total_char_count += cf.count;
        }
        foreach (CharacterFrequency cf in characters)
        {
            cf.probability = cf.count /(float) total_char_count;
        }

        //string[] characters = character_generator.get_random_distribution();
        random_block_activation();
        //for(int i=0; i<board.Length; i++)
        {
            
        }
    }

    private void random_block_activation()
    {
        foreach (CharacterBlock cb in board)
        {
            StartCoroutine(activate_block(Random.Range(.5f,1f), cb));
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator activate_block(float time, CharacterBlock block)
    {

        yield return new WaitForSeconds(time);
        block.gameObject.SetActive(true);
        block.Start();
        float probability = Random.Range(0f, 1f);

        for (int j = 0; j < characters.Count; j++)
        {
            probability -= characters[j].probability;
            if (probability <= 0)
            {
                block.text_mesh.text = characters[j].ansi;
                break;
            }
        }
    }
}


public class CharacterFrequency
{
    public string character;
    public string ansi;
    public int count;
    public float probability;

    public CharacterFrequency(string character, string ansi, int frequency)
    {
        this.character = character;
        this.ansi = ansi;
        this.count = frequency;
    }
}