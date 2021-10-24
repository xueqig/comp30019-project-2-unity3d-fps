using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void Initialize(){
        if(SceneManager.GetActiveScene().name == "01Start"){
            return;
        }
        SceneManager.LoadScene(0);
    }
}
