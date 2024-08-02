using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class birdGliding : MonoBehaviour
{
    public float currentSpeed = 3;
    public float maxSpeed = 100;
    public float minSpeed = 10;
    public float rotationSpeed = 10;
    public float flapHeight = 2;


    public float thrustFacter = 5;
    public float gravityFactor = 3;

    private Rigidbody2D rb;
    public GameObject birdSprite;
    public GameObject invertedBirdSprite;
    public Updraft Updraft;

    private bool alive = true;
    public UnityEvent<float, float> currentSpeedAnnoucment;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            RotationManager();
        }

    }

    private void FixedUpdate()
    {
        if (alive)
        {
            GlidingMovment();
        }
        if (Input.GetKeyDown("space"))
        {
            transform.position = new Vector3((float)-101.3, (float)0.9, 0);
            currentSpeed = 3;
            rb.velocity = Vector2.zero;
            transform.rotation = new Quaternion(0, 0, 0, 0);
            rb.gravityScale = 0;
            rb.angularVelocity = 0;
            alive = true;
        }
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
        currentSpeed = Mathf.Clamp(currentSpeed, (-maxSpeed * 2) / 3, maxSpeed);


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

        currentSpeedAnnoucment.Invoke(currentSpeed, maxSpeed);
        Debug.DrawLine(rb.position, rb.velocity + rb.position, Color.red);
    }

    private void RotationManager()
    {
        float x = Input.GetAxis("Horizontal");
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
        rb.AddForce(Vector2.up * windFactor);
    }

    public void death()
    {
        if (alive)
        {
            rb.velocity = new Vector2(1 * currentSpeed, 1 * gravityFactor);
            rb.gravityScale = 10;
            rb.angularVelocity = 10 * currentSpeed;
            Debug.DrawLine(rb.position, rb.velocity + rb.position, Color.red);
        }
        alive = false;
    }
}
