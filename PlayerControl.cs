using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 7.0f;
    public float LookSpeed = 90.0f;
    public float JumpForce = 5.0f;
    private Camera cam = null;
    private Rigidbody rb = null;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.angularDrag = 10.0f; //dampen spin when hit
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //don't fall over

        //camera has to be child of player (position and initially look same way as player)
        cam = this.transform.GetComponentInChildren<Camera>();
        cam.transform.position = this.transform.position;
        cam.transform.rotation = this.transform.rotation;

        //hide and lock mouse to game screen (its going to control look around)
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // movement
        float horz = Input.GetAxis("Horizontal"); //wasd, arrows, joystick  -1 .. 1
        float vert = Input.GetAxis("Vertical");
        this.transform.Translate(Vector3.right * horz * MoveSpeed * Time.deltaTime); //strafe sideways
        this.transform.Translate(Vector3.forward * vert * MoveSpeed * Time.deltaTime); //back forth

        //look around (note: rotate the player for one axis, rotate the camera itself for the other axis)
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");
        this.transform.localRotation *= Quaternion.AngleAxis(mousex * LookSpeed * Time.deltaTime, Vector3.up);
        cam.transform.localRotation *= Quaternion.AngleAxis(mousey * LookSpeed * Time.deltaTime, Vector3.left);

        //jump
        if (Input.GetButtonDown("Jump") == true) //space
        {
            rb.AddRelativeForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
}