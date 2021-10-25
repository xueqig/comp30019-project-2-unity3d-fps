using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Dontdestroy");
        int level = temp.GetComponent<VariablesSaver>().level;
        if(level != 1){
            GameObject.Find("Fog").GetComponent<MeshRenderer>().enabled = true;
        }else{
            GameObject.Find("Fog").GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
