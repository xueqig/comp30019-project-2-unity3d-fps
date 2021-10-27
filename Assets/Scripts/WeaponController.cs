using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioClip[] gun_Sound;

    private AudioSource shoot_Sound, reload_Sound;

    [SerializeField]
    private int magazineCapacity = 30;

    private int bullet_Count = 0;

    [SerializeField]
    private Transform firepoint;

    public Transform bulletPoint;

    public float reload_Time = 2f;

    private float timer = 0;

    public float damage = 70f;

    public GameObject bulletPrefab;

    public float shootPower = 40f;

    public float ejectPower = 150f;

    public float bullet_AliveTime = 3f;

    //public Transform casingPoint;

    private Animator animator;

    public Text bulletUI;

    private bool isReloading = false;
    private bool upgrade = false;

    
    // Start is called before the first frame update
    void Awake(){
        shoot_Sound = GetComponent<AudioSource>();
        reload_Sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        bullet_Count = magazineCapacity;
        bulletUI.text = bullet_Count.ToString(); 
        GameObject temp = GameObject.Find("Dontdestroy");
        int level = temp.GetComponent<VariablesSaver>().level;
        if (level == 1)
            damage *= 2;
    }

    // Update is called once per frame
    public void Upgrade(float Damageupgrade)
    {
        damage *= Damageupgrade;
        upgrade = true;
    }

    public bool IsUpgraded()
    {
        return upgrade;
    }
    public void SetUpgraded(bool setting)
    {
        upgrade=setting;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && bullet_Count > 0){
            BulletFired();
        }

        if(Input.GetKeyDown(KeyCode.R) && bullet_Count != magazineCapacity && bullet_Count != 0){
           
            isReloading = true;
            ReloadAnimation();
            
            bullet_Count = magazineCapacity;
            Invoke("Set_bulletCount", reload_Time);

                
            Invoke("Play_ReloadSound", reload_Time);
            
        }
        
        if(bullet_Count == 0){
            isReloading = true;
            
            if(timer == 0){
                ReloadAnimation();
            }
            timer = timer + Time.deltaTime;
            

            if(timer > reload_Time){
                bullet_Count = magazineCapacity;
                bulletUI.text = bullet_Count.ToString();
                isReloading = false; 
                timer = 0;
                
                Play_ReloadSound();
            }
        }
    }

    void BulletFired(){

        if(isReloading == false){
            Invoke("Play_ShootSound", 0.1f);
        
            GeneratingBullet();

            Turn_On_MuzzleFlash();

            Invoke("Turn_Off_MuzzleFlash", 0.5f);

            bullet_Count--;
            bulletUI.text = bullet_Count.ToString(); 
        }
        

        
    }

    void Set_bulletCount(){
        bulletUI.text = bullet_Count.ToString(); 
        isReloading = false;
    }

    void Play_ReloadSound()
    {
        reload_Sound.clip = gun_Sound[1];
        reload_Sound.Play();
    }

    void Play_ShootSound()
    {
        shoot_Sound.clip = gun_Sound[0];
        shoot_Sound.Play();
    }

    void Turn_On_MuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        muzzleFlash.GetComponent<ParticleSystem>().Play();
    }

    void Turn_Off_MuzzleFlash()
    {
        muzzleFlash.SetActive(false);
        muzzleFlash.GetComponent<ParticleSystem>().Stop();
    }

    void GeneratingBullet(){
        GameObject temp_BulletPrefab;

        temp_BulletPrefab = BulletPool.GetBulletPoolInstance().GetBullet();
        temp_BulletPrefab.GetComponent<BulletController>().Setbullet_AliveTime(bullet_AliveTime);
        temp_BulletPrefab.GetComponent<BulletController>().SetbulletDamage(damage);
        temp_BulletPrefab.GetComponent<BulletController>().Setspeed(shootPower);

        temp_BulletPrefab.transform.position = bulletPoint.position;
        temp_BulletPrefab.transform.rotation = bulletPoint.rotation;

        temp_BulletPrefab.SetActive(true);
        temp_BulletPrefab.GetComponent<BulletController>().BulletActiveFalse();

    }

    public void ReloadAnimation(){
        animator.SetTrigger("Reload");
    }




}
