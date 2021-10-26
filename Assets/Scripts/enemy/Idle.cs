using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

public class Idle : MonoBehaviour
{
    public string[] idles;
    private float changeTimer=0;
    private float patrolTimer=2;
    private NavMeshAgent patrolAgent;
    private GameObject Player;
    public float findDistance = 6;
    private bool isEngaging = false;
    // Start is called before the first frame update
    void Start()
    {
        patrolAgent = GetComponent<NavMeshAgent>();
        Player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = GetComponent<EnemyApproaching>().CalculateDistance();
        if (distance < findDistance||isEngaging)
        {
            this.patrolAgent.isStopped = false;
            GetComponent<EnemyApproaching>().enabled = true;
        }
        else
        {
            if (idles.Length>0)
                UpdateAnimation();
        }
    }

    public void SetEngaging()
    {
        this.isEngaging = true;
    }

    public bool getEngaging()
    {
        return this.isEngaging;
    }
    void UpdateAnimation()
    {
        if (changeTimer <= 0)
        {
            changeTimer = UnityEngine.Random.Range(3, 5);
            PlayIdleAnimation();
        }
        else
        {
            changeTimer -= Time.deltaTime;
        }
        if (patrolTimer <= 0)
        {
            this.patrolAgent.isStopped = false;
            patrolAgent.speed = 0.5f * this.GetComponent<EnemyApproaching>().speed;
            patrolTimer = 5;
            System.Random rd = new System.Random();
            int xplus;
            int zplus;
            while (true)
            {
                xplus = rd.Next(-10,11);
                zplus = rd.Next(-10, 11);
                if (xplus != 0 || zplus != 0)
                {
                    break;
                }
            }

            Vector3 dir = new Vector3(xplus, 0, zplus);
            Vector3 currentposition = this.transform.position;
            Vector3 des = currentposition + 3.5f * dir.normalized;
            patrolAgent.SetDestination(des);
            int idle = rd.Next(idles.Length);
            this.GetComponent<Animator>().Play("Walk");
        }
        else
        {
            patrolTimer -= Time.deltaTime;
        }

        if (!this.patrolAgent.isStopped)
        {
            float walkTime = 3.5f / this.patrolAgent.speed;
            if (patrolTimer < 5-walkTime)
            {
                this.patrolAgent.isStopped = true;
                PlayIdleAnimation();
            }
            else
            {
                this.GetComponent<Animator>().Play("Walk");
            }
        }
    }

    private void PlayIdleAnimation()
    {
        System.Random rd = new System.Random();
        int idle = rd.Next(idles.Length);
        this.GetComponent<Animator>().Play(idles[idle]);
    }
}
