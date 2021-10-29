using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemenu : MonoBehaviour
{


    public bool IsSetting = false;
    public GameObject SettingPanel;

    public Button BackSettingButton;
    public int NowSceneNum = 2;

    public Slider SoundSlider;
    public AudioSource Sound;

    public GameObject AudioLis;
    public Toggle muteToggle;
    public bool IsToggle = false;
    public Button Overbutton;

    public Button Levelbutton;

    // Start is called before the first frame update
    void Start()
    {
  
        BackSettingButton.onClick.AddListener(BackSettingLis);

        AudioLis.gameObject.GetComponent<AudioSource>().Play();
        muteToggle.isOn = true;

        Overbutton.onClick.AddListener(OverbuttonListe);

        Levelbutton.onClick.AddListener(LevelbuttonLis);
    }

    // Update is called once per frame
    void Update()
    {

        Sound.volume  = SoundSlider.value;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingLis();
        }
        
    }


     public void SettingLis()
    {
        if (IsSetting == false)
        {
            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SettingPanel.SetActive(true);
            IsSetting = true;
        }
        else
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SettingPanel.SetActive(false);
            IsSetting = false;
        }

}

    void BackSettingLis()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(NowSceneNum);
    }


    public void ToggleLis()
    {
        if (IsToggle == true)
        {
            Sound.enabled = true;
            IsToggle = false;
        }
        else
        {
            Sound.enabled = false;
            IsToggle = true;
        }
    }

    public void OverbuttonListe()
    {
         SceneManager.LoadScene(0);
    }

    void LevelbuttonLis()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(5);
    }

}


