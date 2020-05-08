using System;
using UnityEngine;
using Mono.Data.SqliteClient;

public class BBDDconnection : MonoBehaviour
{



    void Start()
    {
        SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/0_BBDD/store.db");
        conexion.Open();
        string query = "SELECT * FROM Player1";
        SqliteCommand cmd = new SqliteCommand(query, conexion);
        SqliteDataReader data = cmd.ExecuteReader();
        while(data.Read())
        {
            
            Debug.Log(Convert.ToString(data[0]));
        }

        conexion.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
