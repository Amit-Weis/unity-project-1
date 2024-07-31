using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class birdGliding : MonoBehaviour
{
    public float currentSpeed = 3;
    public float maxSpeed = 100;
    public float minSpeed = 10;
    public float rotationSpeed = 10;


    public float thrustFacter = 5;
    public float gravityFactor = 3;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotationManager();
    }

    private void FixedUpdate()
    {
        GlidingMovment();    
    }

    private void GlidingMovment() 
    {
        rb.AddForce(Vector2.down * gravityFactor);


        float rotationInRads = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float thrustFromRotation = Mathf.Clamp(-Mathf.Sin(rotationInRads), 0, 1) * thrustFacter;
        float thrustFromGravity = Mathf.Clamp(-Mathf.Sin(rotationInRads), -1, 0) * gravityFactor;

        currentSpeed += thrustFromRotation + thrustFromGravity;
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        if (currentSpeed < minSpeed)
        {
            Vector2 force = -Vector2.down * currentSpeed;
            rb.AddForce(force);
        }
        else
        {
            Vector2 force = Vector2.right * currentSpeed;
            rb.AddRelativeForce(force); 
        }


        Vector2 forward = rb.transform.forward;

        Debug.DrawLine(rb.position, rb.velocity + rb.position, Color.red);
    }

    private void RotationManager() 
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, y);
    }
}
