using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool moveAroundMousePosition;
    [SerializeField] private bool moveWithMousePosition;
    [SerializeField] private bool aimToMousePosition;
    [SerializeField] private GameObject testObj;
    private Vector3 targetPos;
    private Vector3 direction;
    private Animator animator;
    private Camera m_camera;
    private Rigidbody rb;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_camera = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       // Movement();
        if (moveAroundMousePosition)
        {
            AimToMousePosition();
            Movement();
            moveWithMousePosition = false;
        }
        if (moveWithMousePosition)
        {
            MoveWithMouse();
            moveAroundMousePosition = false;
        }
        if (aimToMousePosition)
        {
            if (Input.GetMouseButtonDown(0))
            {
            AimToMousePosition();
            }
            Movement();
        }

    }
    void Movement()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", direction.magnitude);
        if(direction.z != 0)
        {
            transform.position += transform.forward * direction.z* speed * Time.deltaTime;
        }
        if (direction.x != 0)
        {
            transform.position += transform.right * direction.x * speed * Time.deltaTime;
        }
    }
    void AimToMousePosition()
    {
        
        if (Physics.Raycast(CalculateMousePos(), out RaycastHit hitInfo))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
            if(canMove())
                transform.LookAt(target);
        }
    }
    Ray CalculateMousePos()
    {
        return  m_camera.ScreenPointToRay(Input.mousePosition);
    }

    void MoveWithMouse()
    {
        CalculateMousePos();
        AimToMousePosition();
        if (Input.GetMouseButton(0) && canMove())
        {
            animator.SetFloat("Speed", 1);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }
    private bool canMove()
    {
        if (Vector2.Distance(transform.position, target) >= 0.01f)
            return true;
        else return false;
    }
}
