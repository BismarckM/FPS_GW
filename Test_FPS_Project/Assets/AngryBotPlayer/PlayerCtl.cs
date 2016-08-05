using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;

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
    private Rigidbody rb;
    private float moveSpeed = 5.0f;
    private Vector3 moveDir;
    public Animation _animation;
    public Anim anim;

    private PhotonView pv = null;
    public Transform camPivot;

    private Vector3 curPos = Vector3.zero;
    private Quaternion curRot = Quaternion.identity;
    private int animationNumber;

    private float beforeTouch;
    private float rotSpeed = 3.0f;

    // Use this for initialization
    void Awake () {
        _animation = GetComponent<Animation>();
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        
        _animation.clip = anim.idle;
        _animation.Play();

        pv.synchronization = ViewSynchronization.UnreliableOnChange;
        pv.ObservedComponents[0] = this;

        Debug.Log(pv.isMine);
        
        if(pv.isMine)
        {
            Camera.main.GetComponent<SmoothFollow>().target = camPivot;
            rb.centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);
        }
        else
        {
            rb.isKinematic = true;
        }

        curPos = tr.position;
        curRot = tr.rotation;
        animationNumber = 0;
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.isWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
            stream.SendNext(animationNumber);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
            curRot = (Quaternion)stream.ReceiveNext();
            animationNumber = (int)stream.ReceiveNext();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.isMine)
        {
            float accelX = Input.acceleration.x;
            float accelY = -Input.acceleration.y;

            // character movement for left and right
            if (accelX < -0.8f)
            {
                accelX = 0.0f;
            }
            else if (accelX < -0.15f)
            {

            }
            else if (accelX < 0.15f)
            {
                accelX = 0.0f;
            }
            else if (accelX < 0.8f)
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
                animationNumber = 3;
            }
            else if (moveDir.x >= 0.0f && moveDir.z > 0.0f && moveDir.x <= moveDir.z)
            {
                _animation.CrossFade(anim.runFoward.name, 0.1f);
                animationNumber = 1;
            }
            else if (moveDir.x > 0.0f && moveDir.z <= 0.0f && moveDir.x > -moveDir.z)
            {
                _animation.CrossFade(anim.runRight.name, 0.1f);
                animationNumber = 3;
            }
            else if (moveDir.x >= 0.0f && moveDir.z < 0.0f && moveDir.x <= -moveDir.z)
            {
                _animation.CrossFade(anim.runBackward.name, 0.1f);
                animationNumber = 2;
            }
            else if (moveDir.x < 0.0f && moveDir.z >= 0.0f && -moveDir.x > moveDir.z)
            {
                _animation.CrossFade(anim.runLeft.name, 0.1f);
                animationNumber = 4;
            }
            else if (moveDir.x <= 0.0f && moveDir.z > 0.0f && -moveDir.x <= moveDir.z)
            {
                _animation.CrossFade(anim.runFoward.name, 0.1f);
                animationNumber = 1;
            }
            else if (moveDir.x < 0.0f && moveDir.z <= 0.0f && -moveDir.x > -moveDir.z)
            {
                _animation.CrossFade(anim.runLeft.name, 0.1f);
                animationNumber = 4;
            }
            else if (moveDir.x <= 0.0f && moveDir.z < 0.0f && -moveDir.x <= -moveDir.z)
            {
                _animation.CrossFade(anim.runBackward.name, 0.1f);
                animationNumber = 2;
            }
            else if (moveDir.x == 0.0f && moveDir.z == 0.0f)
            {
                _animation.CrossFade(anim.idle.name, 0.1f);
                animationNumber = 0;
            }
            else
            {
                _animation.CrossFade(anim.idle.name, 0.1f);
                animationNumber = 0;
            }

            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                beforeTouch = touch.position.x;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                tr.Rotate(0, (touch.position.x - beforeTouch) * Time.deltaTime * rotSpeed, 0);
                beforeTouch = touch.position.x;
            }
        }
        else
        {
            switch(animationNumber)
            {
                case 0:
                    _animation.CrossFade(anim.idle.name, 0.1f);
                    break;
                case 1:
                    _animation.CrossFade(anim.runFoward.name, 0.1f);
                    break;
                case 2:
                    _animation.CrossFade(anim.runBackward.name, 0.1f);
                    break;
                case 3:
                    _animation.CrossFade(anim.runRight.name, 0.1f);
                    break;
                case 4:
                    _animation.CrossFade(anim.runLeft.name, 0.1f);
                    break;
                default :
                    _animation.CrossFade(anim.idle.name, 0.1f);
                    break;
            }
            tr.position = Vector3.Lerp(tr.position, curPos, Time.deltaTime * 3.0f);
            tr.rotation = Quaternion.Slerp(tr.rotation, curRot, Time.deltaTime * 3.0f);
        }
    }
}
