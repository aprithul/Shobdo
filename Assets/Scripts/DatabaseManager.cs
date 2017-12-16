using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class DatabaseManager:MonoBehaviour {

    string connection_string;

    // Use this for initialization
    public void Start()
    {
#if UNITY_ANDROID
        string filepath = Application.persistentDataPath + "/dictionary.db";
        if (!File.Exists(filepath))
        {
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "dictionary.db");  // this is the path to your StreamingAssets in android

            Debug.Log("will load");
            while (!loadDB.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

            // then save to Application.persistentDataPath
            Debug.Log("loaded");
            File.WriteAllBytes(filepath, loadDB.bytes);
            Debug.Log("written");
        }

        this.connection_string = "URI=file:" + filepath;

#endif
        
#if UNITY_EDITOR
        this.connection_string = "URI=file:" + Application.dataPath + "/DB/" + "dictionary.db";
#endif
    }

    public string get_word (string my_word) {

        using (IDbConnection connection = new SqliteConnection(connection_string))
        {
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from words where word = ?";// + my_word+"'";// 'করা'";
                command.Parameters.Add(new SqliteParameter("param1", my_word));
                using (IDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
        }

        return null;
	}

    public bool has_word(string my_word)
    {
        using (IDbConnection connection = new SqliteConnection(connection_string))
        {
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from words where word = ?";// + my_word+"'";// 'করা'";
                command.Parameters.Add(new SqliteParameter("param1", my_word));
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (my_word == reader.GetString(0))
                            return true;
                    }
                }
            }
        }

        return false;
    }

    public int get_character_frequency(string character)
    {
        using (IDbConnection connection = new SqliteConnection(connection_string))
        {
            connection.Open();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select sum(char_count) from (SELECT (LENGTH(word) - LENGTH(replace(word,?,''))) as char_count from words)";// + my_word+"'";// 'করা'";
                command.Parameters.Add(new SqliteParameter("param1", character));
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
        }
        return -1;
    }




	
}
