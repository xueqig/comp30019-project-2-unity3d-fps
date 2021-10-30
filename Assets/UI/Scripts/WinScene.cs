using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour {


    /// <summary>
    /// win window setting
    /// </summary>
    public Button GameAgainButton;//restart
    public Button GameOverButton;//gamne over 

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameAgainButton.onClick.AddListener(GameAgainButtonClickListener);      //Add restart jump game button event
        GameOverButton.onClick.AddListener(GameOverClickListener);              //Add an end game button event
    }


    void GameAgainButtonClickListener()
    {
        SceneManager.LoadScene(2);
    }

    //exist game
    void GameOverClickListener()
    {
        SceneManager.LoadScene(0);
    }

}
