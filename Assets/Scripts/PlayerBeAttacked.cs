using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeAttacked : MonoBehaviour
{
    // Start is called before the first frame update
    private float hp = 100;
    public AudioClip cutSound;
    public GameObject gameover;
    void Start()
    {
        
    }

    public void beingAttacked(float damage)
    {
        hp -= damage;
        GetComponent<AudioSource>().PlayOneShot(cutSound,0.5f);
        if (hp <= 0){
            hp = 0;
            gameover.SetActive(true);
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerState>().enabled = false;
            GameObject.Find("GameController").GetComponent<GameController>().enabled = false;
            GameObject.Find("FPS_Character").GetComponent<WeaponController>().enabled = false;
            Invoke("popmenu",1f);
        }
        this.GetComponent<PlayerState>().Health_Change(hp);
    }

    private void popmenu(){
        GameObject temp = GameObject.Find("Canvas");
        temp.GetComponent<Gamemenu>().SettingLis();
    }


    public void getHealed(float heal)
    {
        hp += heal;
        if (hp > 100)
            hp = 100;
        this.GetComponent<PlayerState>().Health_Change(hp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
