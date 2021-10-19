using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemenu : MonoBehaviour
{

    public Button SettingButton;
    public bool IsSetting = false;
    public GameObject SettingPanel;

    public Button BackSettingButton;

    public Slider SoundSlider;
    public AudioSource Sound;

    public GameObject AudioLis;
    public Toggle muteToggle;
    public bool IsToggle = false;
    public Button Overbutton;


    // Start is called before the first frame update
    void Start()
    {
        SettingButton.onClick.AddListener(SettingLis);
        BackSettingButton.onClick.AddListener(BackSettingLis);

        AudioLis.gameObject.GetComponent<AudioSource>().Play();
        muteToggle.isOn = false;

        Overbutton.onClick.AddListener(OverbuttonListe);
    }

    // Update is called once per frame
    void Update()
    {

        Sound.volume  = SoundSlider.value;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SettingLis();
        }
        
    }


     public void SettingLis()
    {
        if (IsSetting == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SettingPanel.SetActive(true);
            IsSetting = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SettingPanel.SetActive(false);
            IsSetting = false;
        }

}

    void BackSettingLis()
    {
        SceneManager.LoadScene(2);
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }
}


