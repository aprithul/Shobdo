using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    public TextMesh main_score;
    public TextMesh small_score;
    public int multiplier;

    public void Start()
    {
        main_score.text = "0000";
    }

    public void update_score(int len)
    {
        main_score.text = (int.Parse(main_score.text) + (len * multiplier)).ToString();
        small_score.text = "+"+(len * multiplier).ToString();
        GetComponent<Animator>().SetTrigger("small score");
    }
}