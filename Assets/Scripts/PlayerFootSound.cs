using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSound : MonoBehaviour
{
    private AudioSource footstep_Sound;

    public AudioClip[] footstep_Clip;

    [HideInInspector]
    public float volume_Min, volume_Max;

    [HideInInspector]
    public float step_Distance;

    private float accumulated_Distance;

    private CharacterController characterController;

    void Awake(){
        footstep_Sound = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!characterController.isGrounded){
            return;
        }else{
            if(characterController.velocity.sqrMagnitude > 0){
                accumulated_Distance = accumulated_Distance + Time.deltaTime;
                
                if(accumulated_Distance > step_Distance){
                    footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                    footstep_Sound.clip = footstep_Clip[Random.Range(0,footstep_Clip.Length)];
                    footstep_Sound.Play();

                    accumulated_Distance = 0f;
                }
            }else{
                accumulated_Distance = 0f;
            }
        }

    
    }
}
