using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isRun , isFight;
    public float speed, swipeSpeed;
    public Animator anim;

    Vector2 actionPosition;
    float step;

    private void Start()
    {
        isFight = false;
    }
    void Update()
    {
        if (!isFight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                isRun = true;
                anim.SetBool("Run", true);
            }

            if (isRun)
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);

            if (Input.GetMouseButton(0))
            {

                step = (Input.mousePosition.x - actionPosition.x);

                transform.position += new Vector3(step * swipeSpeed, 0, 0) * Time.deltaTime;


                actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            if (Input.GetMouseButtonUp(0))
            {
                isRun = false;
            }
        }


    }
}
