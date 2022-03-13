using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsSceneManager : MonoBehaviour
{
    public TMP_InputField inputName;
    public AudioClip clickSound; // when you want to play an audio clip, there need to be an audio source component.
    public GameObject MusicPanel;

    public void SetSceneMusic(string musicChoice)
    {
        Debug.Log(musicChoice);
        switch (musicChoice)
        {
            case "Action":
                PlayerPrefs.SetString("musicChoice", "Action");
                break;
            case "Adventure":
                PlayerPrefs.SetString("musicChoice", "Adventure");
                break;
            case "Sci-Fi":
                PlayerPrefs.SetString("musicChoice", "Sci-Fi");
                break;
        }
        DisableMusicPanel();
    }

    public void EnableMusicPanel()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        MusicPanel.gameObject.SetActive(true);
    }

    public void DisableMusicPanel()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        // Be careful on using SetActive could accidentally delete an object instead of just making it invisible.
        MusicPanel.gameObject.SetActive(false);
    }
    public void ResetHighScore()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        PlayerPrefs.SetInt("HighScore", 0);
    }

    public void GoHome()
    {
        // remember that PlayerPrefs is temporary storage, similar to LocalStorage in the browser to store JSON data.
        PlayerPrefs.SetString("UserName", inputName.text); // UserName is a key to get a piece of data later 
        SceneManager.LoadScene("HomeScene");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
    }

    // Start is called before the first frame update
    void Start()
    {
        // first check to see if the username exists to avoid an empty string input
        if (PlayerPrefs.HasKey("UserName"))
        {
            inputName.text = PlayerPrefs.GetString("UserName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
