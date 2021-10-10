using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeAttacked : MonoBehaviour
{
    // Start is called before the first frame update
    private float hp = 100;
    void Start()
    {
        
    }

    public void beingAttacked(float damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
        this.GetComponent<PlayerState>().Health_Change(hp);
    }

    public void getHealed(float heal)
    {
        hp += heal;
        if (hp > 100)
            hp = 100;
        this.GetComponent<PlayerState>().Health_Change(hp);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
