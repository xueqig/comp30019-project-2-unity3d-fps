using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonoBehaviour
{
    public string[] idles;
    private float changeTimer;
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        if (idles.Length>0)
        updateAnimation();
    }
    void updateAnimation()
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
