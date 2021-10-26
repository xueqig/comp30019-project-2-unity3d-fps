using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{

    public Button InstructionsButtonBack;

    // Start is called before the first frame update
    void Start()
    {
        InstructionsButtonBack.onClick.AddListener(InstructionsButtonLisBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstructionsButtonLisBack()
    {
        SceneManager.LoadScene(0);
    }

}
