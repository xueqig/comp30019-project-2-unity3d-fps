using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static Player Instance = null;
    public int life = 100;            //主角的血量
    public Slider HP_zj;            //添加主角血条Slider的引用

    private CharacterController controller;
    private Vector3 cameraRotation;    // 摄像机旋转角度
    private float speed = 3.0f;//移动速度
    private float gravity = 3.0f;//重力
    // 游戏得分
    public int m_score = 0;
    // 游戏得分UI文字
    public Text txt_score;

    //射击
    public LayerMask layer; // 射击时射线能射到的碰撞层
    public Transform fx; // 射中目标后的粒子效果
    public Transform muzzlePoint; // 枪口的Transform组件
    private float shootTimer = 0.1f; // 射击间隔的计时器
    Ray shootRay; //定义射线
    RaycastHit shootHit;
    LineRenderer gunLine;
    int shootableMask;//定义接受射线的面，mask
    public Transform cameraTransform;   // 摄像机的Transform组件
    public AudioClip m_audio;    //射击音效

    /// <summary>
    /// 攻击力增加,防御力增加
    /// </summary>
    public int Attact = 30;//攻击力初始值
    public int Defense = 10;//防御力初始值
    public AudioClip HP_audio;    //HP音效

    void Awake() {
        // 锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = true;
    }

    void Start()
    {
        Instance = this;
        // 获取摄像机
      //  cameraTransform = Camera.main.GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        //旋转摄像机
        //cameraRotation.y += -180f;

    }

    void Update()
    {
        //当生命值小于等0时，返回
        if (life <= 0)
        {
            return;
        }
        Control();

        // 更新射击间隔时间
        shootTimer -= Time.deltaTime;
        // 鼠标左键射击
        if (Input.GetMouseButton(0) && shootTimer <= 0)
        {
            shooting();
        }
    }

    //主角被攻击扣血
    public void OnDamage(int damage)
    {
        life -= damage;
        HP_zj.value = life;//显示主角生命值
        if (life <= 0)
        {
            //取消锁定鼠标光标
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(5);//跳转到失败界面
        }
    }

    /// <summary>
    /// 控制主角的重力运动和前后左右移动
    /// </summary>
    private void Control()
    {
        // 获取鼠标移动距离
        float rh = Input.GetAxis("Mouse X");
        float rv = Input.GetAxis("Mouse Y");

        // 旋转摄像机
        cameraRotation.x -= rv;
        cameraRotation.y += rh;
        cameraTransform.eulerAngles = cameraRotation;

        //使主角的面向方向与摄像机一致
        Vector3 camrot = cameraTransform.eulerAngles;
        camrot.x = 0; camrot.z = 0;
        transform.eulerAngles = camrot;

        //更新摄像机位置（始终与Player一致)
     //   cameraTransform.position = transform.TransformPoint(0, 0.3f, -0.4f);

        float x = 0, y = 0, z = 0;
        // 重力运动
        // y -= gravity * Time.deltaTime;
        // 前后移动
        if (Input.GetKey(KeyCode.W))
        {
            z += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            z -= speed * Time.deltaTime;
        }
        // 左右移动
        if (Input.GetKey(KeyCode.A))
        {
            x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x += speed * Time.deltaTime;
        }
        //跳跃

        if (Input.GetKeyDown(KeyCode.Space))
        {
           // if (this.gameObject.transform.position.y < 7.47f)
           // {
                y += gravity * 100 * Time.deltaTime;
           // }
        }
        if (this.gameObject.transform.position.y >= 0)
        {
            y -= gravity * 10 * Time.deltaTime;
        }
        // 使用Character Controller而不是Transform提供的Move方法
        // 因为Character Controller提供的Move方法会自动进行碰撞检测
        controller.Move(transform.TransformDirection(new Vector3(x, y, z)));
    }

    // 更新分数
    public void SetScore(int score)
    {
        m_score += score;//分数增加
        txt_score.text = m_score.ToString();//分数文本显示
    }

    //射击
    public void shooting()
    {
        shootTimer = 0.1f;

        this.GetComponent<AudioSource>().PlayOneShot(m_audio);//播放射击声音

        //RaycastHit用来保存射线探测结果
        RaycastHit info;
        //从muzzlepoint位置，向摄像机面向的方向射出一根射线
        //射线只能与m_layer所指定的层碰撞
        bool hit = Physics.Raycast(muzzlePoint.position, cameraTransform.TransformDirection(Vector3.forward), out info, 100, layer);
        if (hit)
        {
            //如果射中了tag为enemy的游戏体，怪物
            if (info.transform.tag.CompareTo("enemy") == 0)
            {
                Enemy enemy = info.transform.GetComponent<Enemy>();

                //敌人根据攻击力减少生命
                enemy.OnDamage(Attact);
            }

            //在射中的地方释放一个粒子效果
            Instantiate(fx, info.point, info.transform.rotation);
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        //当主角碰撞检测到敌人随机生成血包，增加攻击力
        if (other.tag == "HP")
        {

            Destroy(other.gameObject);//销毁对应物体

            Attact = Attact + 5;//攻击力加5
            Defense = Defense + 5;//防御力加5

            this.GetComponent<AudioSource>().PlayOneShot(HP_audio);//播放HP声音
            
        }
    }


}
