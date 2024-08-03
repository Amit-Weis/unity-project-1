using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Bird_movment : MonoBehaviour
{
    public Rigidbody2D myrb;
    public float move_speed = 1;
    private float angle = 0;
    float r;
    private float mirror = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // The value of Input.GetAxis() is in the range -1 to 1
        mirror = (269 > angle && angle > 91) ? 180 : 0;
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        float old_angle = transform.eulerAngles.z;
        float translation_y = y * move_speed * Time.deltaTime;
        float translation_x = x * move_speed * Time.deltaTime;
        angle = (x ==  1 && y ==  0) ? 0    :
                (x == -1 && y ==  0) ? 180  :
                (x ==  0 && y ==  1) ? 90   :
                (x ==  0 && y == -1) ? 270  :
                (x ==  1 && y == -1) ? -45  :
                (x == -1 && y == -1) ? -135 :
                (x ==  1 && y ==  1) ? 45   :
                (x == -1 && y ==  1) ? 135  :
                angle;



        Vector3 rotationVector = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.Euler(rotationVector);

        transform.Translate(translation_x, translation_y, 0);
        float smooth_angle = Mathf.SmoothDampAngle(old_angle, angle, ref r, 0.1f);

        // Rotate the sprite to face the direction
        rotationVector = new Vector3(mirror, 0, smooth_angle);
        Debug.Log(mirror);
        transform.rotation = Quaternion.Euler(rotationVector);

    }
}
