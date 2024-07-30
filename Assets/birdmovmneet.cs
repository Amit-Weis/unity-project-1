using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdmovmneet : MonoBehaviour
{
    public Rigidbody2D myrb;
    public float move_speed = 1;
    private float angle = 0;
    float r;
    private float mirror = 0;
    public GameObject bird_sprite;
    public GameObject tracker;
    public GameObject bird_sprite_inverted;
    private float smooth_angle;
    private Vector3 rotationVector;
    private bool bird_inverted = false;
    public static int RoundAwayFromZero(float value)
    {
        if (value > 0)
        {
            return (int)Mathf.Ceil(value); // Round up if positive
        }
        else
        {
            return (int)Mathf.Floor(value); // Round down if negative
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // The value of Input.GetAxis() is in the range -1 to 1
        mirror = (269 > angle && angle > 91) ? 180 : 0;
        float old_angle = tracker.transform.eulerAngles.z;
        float y = RoundAwayFromZero(Input.GetAxis("Vertical"));
        float x = RoundAwayFromZero(Input.GetAxis("Horizontal"));
        Debug.Log(x);
        Debug.Log(y);
        float translation_y = y * move_speed * Time.deltaTime;
        float translation_x = x * move_speed * Time.deltaTime;
        angle = (x == 1 && y == 0) ? 0 :                    // Right
                (x == 1 && y == 1) ? 45 :                   // Up-Right
                (x == 0 && y == 1) ? 90 :                   // Up
                (x == 0 && y == -1) ? -90 :                  // Down
                (x == 1 && y == -1) ? -45 :                  // Down-Right
                (x == -1 && y == 1) ? 45+2*360 :                  // Up-Left
                (x == -1 && y == 0) ? 0+2*360 :                  // Left
                (x == -1 && y == -1) ? -45+2*360 :                 // Down-Left
                angle;                                       // Default case



        if (angle > 360)
        {
            if (bird_inverted == false)
            {
                old_angle = 180 - angle;
                bird_inverted = true;   
            }
            bird_sprite.SetActive(false);
            bird_sprite_inverted.SetActive(true);

            transform.Translate(translation_x, translation_y, 0);
            smooth_angle = Mathf.SmoothDampAngle(old_angle, angle, ref r, 0.1f);

            // Rotate the sprite to face the direction
            rotationVector = new Vector3(0, 180, smooth_angle);
            bird_sprite_inverted.transform.localRotation = Quaternion.Euler(rotationVector);
            tracker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, smooth_angle));

        }
        else
        {   
            if (bird_inverted)
            {
                old_angle = 180 - angle;
                bird_inverted = false;
            }
            bird_sprite.SetActive(true);
            bird_sprite_inverted.SetActive(false);
            transform.Translate(translation_x, translation_y, 0);
            smooth_angle = Mathf.SmoothDampAngle(old_angle, angle, ref r, 0.1f);

            // Rotate the sprite to face the direction
            rotationVector = new Vector3(0, 0, smooth_angle);
            bird_sprite.transform.localRotation = Quaternion.Euler(rotationVector);
            tracker.transform.rotation = Quaternion.Euler(new Vector3(0, 0, smooth_angle));

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        }

    }
}
