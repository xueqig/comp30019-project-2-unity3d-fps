using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour {


    public Button Button_Play;                           
    public Button Button_Next;                          
    public Button Button_Pre;                          
    public static int Level_n = 1;                       
    public GameObject[] Level_img = new GameObject[3];  

    public Button OverBtn;

    // Start is called before the first frame update
    void Start()
    {
        Button_Play.onClick.AddListener(Button_PlayClickListener);         
        Button_Next.onClick.AddListener(Button_NextClickListener);            
        Button_Pre.onClick.AddListener(Button_PreClickListener);             
        OverBtn.onClick.AddListener(OverBtnLis);                              

        for (int D = 0; D < 3; D++)
        {
            Level_img[D].SetActive(false);
        }

        Level_img[Level_n - 1].SetActive(true);                              
    }

    void Button_PlayClickListener()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(Level_n + 2);

        if (Level_n == 1)
        {
            GameObject temp = GameObject.Find("Dontdestroy");
            temp.GetComponent<VariablesSaver>().level = Level_n;
            SceneManager.LoadScene(2);
        }
        else if (Level_n == 2)
        {
            GameObject temp = GameObject.Find("Dontdestroy");
            temp.GetComponent<VariablesSaver>().level = Level_n;
            SceneManager.LoadScene(2);
        }
        else if (Level_n == 3)
        {
            GameObject temp = GameObject.Find("Dontdestroy");
            temp.GetComponent<VariablesSaver>().level = Level_n;
            SceneManager.LoadScene(2);
        }

    }

 
    void Button_NextClickListener()
    {
        if (Level_n < 3)
        {
            Level_n += 1;

            CloseLeven();

            Level_img[Level_n - 1].SetActive(true);
        }
        else
        {
            Level_n = 1;

            CloseLeven();


            Level_img[Level_n - 1].SetActive(true);
        }
    }

   
    void Button_PreClickListener()
    {
        if (Level_n == 1)
        {
            Level_n = 3;

            CloseLeven();

            Level_img[Level_n - 1].SetActive(true);
        }
        else
        {
            Level_n -= 1;

            CloseLeven();

            Level_img[Level_n - 1].SetActive(true);
        }
    }

    void CloseLeven()
    {
        for (int n = 0; n < Level_img.Length; n++)
        {
            Level_img[n].SetActive(false);
        }
    }


    void OverBtnLis()
    {
         SceneManager.LoadScene(0);

    }

}
