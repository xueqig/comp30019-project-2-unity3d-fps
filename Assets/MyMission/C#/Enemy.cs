using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    Transform m_transform;

    //主角实例
    Player m_player;
    //玩家
    private Transform player;

    //寻路组件
    UnityEngine.AI.NavMeshAgent m_agent;

    //移动速度
    public float m_movSpeed = 4f;

    //动画组件
    public Animator m_ani;

    //旋转速度
    public float m_rotSpeed = 5.0f;

    //计时器
    float m_timer = 0;

    public int m_life = 100;//生命值
    public Slider HP_Enemy;    //添加敌人血条Slider的引用
    public GameObject Slider_HP_Enemy;//游戏物体血条

    public int attack_JL = 30;//攻击距离

    //重新定义上下前后最大值的点坐标，用于控制角色不在范围内时的移动
    public float Max_dis = 4;//上下左右最大的值
    public Vector3 Vec_U;//最上的坐标点位置
    public Vector3 Vec_D;//最下的坐标点位置
    public Vector3 Vec_R;//最前的坐标点位置
    public Vector3 Vec_L;//最后的坐标点位置
    public Vector3 Vec_Next;//下次随机前进的点位置

    public float speed = 2;//移动速度

    //随机掉落物HP
    public GameObject HPPre;

    void Start()
    {
        m_transform = this.transform;

        //获得主角
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_agent.speed = m_movSpeed;
        player = GameObject.FindWithTag("Player").transform;

        //赋值上下左右最大值的点坐标
        Vec_U = m_transform.position;
        Vec_D = m_transform.position;
        Vec_R = m_transform.position;
        Vec_L = m_transform.position;

        Vec_U.x = m_transform.position.x + Max_dis;
        Vec_D.x = m_transform.position.x - Max_dis;
        Vec_R.z = m_transform.position.z + Max_dis;
        Vec_L.z = m_transform.position.z - Max_dis;

        Vec_Next = this.transform.position;//下次随机前进的点位置一开始设置为初始点位置

        transform.LookAt(Vec_Next);
    }

    // Update is called once per frame
    void Update()
    {
        //如果主角生命值为0，什么也不做
        if (m_player.life <= 0)
            return;


        if (Vector3.Distance(m_transform.position, m_player.transform.position) >= attack_JL)
        {
            Enemy_AI_();
            return;
        }
        else
        {
            m_ani.SetBool("run", false);
        }

        //更新计时器
        m_timer -= Time.deltaTime;

        //获取当前动画状态
        AnimatorStateInfo stateInfo = m_ani.GetCurrentAnimatorStateInfo(0);

        //如果处于待机且不是过渡状态
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.idle02") && !m_ani.IsInTransition(0))
        {
            m_ani.SetBool("idle", false);

            //待机一定时间
            if (m_timer > 0)
                return;

            //如果距离主角小于？m，进入攻击动画状态
            if (Vector3.Distance(m_transform.position, m_player.transform.position) < attack_JL)
            {
                if (Vector3.Distance(m_transform.position, m_player.transform.position) < 3f)
                {
                    //停止寻路
                    m_agent.ResetPath();
                    m_ani.SetBool("attack", true);
                }
                else
                {
                    //重置计时器
                    m_timer = 2;
                    //进入跑步动画状态
                    m_ani.SetBool("run", true);
                    m_agent.SetDestination(GameObject.Find("Player").transform.position);
                }
            }
        }

        //如果是处于跑步且不是过渡状态
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.run") && !m_ani.IsInTransition(0))
        {
            m_ani.SetBool("run", false);

            //每隔1秒重新定位主角的位置
            if (m_timer < 0)
            {
                m_agent.SetDestination(GameObject.Find("Player").transform.position);
                m_timer = 2;
            }
        }

        //如果距离主角小于？m，进入攻击动画状态
        if (Vector3.Distance(m_transform.position, m_player.transform.position) < 3f)
        {
            //停止寻路
            m_agent.ResetPath();
            //攻击动画状态
            m_ani.SetBool("attack", true);
        }

        //如果是处于攻击且不是过渡状态
        if (stateInfo.fullPathHash == Animator.StringToHash("Base Layer.attack01") && !m_ani.IsInTransition(0))
        {
            //面向主角
            RotateTo();
            m_ani.SetBool("attack", false);

            //如果动画播完，重新进入待机状态
            if (stateInfo.normalizedTime >= 1.0f)
            {
                m_ani.SetBool("idle", true);
                //重置计时器,待机1秒
                m_timer = 2;

               m_player.OnDamage(10);//添加对主角伤害功能            
            }
        }
    }

    void RotateTo()
    {
        //获取目标(Player)方向
        Vector3 targetdir = m_player.transform.position - m_transform.position;
        //计算出新方向
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetdir, m_rotSpeed * Time.deltaTime, 0.0f);
        //旋转至新方向
        m_transform.rotation = Quaternion.LookRotation(newDir);
    }

    //自我AI运动
    void Enemy_AI_()
    {
        //距离下次前进点的位置只差
        float dis_r = Vector3.Distance(transform.position, Vec_Next);
        if (dis_r >= 2)
        {
            //当距离大于N时，继续自我运动
            transform.position = Vector3.MoveTowards(transform.position, Vec_Next, speed * Time.deltaTime);
        }
        else
        {
            //否则，当距离小于N时，重新确认下个随机前进的点位置
            int n = Random.Range(0, 4);//随机0到3的四个数，分别代表四个最大值坐标位置
            if (n == 0)
            {
                Vec_Next = Vec_U;
            }
            if (n == 1)
            {
                Vec_Next = Vec_D;
            }
            if (n == 2)
            {
                Vec_Next = Vec_R;
            }
            if (n == 3)
            {
                Vec_Next = Vec_L;
            }
        }
        //进入跑步动画状态
        m_ani.SetBool("run", true);
        transform.LookAt(Vec_Next);
    }

    //敌人受伤害
    public void OnDamage(int damage)
    {
        m_life -= damage;
        HP_Enemy.value = m_life;

        //如果生命值为0,播放死亡动画
        if (m_life <= 0)
        {
            m_ani.SetBool("death", true);
            Slider_HP_Enemy.SetActive(false);//关闭血条显示
            //停止寻路
            m_agent.ResetPath();
            //销毁自身
            Destroy(this.gameObject);

            //随机产生掉落HP物体
            float ran = Random.Range(0,3);
            if (ran == 0)
            {
                Vector3 Pos = this.gameObject.transform.position + new Vector3(0,1f,0);//调整掉落物的Y轴位置
                GameObject HP = (GameObject)Instantiate(HPPre, Pos, Quaternion.identity);
            }
        }
    }

}
