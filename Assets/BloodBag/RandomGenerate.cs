using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RandomGenerate : MonoBehaviour {
    //该出生点生成的怪物
    public GameObject targetEnemy;
    //生成怪物的总数量
    public int enemyTotalNum = 1000;
    //生成怪物的时间间隔
    public float intervalTime = 1;
    //生成怪物的计数器
    private int enemyCounter;

    // 获得rocks对象
    public GameObject targetRocks;

	// Use this for initialization
	void Start () {
       
        //初始时，怪物计数为0；
        enemyCounter = 0;
        //重复生成怪物
        InvokeRepeating("CreatEnemy", 0.5F, intervalTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //方法，生成怪物
    private void CreatEnemy()
    {
        //如果玩家存活

        {
            //创建随机点
            Vector3 random_pos= this.transform.position;
            random_pos.x= this.transform.position.x + Random.Range(-68.0f, 68.0f);
            random_pos.z= this.transform.position.z + Random.Range(-119.0f, 119.0f);
            random_pos.y = -1.26f;

            Vector3 random_pos1 = this.transform.position;
            random_pos1.x = this.transform.position.x + Random.Range(-119, 119.0f);
            random_pos1.z = this.transform.position.z + Random.Range(-119f, 93.5f);
            random_pos1.y = -1.26f;


            // 循环处理所有的石头，然后判断离随机生成的随机点距离，如果距离小于1.5（根据实际情况再微调），就放弃这次随机生成，重新开始下一次生成
            foreach (Transform rock in targetRocks.transform){
                Vector3 rock_pos = rock.position;
                if ((random_pos - rock_pos).magnitude <= 1.5 || (random_pos1 - rock_pos).magnitude <= 1.5){
                    return;
                }                
            }

            //生成一只怪物
            if (Random.Range(0, 3) % 3 == 0)
            {
                Instantiate(targetEnemy, this.transform.position, Quaternion.identity);
                enemyCounter++;
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
            //如果计数达到最大值
            if (enemyCounter == enemyTotalNum)
            {
                //停止刷新
                CancelInvoke();
            }
        }

    }
}

