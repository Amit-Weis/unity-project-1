using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class birdGliding : MonoBehaviour
{
    public float currentSpeed = 3;
    public float maxSpeed = 100;
    public float minSpeed = 10;
    public float rotationSpeed = 10;
    public float flapFactor = 2;


    public float thrustFacter = 5;
    public float gravityFactor = 3;

    private Rigidbody2D rb;
    public GameObject birdSprite;
    public GameObject invertedBirdSprite;
    public Updraft Updraft;

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

        if (currentSpeed < 0 && thrustFromRotation > 0)
        {
            currentSpeed = -currentSpeed;
        }
        currentSpeed += thrustFromRotation + thrustFromGravity;
        currentSpeed = Mathf.Clamp(currentSpeed, (-maxSpeed*2)/3, maxSpeed);


        if (currentSpeed < minSpeed)
        {
            Vector2 force = -Vector2.down * currentSpeed;
            rb.AddForce(force);
        }
        else
        {

            Vector2 force = Vector2.right * currentSpeed;
            rb.AddRelativeForce(force);
            if (Input.GetKeyDown("a"))
            {
                rb.velocity = Vector2.up * flapFactor;

            }
        }



        Vector2 forward = rb.transform.forward;

        Debug.DrawLine(rb.position, rb.velocity + rb.position, Color.red);
    }

    private void RotationManager() 
    {
        float x = Input.GetAxis("Horizontal") * flapFactor * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, y);

        if ((transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z <= 270))
        {
            invertedBirdSprite.SetActive(true);
            birdSprite.SetActive(false);
        }
        else
        {
            birdSprite.SetActive(true);
            invertedBirdSprite.SetActive(false);
        }
    }


    public void inUpDraft(float windFactor) 
    {
    
    }

}
