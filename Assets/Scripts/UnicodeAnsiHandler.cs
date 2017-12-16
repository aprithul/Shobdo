using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.IO;

namespace Language
{
    public class UnicodeAnsiHandler : MonoBehaviour
    {
        public TextMesh k, t;

        public Dictionary<string, string> ansi_unicode_dict;
        public List<char> unicode_characters = new List<char>();

        public void prepare_ansi_to_unicode_dict()
        {
            ansi_unicode_dict = new Dictionary<string, string>();
            //var sr = new StreamReader(Application.dataPath + "/DB/bornomala.txt");
            //var fileContents = sr.ReadToEnd();
            //sr.Close();

            var fileContents = @"অ A
আ Av
ই B
ঈ C
উ D
ঊ E
ঋ F
এ G
ঐ H
ও I
ঔ J
ক K
খ L
গ M
ঘ N
ঙ O
চ P
ছ Q
জ R
ঝ S
ঞ T
ট U
ঠ V
ড W
ঢ X
ণ Y
ত Z
থ _
দ `
ধ a
ন b
প c
ফ d
ব e
ভ f
ম g
য h
র i
ল j
শ k
ষ l
স m
হ n
ড় o
ঢ় p
য় q
ৎ r
ং s
ঃ t
ঁ u
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
            foreach (string line in lines)
            {
                //line.Trim(' ');
                var line_parts = line.Split(' ');
                if (line_parts.Length == 2)
                {
                   
                    line_parts[1] = line_parts[1].Substring(0, line_parts[1].Length - 1);
                    //print(line_parts[1].Length + " " + line_parts[1] + " " + line_parts[0]);

                    if (!ansi_unicode_dict.ContainsKey(line_parts[1]))
                        //Debug.Log("already ocntains: "+line_parts[1] + " " + line_parts[0]);
                    //else
                        ansi_unicode_dict.Add(line_parts[1], line_parts[0]);
                }
            }

        }



        public string ansi_to_unicode(string ansi_word)
        {
            string unicode_word = "";
            for (int i = 0; i < ansi_word.Length; i++ )
            {
                // special case
               
                if(ansi_word[i] == '†' && ansi_word.Length > i + 2 && ( ansi_word[i+2] == 'v'))
                {
                    unicode_word += ansi_unicode_dict[ansi_word[i + 1].ToString()] + 'ো'.ToString();
                    i += 2;
                }
                else if (ansi_word[i] == '†' && ansi_word.Length > i + 2 && ansi_word[i + 2] == 'Š')
                {
                    unicode_word += ansi_unicode_dict[ansi_word[i + 1].ToString()] + 'ৌ'.ToString();
                    i += 2;
                }
                else if (ansi_word[i] == 'w' || ansi_word[i] == '†' || ansi_word[i] == 'ˆ')
                {
                    unicode_word += ansi_unicode_dict[ansi_word[i + 1].ToString()] + ansi_unicode_dict[ansi_word[i].ToString()];
                    i++;
                }
                else
                {
                    if (!ansi_unicode_dict.ContainsKey(ansi_word[i].ToString()))
                        Debug.Log(ansi_word[i].ToString());
                    else
                        unicode_word += ansi_unicode_dict[ansi_word[i].ToString()];
                }
            }
            return unicode_word;
        }

        void Start()
        {
            prepare_ansi_to_unicode_dict();
        }

    }

}