using System;
using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;


namespace DefaultNamespace
{
    public class EnemyApproaching : MonoBehaviour
    {
        private GameObject Player;
        public float speed;
        private NavMeshAgent findpathAgent;
        private void Awake()
        {
            Player=GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            findpathAgent = GetComponent<NavMeshAgent>();
        }

        public float CalculateDistance()
        {
            Vector3 horizontalPosition = new Vector3(Player.transform.localPosition.x, this.transform.localPosition.y,
                Player.transform.localPosition.z);
            
            
            float distance = (horizontalPosition - this.transform.localPosition).magnitude;
            return distance;
        }


        void Update()
        {
            float distance = CalculateDistance();
            Vector3 horizontalPosition = new Vector3(Player.transform.localPosition.x, this.transform.localPosition.y,
                Player.transform.localPosition.z);
            Vector3 direction = horizontalPosition - this.transform.localPosition;
            if (distance < this.GetComponent<EnemyAttack>().radius)
                {
                    findpathAgent.isStopped = true;
                    this.GetComponent<EnemyAttack>().enabled = true;
                    this.transform.forward = direction;
                    this.enabled = false;
                }
            else
            {
                findpathAgent.isStopped = false;
                findpathAgent.speed = speed;
                //this.transform.forward = direction;
                findpathAgent.SetDestination(Player.transform.position);
                this.GetComponent<Animator> ().Play ("Walk");
            }
        }
    }
}