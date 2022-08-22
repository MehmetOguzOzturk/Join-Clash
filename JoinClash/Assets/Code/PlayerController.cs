using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isRun , isFight;
    public float speed, swipeSpeed;
    public Animator anim;
    public float xPos ;
    public float xPos2 ;

    Vector2 actionPosition;
    
    float step;

    private void Start()
    {
        isFight = false;
    }
    void Update() 
    {
        xPos = 100000f;
        xPos2 = -100000f;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            
            if (transform.GetChild(i).transform.position.x < xPos)
            {
                xPos = transform.GetChild(i).transform.position.x;
            }
            if (transform.GetChild(i).transform.position.x > xPos2)
            {
                xPos2 = transform.GetChild(i).transform.position.x;
            }


        }
        
        float diffirance =Mathf.Abs( xPos - transform.position.x);
        float diffirance2 = Mathf.Abs(xPos2 - transform.position.x);

        if (!isFight)
        {
            if (Input.GetMouseButtonDown(0))
            {
                actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                isRun = true;
                anim.SetBool("Run", true);
            }

            if (isRun)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
                
            }
                

            if (Input.GetMouseButton(0))
            {

                step = (Input.mousePosition.x - actionPosition.x);

                transform.position += new Vector3(step * swipeSpeed, 0, 0) * Time.deltaTime;


                actionPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                Vector3 pos = transform.position;
                pos.x = Mathf.Clamp(transform.position.x, -4.6f+diffirance, 4.6f-diffirance2);
                transform.position = pos;

            }

            if (Input.GetMouseButtonUp(0))
            {
                isRun = false;
                anim.SetBool("Run", false);
            }


        }


    }
}
