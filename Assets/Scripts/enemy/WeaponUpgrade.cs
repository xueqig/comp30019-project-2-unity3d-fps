using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public int weaponPowerUpgrade = 5;
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
        other.gameObject.GetComponent<HealSound>().PlayHealSound();
        GameObject.Find("FPS_Character").GetComponent<WeaponController>().Upgrade(weaponPowerUpgrade);
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update
}
