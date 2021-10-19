using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour {


    /// <summary>
    /// Victory interface settings
    /// </summary>
    public Button GameAgainButton;//Restart button
    public Button GameOverButton;//Game over button

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

    //exit the game
    void GameOverClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

}
