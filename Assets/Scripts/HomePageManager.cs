using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // for managing scene transitions
using TMPro;

public class HomePageManager : MonoBehaviour
{
    public TMP_Text tmName;
    public GameObject AboutPanel; // there is no Panel object

    public AudioClip clickSound; // when you want to play an audio clip, there need to be an audio source component.

    public void EnableAboutPanel()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        AboutPanel.gameObject.SetActive(true);
    }

    public void DisableAboutPanel()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        // Be careful on using SetActive could accidentally delete an object instead of just making it invisible.
        AboutPanel.gameObject.SetActive(false); 
    }

    // remember, when methods are public then you are able to use them as event handlers.
    public void GoToGame()
    {
        PlayerPrefs.SetInt("IsAlternateSpot", 1); // 1 being false
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        SceneManager.LoadScene("GameScene");
    }

    public void GoToGameAlternateSpot()
    {
        // store a value in PlayerPrefs letting the game know if the user clicked the alternate start button
        PlayerPrefs.SetInt("IsAlternateSpot", 0); // 0 being true

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        SceneManager.LoadScene("GameScene");
    }

    public void GoToSettings()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().PlayOneShot(clickSound);  // for finding game objects that're tagged.
        SceneManager.LoadScene("SettingsScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            tmName.text = "Welcome " + PlayerPrefs.GetString("UserName");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
