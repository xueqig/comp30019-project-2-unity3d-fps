using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool uniqueInstance;

    public GameObject bulletPrefab;

    public int bulletPoolSize = 40;

    private int bulletInterator = 0;

    private GameObject[] bulletPool;

    void Awake(){
        
        bulletPool = new GameObject[bulletPoolSize];

        for(int i = 0; i < bulletPoolSize; i++){
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool[i] = bullet;
        }
        uniqueInstance = this;
    }

    public static BulletPool GetBulletPoolInstance()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = new BulletPool();
        }

        return uniqueInstance;
    }

    public GameObject GetBullet(){
        GameObject bullet = bulletPool[bulletInterator];
        bulletInterator++;
        if(bulletInterator == bulletPoolSize){
            bulletInterator = 0;
        }
        return bullet;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
