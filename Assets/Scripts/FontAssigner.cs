using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FontAssigner : MonoBehaviour {
    public Font font;

	// Use this for initialization
	void Start () {
        assign();

    }
	
    private void assign()
    {
        Text[] text_scripts = GetComponentsInChildren<Text>();
        foreach (Text t in text_scripts)
            t.font = font;
    }
}
