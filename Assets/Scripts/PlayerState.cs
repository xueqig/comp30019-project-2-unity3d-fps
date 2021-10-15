using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float player_Health = 100f;

    [SerializeField]
    private float player_Stamina = 100f;

    [SerializeField]
    private Image health_State, stamina_State;

    [SerializeField]
    private int score = 0;

    public Text scoreUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Health_Change(float healthValue){
        healthValue = healthValue/player_Health;
        health_State.fillAmount = healthValue;
    }

    public void Stamina_Change(float staminaValue){
        staminaValue = staminaValue/player_Stamina;
        stamina_State.fillAmount = staminaValue;
    }

    public void Score_Change(int scoreValue){
        score = score + scoreValue;
        scoreUI.text = score.ToString();
    }
    

    public float get_player_Health(){
        return player_Health;
    }

    public float get_stamina_Health(){
        return player_Stamina;
    }
}
