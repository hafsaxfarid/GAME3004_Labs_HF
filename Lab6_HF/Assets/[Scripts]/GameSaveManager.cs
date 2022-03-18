using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SAVEING + LOADING using Binary Formatting
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
class SaveData
{
    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
}

public class GameSaveManager : MonoBehaviour
{
    // SAVEING + LOADING using Binary Formatting
    public Transform player;
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.playerPositionX = player.position.x;
        data.playerPositionY = player.position.y;
        data.playerPositionZ = player.position.z;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data saved!");
    }
    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            var x = data.playerPositionX;
            var y = data.playerPositionY;
            var z = data.playerPositionZ;


            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySaveData.dat");
            Debug.Log("Data reset complete!");
        }
        else
        {
            Debug.LogError("No save data to delete.");
        }
    }

    // SAVEING + LOADING using PlayerPrefs
    /*
    public Transform player;

    // serialize player data
    void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.position.z);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }

    // deserialize player data
    void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            var x = PlayerPrefs.GetFloat("PlayerPositionX");
            var y = PlayerPrefs.GetFloat("PlayerPositionY");
            var z = PlayerPrefs.GetFloat("PlayerPositionZ");

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(x, y, z);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            Debug.Log("Game data loaded!");
        }
        else
        {
            Debug.LogError("There is no save data!");
        }
    }

    void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Data reset complete");
    }
*/

    public void SaveButtonPressed()
    {
        SaveGame();
    }

    public void LoadButtonPressed()
    {
        LoadGame();
    }

    
}