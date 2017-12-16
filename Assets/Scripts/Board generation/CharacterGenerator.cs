using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Data;

public class CharacterGenerator{

    private string[] all_characters = { // vowels
                                        "A", "Av","B", "C", "D",
                                        "E", "F", "G", "H", "I",
                                        "J",

                                        // consonants
                                        "K", "L", "M", "N", "O",
                                        "P", "Q", "R", "S", "T",
                                        "U", "V", "W", "X", "Y",
                                        "Z", "_", "`", "a", "b",
                                        "c", "d", "e", "f", "g",
                                        "h", "i", "j", "k", "l",
                                        "m", "n", "r", "o", "p",
                                        "q"
                                      };
    private string[] distribution = new string[16] ;


    public string[] get_random_distribution()
    {
        for(int i = 0; i<distribution.Length; i++)
        {
            distribution[i] = all_characters[Random.Range(0, all_characters.Length)];
        }

        return distribution;
    }

    public List<CharacterFrequency> get_character_frequency(DatabaseManager db)
    {

        List<CharacterFrequency> char_freq = new List<CharacterFrequency>();
        //DatabaseManager db = new DatabaseManager("dictionary.db");
        //var sr = new StreamReader(Application.dataPath + "/DB/bornomala.txt");
        //var fileContents = sr.ReadToEnd();
        //sr.Close();
        var fileContents = @"অ A 5321
আ Av 2325
ই B 1171
ঈ C 84
উ D 1662
ঊ E 102
ঋ F 88
এ G 709
ঐ H 43
ও I 640
ঔ J 33
ক K 17756
খ L 2368
গ M 6569
ঘ N 1108
ঙ O 1334
চ P 5122
ছ Q 1506
জ R 5360
ঝ S 476
ঞ T 994
ট U 4034
ঠ V 986
ড W 1031
ঢ X 178
ণ Y 3570
ত Z 19710
থ _ 2048
দ ` 9345
ধ a 4408
ন b 19879
প c 12526
ফ d 1109
ব e 16163
ভ f 3969
ম g 12049
য h 6429
র i 26882
ল j 9839
শ k 5971
ষ l 5057
স m 11462
হ n 4619
ড় o 2085
ঢ় p 117
য় q 5289
ৎ r 799
ং s 1426
ঃ t 412
ঁ u 1628
া v
ি w
ী x
ু z
ূ ‚
ৃ …
ে †
ৈ ˆ
ো †v
ৌ †Š";
        string[] lines = fileContents.Split('\n');
        int consonant_count = 50;
        foreach (string line in lines)
        {
            //line.Trim(' ');
            var line_parts = line.Split(' ');
            if (line_parts.Length == 3)
            {
                //line_parts[1] = line_parts[1].Substring(0, line_parts[1].Length - 1);

                char_freq.Add(new CharacterFrequency(line_parts[0], line_parts[1], int.Parse(line_parts[2])));//  db.get_character_frequency(line_parts[0])));
            }

            consonant_count--;
            if (consonant_count == 0)
                break;
        }
        char_freq.Sort((x, y) => y.count.CompareTo(x.count));
        return char_freq;
    }
}
