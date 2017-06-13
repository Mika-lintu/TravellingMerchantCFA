using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class InventoryManager : MonoBehaviour {
    private string connectingString;
    public GameObject itemObjects;
    [HideInInspector]
    public GameObject selectedObject;
    //WorldList worldListScript;

    private void Awake()
    {
        connectingString = "URI=file:" + Application.dataPath + "/Database/InventoryDB.sqlite";
        //worldListScript = GetComponent<WorldList>();
        //GetAllItems();

    }
    public void AddItem(int id)
    {
        if (CheckItem(id))
        {
            AddQuant(id);
        }
        else
        {
            AddNewItem(id);
        }
    }
    public void RemoveItem(int id)
    {
        if(QuantityCheck(id) <= 1)
        {
            RemoveFromInventory(id);
        }
        else
        {
            RemoveQuant(id);
        }
    }

    public List<Item> GetAllItems()
    {
        int id = 0;
        List<Item> tempList = new List<Item>();

        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM PlayerInventory";

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log("ID: " + reader.GetInt32(0) + " Sprite: " + reader.GetInt32(1));
                        id = reader.GetInt32(1);
                        //sprite = reader.GetInt32(1);
                        tempList.Add(new Item(id, GetItemSprite(id)));
                        //worldListScript.FindDeactivated(id, sprite);
                    }
                   // worldListScript.FindDeactivated(id, sprite);
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return tempList;
    }
    private void GetItem(int id)
    {
        //itemData.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Item WHERE ItemID = " + id;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.Log("ID: " + reader.GetInt32(0) + " Name: " + reader.GetString(2));

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }

    private int GetItemSprite(int id)
    {
        int spriteNr = 0;
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Item WHERE ItemID = " + id;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spriteNr = reader.GetInt32(1);
                    }
                    dbConnection.Close();
                    reader.Close();
                }
                return spriteNr;
            }
        }
    }

    private void GetItemByRarity(int rarity)
    {
        //itemData.Clear();

        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM Item WHERE Rarity = " + rarity;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    List<string> tempList = new List<string>();
   
                    while (reader.Read())
                    {
                        string tempString = "ID: " + reader.GetInt32(0) + " Name: " + reader.GetString(2);
                        tempList.Add(tempString);
                        //Debug.Log("ID: " + reader.GetInt32(0) + " Name: " + reader.GetString(2));

                    }
                    dbConnection.Close();
                    reader.Close();
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        Debug.Log(tempList[i]);
                    }
                }
            }
        }
    }
    private bool CheckItem(int id)
    {
        bool found = false;
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM PlayerInventory WHERE ItemID = " + id;

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        found = true;

                    }
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return found;
    }
    private void AddQuant(int itemid)
    {

        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = ("UPDATE PlayerInventory Set Quantity = Quantity + 1 WHERE ItemID =" + itemid);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }
           
        }
    }
    private void AddNewItem(int newItemID)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();
            
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = ("INSERT INTO PlayerInventory(ItemID, Quantity) VALUES (" + newItemID + ",1)");
                string sqlQuery2 = ("SELECT Sprite FROM Item WHERE ItemID = " + newItemID);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();

                dbCmd.CommandText = sqlQuery2;
                using (IDataReader reader = dbCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Debug.Log("ID: " + reader.GetInt32(0) + " Name: " + reader.GetString(2));
                    }
                    //worldListScript.FindDeactivated(newItemID, sprite);
                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
    }
    private int QuantityCheck(int id)
    {
        int getQuant = 0;
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = ("SELECT Quantity FROM PlayerInventory WHERE ItemID = " + id);

                dbCmd.CommandText = sqlQuery;

                using (IDataReader reader = dbCmd.ExecuteReader()) {

                    while (reader.Read())
                    {
                        getQuant = reader.GetInt32(0);
                        Debug.Log(getQuant);
                    }

                    dbConnection.Close();
                    reader.Close();
                }
            }
        }
        return getQuant;
        
    }
    private void RemoveQuant(int id)
    {
        
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = ("UPDATE PlayerInventory Set Quantity = Quantity - 1 WHERE ItemID =" + id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }

        }
    }
    private void RemoveFromInventory(int id)
    {
        using (IDbConnection dbConnection = new SqliteConnection(connectingString))
        {
            dbConnection.Open();

            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = ("DELETE FROM PlayerInventory WHERE ItemID =" + id);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();
            }

        }
    }  
}