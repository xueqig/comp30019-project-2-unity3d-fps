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

        private void Awake()
        {
            Player = GameObject.Find("Player");
        }

        


        void Update()
        {
            Vector3 horizontalPosition = new Vector3(Player.transform.localPosition.x, this.transform.localPosition.y,
                Player.transform.localPosition.z);
            Vector3 direction = horizontalPosition - this.transform.localPosition;
            
            float distance = (horizontalPosition - this.transform.localPosition).magnitude;
            float findDistance = 5;
            if (distance<findDistance)
            {
                this.GetComponent<Idle>().enabled = false;
                this.transform.forward = direction;
                this.transform.localPosition += direction.normalized * speed * Time.deltaTime;
                this.GetComponent<Animator> ().Play ("Walk");
                needReset = true;
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