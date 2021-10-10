using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public float hp = 100f;
    public GameObject bleedingEffect;
    private bool die = false;

    private float destroytimer = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(hp);
    }

    public void BeingAttacked(float damage)
    {
        hp -= damage;
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
            die = true;
        }
        if(die)
        {
            destroytimer -= Time.deltaTime;
        }
        if (destroytimer<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
