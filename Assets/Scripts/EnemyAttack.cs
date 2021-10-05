using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public float radius;
    public GameObject Player;
    public float frequency=1f;
    private float timer;
    private float attackTimer = 0.6f;
    private bool isAttack = false;
    void Start()
    {
        timer = 0;
        this.GetComponent<EnemyApproaching>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            this.GetComponent<Animator> ().Play ("Attack1");
            timer = frequency;
            this.isAttack = true;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (isAttack)
        {
            if (attackTimer <= 0)
            {
                this.isAttack = false;
                if (this.GetComponent<EnemyApproaching>().CalculateDistance() < 1)
                {
                    GameObject.Find("Player").GetComponent<PlayerBeAttacked>().beingAttacked(damage);
                    attackTimer = 0.6f;
                }
                else
                {
                    this.GetComponent<EnemyApproaching>().enabled = true;
                    timer = 0;
                    attackTimer = 0.6f;
                    this.enabled = false;
                }

            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }

    }
}
