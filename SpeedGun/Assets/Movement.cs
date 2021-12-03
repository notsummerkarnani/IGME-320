using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 position;
    private Vector3 velocity;
    private float thrust;
    Camera cam;
    public GameObject projectile;
    private Rigidbody rb;

    private const float AXISDEAD = 0.1f;
    public bool cooldown = false;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        position = Vector3.zero;
        velocity = Vector3.zero;
        thrust = 10;
        cam = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
        Physics.gravity = -Vector3.up;
        rb.AddRelativeForce(Vector3.up);
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, position, 1);
    }

    // Update is called once per frame
    void Update()
    {
        position = gameObject.transform.position;
        cam = Camera.main;
        
        ProcessKey();

        if ((Input.GetJoystickNames().Length > 0))        //checks if gamepad is connected
            ProcessGamePad();

        cam.transform.rotation = Quaternion.Euler(velocity.z / 10, this.transform.localEulerAngles.y, velocity.x / 10);     //check rotations

        rb.velocity = velocity;
        if (rb.angularVelocity != Vector3.zero)
            rb.angularVelocity = Vector3.zero;
        if(this.transform.rotation.eulerAngles.x != 0 || this.transform.rotation.eulerAngles.z != 0)
        {
            transform.rotation = Quaternion.Euler(0, this.transform.localEulerAngles.y, 0);
        }
        if (cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
        if (cooldownTimer >= 3)
        {
            cooldown = false;
            cooldownTimer = 0;
        }
    }

    void ProcessKey()
    {
        velocity = velocity.normalized;
        int scalar = 1;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            scalar = 2;
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.forward * thrust * scalar; 
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity -= transform.right * thrust * scalar;
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.forward * thrust * scalar;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += transform.right * thrust * scalar;
        }
        if (Input.GetKey(KeyCode.E))
        {
            velocity += transform.up * 5;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            velocity -= transform.up * 5;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!cooldown)
            {
                Instantiate(projectile, cam.transform.position, cam.transform.rotation);
                velocity -= transform.forward * thrust;
                cooldown = true;
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
            Debug.LogError("end");
        }
        this.transform.Rotate(0, Input.GetAxis("Mouse X") * 5, 0);
    }

    void ProcessGamePad()
    {
        float leftHorizontalAxis = Input.GetAxis("Horizontal");
        float leftVerticalAxis = Input.GetAxis("Vertical");
        float rightHorizontalAxis = Input.GetAxis("Mouse X") * 10;
        
        if(leftHorizontalAxis>AXISDEAD || leftHorizontalAxis < -AXISDEAD)
            velocity += transform.right * leftHorizontalAxis * thrust * 2;      //left and right movement

        if (leftVerticalAxis > AXISDEAD || leftVerticalAxis < -AXISDEAD)
            velocity += transform.forward * leftVerticalAxis * thrust * 2;      //forward and back movement

        if (rightHorizontalAxis > AXISDEAD || rightHorizontalAxis < -AXISDEAD)
            this.transform.Rotate(0, rightHorizontalAxis, 0);      //rotation
    }


}
