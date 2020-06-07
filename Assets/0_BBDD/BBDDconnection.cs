using System;
using System.IO;
using UnityEngine;
using Mono.Data.SqliteClient;
using System.Collections.Generic;

public class BBDDconnection : MonoBehaviour
{
    public List<Partida> partidas;// = new List<Partida>();

    private void Awake()
    {
        partidas = new List<Partida>();
    }

    public void DownloadDatabase()
    {
        SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/0_BBDD/store.db");
        conexion.Open();
        string query = "SELECT * FROM score";
        SqliteCommand cmd = new SqliteCommand(query, conexion);
        SqliteDataReader data = cmd.ExecuteReader();
        while (data.Read())
        {
            Partida game = new Partida();
            game.estrellas = Convert.ToInt16(data[0]);
            game.tiempo = Convert.ToString(data[1]);
            partidas.Add(game);
        }
        cmd.ExecuteNonQuery();
        conexion.Close();
    }

    public void UpdateDatabase(int estrellas, string tiempo)
    {
        SqliteConnection conexion = new SqliteConnection("URI=file:" + Application.dataPath + "/0_BBDD/store.db");
        conexion.Open();
        string query = "INSERT INTO score VALUES (" + estrellas + ", '"+ tiempo + "')";
        SqliteCommand cmd = new SqliteCommand(query, conexion);
        cmd.ExecuteNonQuery();
        conexion.Close();
    }


    public void SerializeDataBase2Local()
    {
        Debug.Log(Application.persistentDataPath);
        StreamWriter file = new StreamWriter(Application.dataPath + "/" + "bbddScorelog.txt");
        file.Write("");
        file.Close();
        foreach (Partida partida in partidas)
        {
            string partida2Json = JsonUtility.ToJson(partida);
            file = File.AppendText(Application.dataPath + "/" + "bbddScorelog.txt");
            file.WriteLine(partida2Json);
            file.Close();
        }
    }




}
