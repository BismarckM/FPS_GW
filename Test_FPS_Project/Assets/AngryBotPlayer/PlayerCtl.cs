using UnityEngine;
using System.Collections;

[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runFoward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}

public class PlayerCtl : MonoBehaviour {
    private Transform tr;
    private float moveSpeed = 5.0f;
    private float rotSpeed = 1.2f;
    private Vector3 moveDir;
    public Animation _animation;
    public Anim anim;

    private float beforePosition;


    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;


    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        _animation = GetComponentInChildren<Animation>();
        _animation.clip = anim.idle;
        _animation.Play();
	}
	
	// Update is called once per frame
	void Update () {
        //Touch touch = Input.GetTouch(0);
        //if (touch.phase == TouchPhase.Began)
        //{
        //    beforePosition = touch.position.x;
        //}
        //else
        //{
        //    tr.Rotate(Time.deltaTime * 3.0f * new Vector3(0, beforePosition - touch.position.x, 0));
        //    beforePosition = touch.position.x;
        //}

        float accelX = Input.acceleration.x;
        float accelY = -Input.acceleration.y;


        /*
        // ---------------------------------------------------------------------
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
        }
        else
        {
            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            // touch start
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(pos);

                if (Physics.Raycast(ray, out hit) && (hit.collider.tag == "Player"))
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, pos.z);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }

            // drag
            if (dragging && (touch.phase == TouchPhase.Moved))
            {
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;
            }

            // touch termination
            if (dragging && (touch.phase == TouchPhase.Ended) || (touch.phase == TouchPhase.Canceled))
            {
                dragging = false;
            }
        }
        // ----------------------------------------------------------------------------
        */

        // character movement for left and right
        if(accelX < -0.8f) {
            accelX = 0.0f;
        }
        else if(accelX < -0.15f)
        {
           
        }
        else if(accelX < 0.15f)
        {
            accelX = 0.0f;
        }
        else if(accelX < 0.8f)
        {

        }
        else
        {
            accelX = 0.0f;
        }

        // character movement for forward and backward
        if (accelY < 0.0f)
        {
            accelY = 0.0f;
        }
        else if (accelY < 0.65f)
        {
           
        }
        else if (accelY < 0.85f)
        {
            accelY = 0.0f;
        }
        else if (accelY < 1.0f)
        {
            accelY = -accelY + 0.5f;
        }
        else
        {
            accelY = 0.0f;
        }

        moveDir = (new Vector3(accelX, 0, accelY)).normalized;
        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);

        // for animation
        if (moveDir.x > 0.0f && moveDir.z >= 0.0f && moveDir.x > moveDir.z)
        {
            _animation.CrossFade(anim.runRight.name, 0.1f);
        }
        else if (moveDir.x >= 0.0f && moveDir.z > 0.0f && moveDir.x <= moveDir.z)
        {
            _animation.CrossFade(anim.runFoward.name, 0.1f);
        }
        else if (moveDir.x > 0.0f && moveDir.z <= 0.0f && moveDir.x > -moveDir.z)
        {
            _animation.CrossFade(anim.runRight.name, 0.1f);
        }
        else if (moveDir.x >= 0.0f && moveDir.z < 0.0f && moveDir.x <= -moveDir.z)
        {
            _animation.CrossFade(anim.runBackward.name, 0.1f);
        }
        else if (moveDir.x < 0.0f && moveDir.z >= 0.0f && -moveDir.x > moveDir.z)
        {
            _animation.CrossFade(anim.runLeft.name, 0.1f);
        }
        else if (moveDir.x <= 0.0f && moveDir.z > 0.0f && -moveDir.x <= moveDir.z)
        {
            _animation.CrossFade(anim.runFoward.name, 0.1f);
        }
        else if (moveDir.x < 0.0f && moveDir.z <= 0.0f && -moveDir.x > -moveDir.z)
        {
            _animation.CrossFade(anim.runLeft.name, 0.1f);
        }
        else if (moveDir.x <= 0.0f && moveDir.z < 0.0f && -moveDir.x <= -moveDir.z)
        {
            _animation.CrossFade(anim.runBackward.name, 0.1f);
        }
        else if (moveDir.x == 0.0f && moveDir.z == 0.0f)
        {
            _animation.CrossFade(anim.idle.name, 0.1f);
        }
        else
        {
            _animation.CrossFade(anim.idle.name, 0.1f);
        }
    }
}
