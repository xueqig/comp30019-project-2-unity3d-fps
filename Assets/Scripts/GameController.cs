using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int timeLimitation = 60;
    public Text timeUI;

    private float spendTime = 0;
    private int timeSum = 60;

    public GameObject gameover; 

    // Start is called before the first frame update
    void Start()
    {
        timeUI.text = timeLimitation.ToString();
        timeSum = timeLimitation;
       
    }

    // Update is called once per frame
    void Update()
    {
        spendTime = spendTime + Time.deltaTime;
        if(spendTime >= 1){
            timeSum = timeSum -1;
            timeUI.text = timeSum.ToString();
            spendTime = 0;
        }

        if(timeSum <= 0){
            timeUI.text = "game over";          
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerState>().enabled = false;
            GameObject.Find("GameController").GetComponent<GameController>().enabled = false;
            GameObject.Find("FPS_Character").GetComponent<WeaponController>().enabled = false;
            gameover.SetActive(true);
            Invoke("popmenu",1f);
        }

        
    }

    private void popmenu(){
        GameObject temp = GameObject.Find("Canvas");
        temp.GetComponent<Gamemenu>().SettingLis();
    }
}
