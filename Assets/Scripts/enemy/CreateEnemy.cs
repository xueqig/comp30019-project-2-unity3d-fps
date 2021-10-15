using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Instantiate(this.enemy, new Vector3(0, 0, 9), Quaternion.identity);
        Instantiate(this.enemy, new Vector3(2, 0, 9), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
