using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprint_Speed = 10f;

    private float move_Speed = 5f;

    public float crouch_Speed = 2f;

    private float stamina = 100f;

    public float sprint_Cost = 10f;

    private bool is_Crouching = false;

    private float stand_Height = 0.4f;

    private float crouch_Height = -0.1f;

    private PlayerFootSound playerFootSound;
    private float sprint_Volume = 1f;
    
    private float crouch_Volume = 0.1f;

    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;

    private Transform mainCamera;

    private PlayerState playerState;

    void Awake(){
        playerMovement = GetComponent<PlayerMovement>();
        move_Speed = playerMovement.speed;
        mainCamera = transform.GetChild(0);
        playerFootSound = GetComponent<PlayerFootSound>();
        playerState = GetComponent<PlayerState>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerFootSound.volume_Min = walk_Volume_Min;
        playerFootSound.volume_Max = walk_Volume_Max;
        playerFootSound.step_Distance = walk_Step_Distance;
        stamina = playerState.get_stamina_Health();

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint(){
        if(stamina >= 0f){
            if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching){
                playerMovement.speed = sprint_Speed;
                playerFootSound.volume_Min = sprint_Volume;
                playerFootSound.volume_Max = sprint_Volume;
                playerFootSound.step_Distance = sprint_Step_Distance;
            }

            if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching){
                playerMovement.speed = move_Speed;
                playerFootSound.volume_Min = walk_Volume_Min;
                playerFootSound.volume_Max = walk_Volume_Max;
                playerFootSound.step_Distance = walk_Step_Distance;
            }

            if(Input.GetKey(KeyCode.LeftShift) && !is_Crouching){
                stamina = stamina - sprint_Cost * Time.deltaTime;
                playerState.Stamina_Change(stamina);

                if(stamina <= 0f){
                    stamina = 0f;
                    playerMovement.speed = move_Speed;
                    playerFootSound.volume_Min = walk_Volume_Min;
                    playerFootSound.volume_Max = walk_Volume_Max;
                    playerFootSound.step_Distance = walk_Step_Distance;
                }
            }
            else{
                if(stamina != 100f){
                    stamina = stamina + sprint_Cost/2f * Time.deltaTime;
                    playerState.Stamina_Change(stamina);

                    if(stamina > 100f){
                        stamina = 100f;
                    }
                }
            }
        }


    }

    public void AddStamina(float value)
    {
        this.stamina += value;
    }

    void Crouch(){
        if(Input.GetKeyDown(KeyCode.C)){
            if(is_Crouching){
                playerMovement.speed = move_Speed;
                mainCamera.localPosition = new Vector3(0f, stand_Height, 0f);
                is_Crouching = false;

                playerFootSound.step_Distance = walk_Step_Distance;
                playerFootSound.volume_Min = walk_Volume_Min;
                playerFootSound.volume_Max = walk_Volume_Max;
                
            }else{
                playerMovement.speed = crouch_Speed;
                mainCamera.localPosition = new Vector3(0f, crouch_Height, 0f);
                
                playerFootSound.step_Distance = crouch_Step_Distance;
                playerFootSound.volume_Max = crouch_Volume;
                playerFootSound.volume_Min = crouch_Volume;
                
                is_Crouching = true;

            }
        }

    }
}
