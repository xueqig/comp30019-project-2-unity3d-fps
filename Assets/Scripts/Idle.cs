using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public string[] idles;
    private float changeTimer;
    private GameObject Player;
    public float findDistance = 6;
    // Start is called before the first frame update
    void Start()
    {
        ;Player=GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = GetComponent<EnemyApproaching>().CalculateDistance();
        Vector3 horizontalPosition = new Vector3(Player.transform.localPosition.x, this.transform.localPosition.y,
            Player.transform.localPosition.z);
        Vector3 direction = horizontalPosition - this.transform.localPosition;
        if (distance < findDistance)
        {
            GetComponent<EnemyApproaching>().enabled = true;
        }
        else
        {
            if (idles.Length>0)
                UpdateAnimation();
            GetComponent<EnemyApproaching>().enabled = false;
        }

    }
    
    void UpdateAnimation()
    {
        if (changeTimer <= 0)
        {
            changeTimer = UnityEngine.Random.Range(3, 5);
            System.Random rd = new System.Random();
            int idle = rd.Next(idles.Length);
            this.GetComponent<Animator>().Play(idles[idle]);
        }
        else
        {
            changeTimer -= Time.deltaTime;
        }
    }
}
