using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesSaver : MonoBehaviour
{
    public static VariablesSaver instance;

    public int level = 2;

    //public int hasFog= 0; 

    void Awake(){
        if(instance != null){
            DestroyImmediate(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
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
