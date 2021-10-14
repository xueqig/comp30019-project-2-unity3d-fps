using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomGenerate : MonoBehaviour {
    //This position's monster generated
    public GameObject targetEnemy;
    //The total number of the monsters
    public int enemyTotalNum = 20;
    //The time interval of generating monsters
    public float intervalTime = 1;
    //The counter of generating monsters
    private int enemyCounter;

    private GameObject[] rocks;
    // get rocks object

    // Use this for initialization
	void Start ()
    {
        rocks = GameObject.FindGameObjectsWithTag("rock");
        //at start, the intial value of the monster is 0；
        enemyCounter = 0;
        // repeat generating
        InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //the generating method 
    public void deathEvent()
    {
        this.enemyCounter--;
    }

    private void CreatEnemy()
    {
        //If the player survives
        if (enemyCounter < enemyTotalNum)
        {
            //create random position
            Vector3 random_pos= this.transform.position;
            random_pos.x= this.transform.position.x + Random.Range(-68.0f, 68.0f);
            random_pos.z= this.transform.position.z + Random.Range(-119.0f, 119.0f);
            random_pos.y = -1.26f;
            
            // Process all the stones in a loop, and then determine the distance from the randomly generated random point.
            //If the distance is less than 1.5 (fine-tune according to the actual situation), then abandon this random generation and restart the next generation.
            
            foreach (GameObject rock in rocks)
            {
                Vector3 rock_pos = new Vector3(rock.transform.position.x,random_pos.y,rock.transform.position.z);
                if ((random_pos - rock_pos).magnitude <= 2f )
                {
                    return;
                }
            }
            
            

            GameObject player = GameObject.Find("Player");
            if ((player.transform.position - random_pos).magnitude < 12 )
            {
                return;
            }
            Instantiate(targetEnemy,random_pos, Quaternion.identity);
                enemyCounter++;
        }

    }
}

