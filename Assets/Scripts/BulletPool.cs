using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //Singleton Pattern object
    private static BulletPool uniqueInstance;

    public GameObject bulletPrefab;

    // Set the number of bullets in the object pool
    public int bulletPoolSize = 40;

    private int bulletInterator = 0;

    private GameObject[] bulletPool;

    // When the scene is generated, generate all the required bullets
    void Awake(){
        // Create a object list, the size is the value of bulletPoolSize
        bulletPool = new GameObject[bulletPoolSize];
        
        // Create every bullet object
        for(int i = 0; i < bulletPoolSize; i++){
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            
            // Set every bullet statuse is unactive
            bullet.SetActive(false);
            
            // put bullet object in the bullet list
            bulletPool[i] = bullet;
        }
        uniqueInstance = this;
    }

    // Get the bullet pool object
    public static BulletPool GetBulletPoolInstance()
    {
        if (uniqueInstance == null)
        {
            uniqueInstance = new BulletPool();
        }

        return uniqueInstance;
    }

    // When player shoot, call bullet objects in order
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
