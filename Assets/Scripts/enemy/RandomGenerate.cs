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

    // get rocks object
    public GameObject[] targetRocks;

	// Use this for initialization
	void Start () {
       
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

        {
            //create random position
            Vector3 random_pos= this.transform.position;
            random_pos.x= this.transform.position.x + Random.Range(-68.0f, 68.0f);
            random_pos.z= this.transform.position.z + Random.Range(-119.0f, 119.0f);
            random_pos.y = -1.26f;

            Vector3 random_pos1 = this.transform.position;
            random_pos1.x = this.transform.position.x + Random.Range(-119, 119.0f);
            random_pos1.z = this.transform.position.z + Random.Range(-119f, 93.5f);
            random_pos1.y = -1.26f;


            // Process all the stones in a loop, and then determine the distance from the randomly generated random point.
            //If the distance is less than 1.5 (fine-tune according to the actual situation), then abandon this random generation and restart the next generation.
            if (targetRocks.Length>0)
            {
                foreach (GameObject rocksGroup in targetRocks)
                {
                    foreach (Transform rock in rocksGroup.transform)
                    {
                        Vector3 rock_pos = rock.position;
                        if ((random_pos - rock_pos).magnitude <= 2 || (random_pos1 - rock_pos).magnitude <= 2)
                        {
                            return;
                        }
                    }
                }
            }

            GameObject player = GameObject.Find("Player");
            if ((player.transform.position - random_pos).magnitude < 15 ||
                (player.transform.position - random_pos1).magnitude < 15)
            {
                return;
            }

            if (Random.Range(0, 3) % 3 == 1)
            {
                Instantiate(targetEnemy,random_pos, Quaternion.identity);
                enemyCounter++;
            }
            if (Random.Range(0, 3) % 3 == 2)
            {
                Instantiate(targetEnemy, random_pos1, Quaternion.identity);
                enemyCounter++;
            }
            //If the count reaches the maximum
            if (enemyCounter == enemyTotalNum)
            {
                //stop updating
                CancelInvoke();
            }
        }

    }
}

