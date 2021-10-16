using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour {


    /// <summary>
    /// 胜利界面设置
    /// </summary>
    public Button GameAgainButton;//重新开始按钮
    public Button GameOverButton;//游戏结束按钮

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameAgainButton.onClick.AddListener(GameAgainButtonClickListener);      //添加重新开始跳转游戏按钮事件
        GameOverButton.onClick.AddListener(GameOverClickListener);              //添加结束游戏按钮事件
    }


    void GameAgainButtonClickListener()
    {
        SceneManager.LoadScene(2);
    }

    //退出游戏
    void GameOverClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

}
