using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnergy : MonoBehaviour
{
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
        other.gameObject.GetComponent<HealSound>().PlayHealSound();
        other.gameObject.GetComponent<PlayerSprintAndCrouch>().AddStamina(staminaGet);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
}
