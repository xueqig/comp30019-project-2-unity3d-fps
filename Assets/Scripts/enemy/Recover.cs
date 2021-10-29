using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour
{
    public float heal = 50;
    public float staminaGet = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            other.gameObject.GetComponent<HealSound>().PlayHealSound();
            other.gameObject.GetComponent<PlayerBeAttacked>().getHealed(heal);
            other.gameObject.GetComponent<PlayerSprintAndCrouch>().AddStamina(staminaGet);
            Destroy(this.gameObject);
        }
    }
}
