using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SAVEING + LOADING using Binary Formatting
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveData
{
    public float[] playerPosition;
    public float[] playerRotation;

    public SaveData()
    {
        playerPosition = new float[3];
        playerRotation = new float[3];
    }
}

public class GameSaveManager : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    // SAVEING + LOADING using Binary Formatting
    public Transform player;
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.playerPosition = new[] { player.position.x, player.position.y, player.position.z } ;
        data.playerRotation = new[] { player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, player.rotation.eulerAngles.z } ;
        
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
            //Vector3 playerPosition  = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            //Quaternion playerRotation = Quaternion.Euler(data.playerRotation[0], data.playerRotation[1], data.playerRotation[2]);

            var xR = data.playerRotation[0];
            var yR = data.playerRotation[1];
            var zR = data.playerRotation[2];

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            player.rotation = Quaternion.Euler(data.playerRotation[0], data.playerRotation[1], data.playerRotation[2]);
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

    public void SaveButtonPressed()
    {
        SaveGame();
    }

    public void LoadButtonPressed()
    {
        LoadGame();
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
}