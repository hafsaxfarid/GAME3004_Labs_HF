using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
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

    public void SaveButtonPressed()
    {
        SaveGame();
    }

    public void LoadButtonPressed()
    {
        LoadGame();
    }
}