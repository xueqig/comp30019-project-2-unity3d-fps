using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float hp = 100f;
    public GameObject bleedingEffect;
    private bool die = false;
    private string type = "normal";
    public int score = 10;

    private GameObject player;

    public GameObject healPackage;
    public GameObject staminaPackage;
    public GameObject weaponUpgradePackage;
    public GameObject recoverPackage;
    private float destroytimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (hp > 100f)
        {
            type = "strong";
        }
    }

    public void BeingAttacked(float damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
        GetComponent<Idle>().SetEngaging();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0 && die==false)
        {
            this.GetComponent<EnemyApproaching>().stop();
            this.GetComponent<EnemyApproaching>().enabled = false;
            this.GetComponent<EnemyAttack>().enabled = false;
            this.GetComponent<Idle>().enabled = false;
            this.GetComponent<Animator> ().Play ("Death1");

            player.GetComponent<PlayerState>().Score_Change(score);

            System.Random random = new System.Random();
            if (type.Equals("strong"))
            {
                if (GameObject.Find("FPS_Character").GetComponentInChildren<WeaponController>().IsUpgraded())
                {
                    Instantiate(this.recoverPackage, this.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(this.weaponUpgradePackage, this.transform.position, Quaternion.identity);
                }
            }
            else
            {
                int randomDrop = random.Next(0, 10);
                if (randomDrop < 6)
                    Instantiate(this.healPackage, this.transform.position, Quaternion.identity);
                else if(randomDrop<9)
                    Instantiate(this.staminaPackage, this.transform.position, Quaternion.identity);
            }
            die = true;
        }
        if(die)
        {
            destroytimer -= Time.deltaTime;
        }
        if (destroytimer<=0)
        {
            if (type.Equals("strong"))
            {
                GameObject.Find("CreateStrongerEnemy").GetComponent<RandomGenerate>().deathEvent();
            }
            else
            {
                GameObject.Find("CreateEnemy").GetComponent<RandomGenerate>().deathEvent();
            }
            Destroy(this.gameObject);
        }
    }
}
