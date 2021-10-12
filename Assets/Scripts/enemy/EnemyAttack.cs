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
    public float frequency=0.6f;
    private float timer;
    private float attackTimer = 0.25f;
    private bool isAttack = false;
    void Start()
    {
        timer = 0;
        this.GetComponent<EnemyApproaching>().enabled = false;
    }

    // Update is called once per frame

    void Update()
    {
        //Debug.Log(timer);
        if (this.GetComponent<EnemyApproaching>().CalculateDistance() > radius)
        {
            this.isAttack = false;
            
            this.GetComponent<EnemyApproaching>().enabled = true;
            timer = 0;
            attackTimer = 0.25f;
            
        }
        else
        {

            if (timer <= 0)
            {
                this.GetComponent<EnemyApproaching>().enabled = false;
                Animator animator = this.GetComponent<Animator>();
                animator.Play("Attack1");
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
                    GameObject.Find("Player").GetComponent<PlayerBeAttacked>().beingAttacked(damage);
                    attackTimer = 0.25f;
                }
                else
                {
                    attackTimer -= Time.deltaTime;
                }
            }
        }

    }
}
