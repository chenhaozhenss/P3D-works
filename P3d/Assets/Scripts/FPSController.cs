using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    private CharacterController cc;
    public float speed = 3.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public Door door;
    internal bool isOpen;
    private AudioSource audioDoor;
    public Transform childX;
    private Transform child;
    public GameObject texiao;

    void Start()
    {
        cc=GetComponent<CharacterController>();
        audioDoor = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instance.isStart) return;
        transform.Rotate(0, Input.GetAxis("Mouse X") * 2, 0);
        childX.Rotate(-Input.GetAxis("Mouse Y") * 2, 0, 0);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (door.pick != null && Vector3.Distance(transform.position, door.pick.transform.position) <= 2f)
            {
                door.pick.GetComponent<Rigidbody>().useGravity = false;
                door.pick.GetComponent<BoxCollider>().enabled = false;
                GameObject go = Instantiate(texiao);
                go.transform.position = door.pick.transform.position;
                door.pick.transform.SetParent(transform);
                child = door.pick.transform;
                door.pick.transform.localPosition = new Vector3(door.pick.transform.localPosition.x, 0.2f, door.pick.transform.localPosition.z);
                door.pick.transform.LookAt(new Vector3(transform.position.x, door.pick.transform.position.y, transform.position.z));
            }
        }

        if (child != null && Input.GetMouseButtonDown(0))
        {
            door.pick.transform.SetParent(null);
            child = null;
            door.pick.GetComponent<BoxCollider>().enabled = true;
            Rigidbody rig = door.pick.GetComponent<Rigidbody>();
            rig.useGravity = true;
            rig.AddForce(-door.pick.transform.forward * 500);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(transform.position, door.transform.position) <= 2f)
            {
                isOpen = !isOpen;
                if (isOpen)
                {
                    door.Open();
                }
                else
                {
                    door.Close();
                }
                audioDoor.Play();
            }
        }
        if (cc.isGrounded)
        {
            
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);


    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("door"))
        {
            hit.rigidbody.AddForce(transform.forward * speed);
        }
    }
}
