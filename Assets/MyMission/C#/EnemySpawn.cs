using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    //敌人类型的Prefab
    public Transform[] m_enemy1 = new Transform[3];
    //生成敌人的数量
    public int m_enemyCount1 = 0;

    //敌人的最大生成数量
    public int m_maxEnemy = 10;

    //生成敌人的时间间隔
    public float m_timer1 = 2.0f;

    protected Transform m_transform1;

    void Start()
    {
        m_transform1 = this.transform;//获取当前位置
    }

    void Update()
    {
        //如果生成敌人的数量达到最大值，停止生成
        if (m_enemyCount1 >= m_maxEnemy)
            return;

        //每隔一定时间
        m_timer1 -= Time.deltaTime;
        if (m_timer1 <= 0)
        {
            m_timer1 = Random.value * 5.0f + 1.0f;
            //敌人数量加一
            m_enemyCount1 += 1;

            InsEnemy();
        }
    }

    //敌人随机生成
    public void InsEnemy()
    {
        //随机的范围
        float Pos_X = Random.Range(-2, 3);//X轴的范围，也就是前后范围
        float Pos_Z = Random.Range(-2, 3);//Z轴的范围，也就是左右范围
        Vector3 enemyPos = m_transform1.position + new Vector3(Pos_X, 0, Pos_Z);
        //生成敌人
        Instantiate(m_enemy1[Random.Range(0, m_enemy1.Length)], enemyPos, Quaternion.identity);//实例化敌人出来
    }

}
