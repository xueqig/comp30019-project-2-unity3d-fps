using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Vector3 move_Direction;

    public float speed = 5f;
    
    public float jump_Force = 10f;

    public float mouseSpeed = 5f;

    public Transform mainCamera;
    

    float RotationY = 0f;
    float RotationX = 0f;

    public float minmouseY = -45f;
    public float maxmouseY = 45f;

    private float gravity = 20f;

    private float vertical_Velocity;


    void Awake(){
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //LockAndUnlockCursor();
        
        MoveThePlayer();
        
        
    }

    void LockAndUnlockCursor(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState = CursorLockMode.None;
            }
            else{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void MoveThePlayer(){
        move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f,
                                     Input.GetAxis("Vertical"));
        
        move_Direction = transform.TransformDirection(move_Direction);

        move_Direction = move_Direction * speed * Time.deltaTime;

         if( this.transform.position.x <= -1){
            if(move_Direction.x < 0)
                return;
        }

        if(this.transform.position.x >= 254){
            if(move_Direction.x > 0)
                return;
        }

        if(this.transform.position.z <= -37){
            if(move_Direction.z < 0)
                return;
        }

        if(this.transform.position.z >= 216){
            if(move_Direction.z > 0){
                return;
            }
        }

        if(characterController.isGrounded){
            vertical_Velocity = vertical_Velocity - gravity * Time.deltaTime;
            
            if(characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)){
                vertical_Velocity = jump_Force;
            }
        }else{
            vertical_Velocity = vertical_Velocity - gravity * Time.deltaTime;
        }

        move_Direction.y = vertical_Velocity * Time.deltaTime;

        characterController.Move(move_Direction);

        RotationX += mainCamera.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSpeed;
        
        RotationY -= Input.GetAxis("Mouse Y") * mouseSpeed;
        RotationY = Mathf.Clamp(RotationY, minmouseY, maxmouseY);
        
        this.transform.eulerAngles = new Vector3(0, RotationX, 0);
        mainCamera.transform.eulerAngles = new Vector3(RotationY, RotationX, 0);
    }
}
