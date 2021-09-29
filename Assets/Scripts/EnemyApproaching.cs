using System;
using UnityEditor.UIElements;
using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class EnemyApproaching : MonoBehaviour
    {
        public GameObject Player;
        public float speed=1;
        private void Awake()
        {
            Player = GameObject.Find("Player");
        }

        
        
        void Update()
        {
            Vector3 direction = Player.transform.localPosition - this.transform.localPosition;
            float distance = (Player.transform.localPosition - this.transform.localPosition).magnitude;
            float findDistance = 5;
            if (distance < findDistance)
            {
                this.transform.localPosition += direction.normalized * speed * Time.deltaTime;
            }
        }
    }
}