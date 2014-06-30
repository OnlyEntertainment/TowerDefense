using UnityEngine;
using System.Collections;

public class OE_FirstPersonController : MonoBehaviour {

//Public Variablen
    //Objekte
    //public GameObject playerBody;
    public Camera playerCamera;
    private CharacterController controller;


    //Bewegung
    public  float moveWalkSpeed = 6.0f;
    public float moveRunMultilikator = 2.0f;
    public float moveJumpSpeed = 8.0f;

    //Gravitation
    public bool gravityUseSystemGravity = true;
    public float gravityUser = 9.81f;
    private float gravity;

    //Kamera
    public float lookRotationSpeed = 100.0f;
    public float lookMinRotationX = -50.0f;
    public float lookMaxRotationX = 50.0f;
    private float lookCurrentRotationX = 0f;

    /*
     * Input.GetAxis("Horizontal")
     *      Negativ = z.B. A (Links)
     *      Positiv = z.B. D (rechts)
     * Input.GetAxis("Vertical"));
     *      Negativ = z.B. S (Hinten)
     *      Positiv = z.B. W (Vorne)
     *      
     * 
     * Input.GetAxis("Mouse X")
     *      Negativ = z.B. (Maus Nach Links) - Links gucken
     *      Positiv = z.B. (Maus Nach Rechts) - Rechts gucken
     * Input.GetAxis("Mouse Y"));
     *      Negativ = z.B. (Maus Nach Hinten) - Nach unten gucken
     *      Positiv = z.B. (Maus Nach Vorne) - Nach oben gucken
     *      
     * Input.GetAxis("Mouse X")
     *      Negativ 
    */

    public float check;
    //public float currentMoveSpeed;

    private Vector3 moveCharacter = Vector3.zero;

	// Use this for initialization
	void Start () 
    {
        
        controller = gameObject.GetComponent<CharacterController>();
        if (gravityUseSystemGravity)
        {
            gravity = Physics.gravity.y *-1;
        }
        else
        {
            gravity = gravityUser * -1;
        }
	}
	
	// Update is called once per frame
  

    

    void Update() {
        
        //float currentMoveSpeed = moveSpeed;
        float moveCurrentSpeed = moveWalkSpeed;

        CharacterController controller = GetComponent<CharacterController>();
        if (Input.GetAxis("Vertical") > 0  && Input.GetKey(KeyCode.LeftShift))
        { moveCurrentSpeed *= moveRunMultilikator; }
        
        if (controller.isGrounded) {
            moveCharacter = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveCharacter = transform.TransformDirection(moveCharacter);
            moveCharacter *= moveCurrentSpeed;
            if (Input.GetButton("Jump"))
                moveCharacter.y = moveJumpSpeed;
            
        }
        moveCharacter.y -= gravity * Time.deltaTime;
        controller.Move(moveCharacter * Time.deltaTime);


//        controller.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime);


        controller.transform.Rotate(0, Input.GetAxis("Mouse X") * lookRotationSpeed * Time.deltaTime, 0);
        lookCurrentRotationX += Input.GetAxis("Mouse Y") * lookRotationSpeed * Time.deltaTime;
        lookCurrentRotationX  = Mathf.Clamp(lookCurrentRotationX, lookMinRotationX, lookMaxRotationX);

        check = lookCurrentRotationX;

        playerCamera.transform.localEulerAngles = (new Vector3(-lookCurrentRotationX, 0, 0));
        //Debug.Log(lookRotateX);
        //Debug.Log(playerCamera.transform.localEulerAngles);


        //Camera.mainCamera.transform.Rotate(Input.GetAxis("Mouse Y") * lookRotationSpeed * Time.deltaTime* -1, 0, 0);
        //playerCamera.transform.Rotate(Input.GetAxis("Mouse Y") * lookRotationSpeed * Time.deltaTime * -1, 0, 0);

    }
 
}
