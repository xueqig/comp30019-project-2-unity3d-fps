using System;
using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;


namespace DefaultNamespace
{
    public class EnemyApproaching : MonoBehaviour
    {
        public GameObject Player;
        public float speed;
        private bool needReset = true;
        public float findDistance = 6;
        private void Awake()
        {
            //Player = GameObject.Find("Player");
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
            if (distance<findDistance)
            {
                this.GetComponent<Idle>().enabled = false;
                if (distance < this.GetComponent<EnemyAttack>().radius)
                    this.GetComponent<EnemyAttack>().enabled = true;
                else
                {
                                    
                    this.transform.forward = direction;
                    this.transform.localPosition += direction.normalized * speed * Time.deltaTime;
                    this.GetComponent<Animator> ().Play ("Walk");
                    needReset = true;
                }

            }
            else
            {
                if (needReset)
                {
                    this.GetComponent<Animator>().Play("idle1", -1, 0f);
                    this.GetComponent<Idle>().enabled = true;
                    needReset = false;
                }
            }
        }
    }
}