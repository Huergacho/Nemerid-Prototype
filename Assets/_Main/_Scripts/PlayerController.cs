using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    private Vector3 moveSpeed;
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
        Movement();
        MoveToMousePosition();
        
    }
    void Movement()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        animator.SetFloat("Speed", direction.magnitude);
        if(direction.z != 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (direction.x != 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }
    void MoveToMousePosition()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
            var dist = Vector2.Distance(transform.position, target);
            if(dist >= 1f)
                transform.LookAt(target);
        }
    }
}
