using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterBlock : MonoBehaviour {

    //public static EventHandler<CharacterBlockEventArgs> onSelectBlockEvent;
    //public static EventHandler<CharacterBlockEventArgs> onDeSelectBlockEvent;
    public static EventHandler<CharacterBlockEventArgs> onBlockEvent;
    public static Stack<CharacterBlock> selected_block_stack = new Stack<CharacterBlock>();
    public static CharacterBlock last_clicked_on_character;

    public Material on_hover_font_material;
    public float on_hover_scale_multiplier;
    public string index;
    public Sprite on_reachable_sprite;
    public Sprite on_normal_sprite;
    public Sprite on_selected_sprite;
    public Sprite on_latest_selection_sprite;
    public SpriteRenderer shadow;
    public SpriteRenderer board_darkner;
    public GameObject vowel_panel;

    //[HideInInspector]
    public TextMesh text_mesh;

    private float vowel_panel_x_offset = 1f;
    private float vowel_panel_y_offset = 2.1f;
    public bool is_reachable = true;
    private bool is_selected;
    private bool just_selected;
    private Material on_leave_font_material;
    private Vector3 initial_scale;
    private SpriteRenderer block_sprite;
    private LineRenderer line_renderer;
    private Animator block_animator;
    private char[] vowels = { 'v', 'w', 'z', ',', '…', '†', 'ˆ', 'Š', '‚', 'x'};
    private InputHandler input_handler;
    private int touch_up_count = 0;
    private float hold_time = 0f;
    void Awake()
    {
        //onBlockEvent = null;
        //last_clicked_on_character = null;
        block_sprite = GetComponent<SpriteRenderer>();
        line_renderer = GetComponentInChildren<LineRenderer>();
        block_animator = GetComponent<Animator>();
        index = GetComponentInChildren<TextMesh>().gameObject.name;
        CharacterBlock.onBlockEvent += OnBlockInteracted;
        PlayerController.on_blocks_reset += reset;

    }

    // Use this for initialization
    public void Start () {
		//text_mesh = GetComponentInChildren<TextMesh>();
        is_reachable = true;
        is_selected = false;
        just_selected = false;
        input_handler = new InputHandler();
        line_renderer.gameObject.SetActive(false);
        //on_leave_font_material = text_mesh;
        initial_scale = transform.localScale;
        block_sprite.sprite = on_normal_sprite;
        InputHandler.on_input_disabled += input_handler.disable_input;
        //shadow.gameObject.SetActive(false);
    }

    public void reset(object o, EventArgs cbea)
    {
        is_reachable = true;
        is_selected = false;
        just_selected = false;
        line_renderer.gameObject.SetActive(false);
        //shadow.gameObject.SetActive(false);
        block_sprite.sprite = on_normal_sprite;
        text_mesh.text = text_mesh.text.Trim(vowels); 
    }

	// Update is called once per frame
	void Update () {

		// see if this block was clicked on
        if (!vowel_panel.activeInHierarchy && GetComponent<BoxCollider2D>().bounds.Contains((Vector2)Camera.main.ScreenToWorldPoint(input_handler.GetMousePosition())))
        {
            if (input_handler.GetMouseButtonDown(0))
            {
                if (is_selected || is_reachable )
                {
                    //vowel_panel.transform.position = transform.position + new Vector3(Mathf.Sign(transform.position.x) * -1 * vowel_panel_x_offset, Mathf.Sign(transform.position.y) * -1 * vowel_panel_y_offset, 0f);
                    //vowel_panel.SetActive(true);
                    // modify sorting order


                    CharacterBlock.last_clicked_on_character = this;

                    //board_darkner.gameObject.SetActive(true);
                    

                    //var line = vowel_panel.GetComponentInChildren<LineRenderer>();
                    //line.SetPositions(new Vector3[] { transform.position, transform.position + transform.right * Mathf.Sign(transform.position.x) * -1, vowel_panel.transform.position });
                    //line.sortingOrder = 2;
                }

                // selecting an unselected block
                if (!is_selected && is_reachable)
                {
                    if (CharacterBlock.selected_block_stack.Count > 0)
                    {
                        line_renderer.gameObject.SetActive(true);
                        line_renderer.sortingOrder = 1;
                        line_renderer.SetPositions(new Vector3[2] { line_renderer.transform.position, CharacterBlock.selected_block_stack.Peek().line_renderer.transform.position });
                    }

                    just_selected = true;   // transitioning from unselected to selected state with this click down 
                    CharacterBlock.selected_block_stack.Push(this);
                    if (onBlockEvent != null)
                        onBlockEvent(this, new CharacterBlockEventArgs(this, this.index));

                    CharacterBlock.last_clicked_on_character = this;
                }
                else // already selected so this is the second click on this block
                    just_selected = false;
            }
            else if (input_handler.GetMouseButtonUp(0))
            {
                
                if ( selected_block_stack.Count>0 && this.index == selected_block_stack.Peek().index)
                {
                    if (!just_selected)
                    {
                        line_renderer.gameObject.SetActive(false);
                        selected_block_stack.Pop();
                        if (onBlockEvent != null)
                            onBlockEvent(this, new CharacterBlockEventArgs(this, this.index));
                    }
                }
            }
        }

        if(input_handler.GetMouseButton(0) && GetComponent<BoxCollider2D>().bounds.Contains((Vector2)Camera.main.ScreenToWorldPoint(input_handler.GetMousePosition())) && is_selected)
        {
            hold_time+=Time.deltaTime;
            if(hold_time>=0.25f)
            {
                if(!vowel_panel.activeInHierarchy)
                {
                    vowel_panel.transform.position = transform.position + new Vector3(Mathf.Sign(transform.position.x) * -1 * vowel_panel_x_offset, Mathf.Sign(transform.position.y) * -1 * vowel_panel_y_offset, 0f);
                    vowel_panel.SetActive(true);
                                        
                    text_mesh.GetComponent<Renderer>().sortingOrder = 14;
                    block_sprite.sortingOrder = 13;
                    shadow.sortingOrder = 12;
                }

            }
        }

        if (input_handler.GetMouseButtonUp(0))
        {
            hold_time = 0f;
            //print("yes");
            //board_darkner.gameObject.SetActive(false);
            if(vowel_panel.activeInHierarchy)
            {
                if(vowel_panel.GetComponent<BoxCollider2D>().bounds.Contains((Vector2)Camera.main.ScreenToWorldPoint(input_handler.GetMousePosition())) || touch_up_count==1)
                {
                    //vowel_panel.transform.position = transform.position + new Vector3(Mathf.Sign(transform.position.x) * -1 * vowel_panel_x_offset, Mathf.Sign(transform.position.y) * -1 * vowel_panel_y_offset, 0f);
                    vowel_panel.GetComponent<Animator>().SetTrigger("disappear");
                    //vowel_panel.SetActive(false);
                    text_mesh.GetComponent<Renderer>().sortingOrder = 4;
                    block_sprite.sortingOrder = 3;
                    shadow.sortingOrder = 2;
                    touch_up_count = 0;
                }
                else
                    touch_up_count++;
            }
        }
	}

    public void OnBlockInteracted(object o, CharacterBlockEventArgs cbea)
    {
        // this is the block on which event has occured
        if (cbea.index == this.index)
        {


            // block was selected
            if (selected_block_stack.Count > 0 && selected_block_stack.Peek().index == this.index)
            {
                is_selected = true;
                //text_mesh_pro.fontSharedMaterial = on_hover_font_material;
                //transform.localScale = initial_scale * on_hover_scale_multiplier;
                block_sprite.sprite = on_latest_selection_sprite;
                is_reachable = false;
                block_animator.SetTrigger("bounce");
                shadow.gameObject.SetActive(true);
            }
            else
            {
                // block was deselected
                is_selected = false;
                //text_mesh_pro.fontSharedMaterial = on_leave_font_material;
                //transform.localScale = initial_scale;
                is_reachable = true;
                block_animator.SetTrigger("bounce");
                shadow.gameObject.SetActive(false);
                if (selected_block_stack.Count > 0)
                {
                    block_sprite.sprite = on_reachable_sprite;
                }
                else
                {
                    block_sprite.sprite = on_normal_sprite;

                }
            }
        }

        if (CharacterBlock.selected_block_stack.Count > 0)
        {
            if (is_selected)
            {
                is_reachable = false;
                block_sprite.sprite = on_selected_sprite;
                if (CharacterBlock.selected_block_stack.Peek().index == this.index)
                    block_sprite.sprite = on_latest_selection_sprite;
            }
            else 
            {
                string index_to_compare = CharacterBlock.selected_block_stack.Peek().index;

                int row = int.Parse(index_to_compare[0].ToString());
                int col = int.Parse(index_to_compare[1].ToString());

                int row_this = int.Parse(this.index[0].ToString());
                int col_this = int.Parse(this.index[1].ToString());

                int row_dif = Mathf.Abs(row - row_this);
                int col_dif = Mathf.Abs(col - col_this);
                int sum_of_dif = row_dif + col_dif;
                //if(sum_of_dif > 0)
                //{
                if (row_dif <= 1 && col_dif <= 1)
                {
                    is_reachable = true;
                    block_sprite.sprite = on_reachable_sprite;
                }
                else
                {
                    is_reachable = false;
                    block_sprite.sprite = on_normal_sprite;
                }
            }
        }
        else
        {
            is_reachable = true;
            block_sprite.sprite = on_normal_sprite;
        }
    }
}

public class CharacterBlockEventArgs:EventArgs
{
    public CharacterBlock character_block { get; private set; }
    public string index { get; private set; }
    public CharacterBlockEventArgs(CharacterBlock cb, string index)
    {
        this.character_block = cb;
        this.index = index;
    }
}
