using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float hp = 100f;
    public GameObject bleedingEffect;
    private bool die = false;
    public GameObject healPackage;
    private float destroytimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void BeingAttacked(float damage)
    {
        hp -= damage;
        this.GetComponent<Idle>().SetEngaging();
        if (hp < 0)
            hp = 0;
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
            System.Random random = new System.Random();
            int randomDrop = random.Next(0, 10);
            if (randomDrop < 4)
                Instantiate(this.healPackage, this.transform.position, Quaternion.identity);
            die = true;
        }
        if(die)
        {
            destroytimer -= Time.deltaTime;
        }
        if (destroytimer<=0)
        {
            GameObject.Find("CreateEnemy").GetComponent<RandomGenerate>().deathEvent();
            Destroy(this.gameObject);
        }
    }
}
