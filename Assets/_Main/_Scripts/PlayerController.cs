using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float distanceToDetectMouse;
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
        if (direction != Vector3.zero)
        {
            transform.position += new Vector3(direction.x * speed, 0, direction.z * speed) *Time.deltaTime;
        }
    }
    void Look()
    {
        if (direction != Vector3.zero)
        {

            var relative = (transform.position + direction) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = rot;
        }
    }
    void MoveToMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            target = hitInfo.point;
            target.y = transform.position.y;
            var distance = Vector3.Distance(transform.position, hitInfo.point);
            if (distance >= 1f)
                transform.LookAt(target);
        }
    }
}
