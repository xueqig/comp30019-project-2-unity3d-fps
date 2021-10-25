using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene_ : MonoBehaviour
{

    public Button PlayButton;
    public Button InstructionsButton;
    public Button Overbutton;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(PlayButtonLis);
        InstructionsButton.onClick.AddListener(InstructionsButtonLis);
        Overbutton.onClick.AddListener(OverbuttonListe);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void PlayButtonLis()
    {
        SceneManager.LoadScene(5);
    }

    public void InstructionsButtonLis()
    {
        SceneManager.LoadScene(1);
    }

    public void OverbuttonListe()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

}
