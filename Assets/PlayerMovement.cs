using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float acceleration = 80.0f;
    [SerializeField] private float maxVelocity = 10.0f;
    [SerializeField] private LayerMask lookMask;
    [SerializeField] private Camera cam;
    
    private Vector3 motion = Vector3.zero;
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moves the player
        motion.x = Input.GetAxisRaw("Horizontal");
        motion.z = Input.GetAxisRaw("Vertical");
        motion.Normalize();
        
        rb.velocity += motion * acceleration * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        
        // Look to mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, lookMask))
        {
            transform.LookAt(hit.point);
        }
    }
}
