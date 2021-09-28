using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;

    private float move_Speed = 5f;

    public float crouch_Speed = 2f;

    public float stamina = 100f;

    public float sprint_Cost = 10f;

    private bool is_Crouching = false;

    private float stand_Height = 0.4f;

    private float crouch_Height = -0.1f;

    private Transform mainCamera;

    void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        move_Speed = playerMovement.speed;
        mainCamera = transform.GetChild(0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint(){
        if(stamina > 0f){
            if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching){
                playerMovement.speed = sprint_Speed;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching){
                playerMovement.speed = move_Speed;
            }

            if(Input.GetKey(KeyCode.LeftShift) && !is_Crouching){
                stamina = stamina - sprint_Cost * Time.deltaTime;
                if(stamina <= 0f){
                    stamina = 0f;
                    playerMovement.speed = move_Speed;
                }
            }
            else{
                if(stamina != 100f){
                    stamina = stamina + sprint_Cost/2f * Time.deltaTime;

                    if(stamina > 100f){
                        stamina = 100f;
                    }
                }
            }
        }


    }

    void Crouch(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(is_Crouching){
                playerMovement.speed = move_Speed;
                mainCamera.localPosition = new Vector3(0f, stand_Height, 0f);
                is_Crouching = false;
            }else{
                playerMovement.speed = crouch_Speed;
                mainCamera.localPosition = new Vector3(0f, crouch_Height, 0f);
                is_Crouching = true;
            }
        }

    }
}
