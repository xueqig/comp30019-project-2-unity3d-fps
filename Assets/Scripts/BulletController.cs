using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletController : MonoBehaviour
{
    private float bulletDamage = 20f;

    private float detectDistance = 2f;
    private float speed = 0.1f;

    private float bullet_AliveTime = 3f;
    
    private Vector3 currentPosition;

    private Vector3 nextPosition;

    public void SetbulletDamage(float damage){
        bulletDamage = damage;

    }

    public void Setspeed(float speed){
        this.speed = speed;
    }

    public void Setbullet_AliveTime(float time){
        bullet_AliveTime = time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();
    }

    public void DetectEnemy(){

        currentPosition = transform.position;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        nextPosition = transform.position;

        Ray ray = new Ray(currentPosition, nextPosition - currentPosition);
        
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, (nextPosition - currentPosition).magnitude)){
            if(hit.transform.CompareTag("Enemy"))
            {
                this.gameObject.SetActive(false);
                GameObject target = hit.transform.parent.gameObject;
                Instantiate(target.GetComponent<EnemyHealthController>().bleedingEffect,
                    hit.point, Quaternion.identity);
                target.GetComponent<EnemyHealthController>().BeingAttacked(bulletDamage);
            }
            else{
                
            }
        }

    }

    public void BulletActiveFalse(){
        StartCoroutine(SetBulletActiveFalse());
    }

    private IEnumerator SetBulletActiveFalse(){
        yield return new WaitForSeconds(bullet_AliveTime);
        this.gameObject.SetActive(false);
    }


}
