using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public float weaponPowerUpgrade = 1.25f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("FPS_Character").GetComponent<WeaponController>().SetUpgraded(true);
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
            GameObject.Find("FPS_Character").GetComponent<WeaponController>().Upgrade(weaponPowerUpgrade);
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
}
