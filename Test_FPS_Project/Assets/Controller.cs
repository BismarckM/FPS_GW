using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]
//[System.Serializable]
//public class Anim
//{
//    public AnimationClip idle;
//    public AnimationClip runFoward;
//    public AnimationClip runBackward;
//    public AnimationClip runRight;
//    public AnimationClip runLeft;
//}

public class Controller : MonoBehaviour {

    private float beginPosition;
    private float currentPosition;

    public Image fireButton;
    //public GameObject bullet;
    public Transform firePos;

    public MeshRenderer muzzleFlash;

    public AudioClip fireSfx;
    private AudioSource source = null;
    
    public Transform tr;
    //private float moveSpeed = 5.0f;
    private float rotSpeed = 3.0f;

    public Touch touch;

    //private Vector3 moveDir;
    //public Anim anim;
    //public Animation _animation;

    //public Vector3 forceVec;
    //public Rigidbody rb;

    // Use this for initialization
    void Start () {
        // fire effect
        muzzleFlash.enabled = false;
        /*
         for joystick
        originPos = Stick.transform.position;
        StickRadius = Stick.rectTransform.sizeDelta.x / 2;

         for animation
        _animation = tr.GetComponentInChildren<Animation>();
        _animation.clip = anim.idle;
        _animation.Play();

         for gyroscope sensor
        Input.gyro.enabled = true;
        rb = GetComponent<Rigidbody>();

         for sound
        */
        source = GetComponent<AudioSource>();
    }

    /*
    public void OnClick()
    {
        Touch touch = Input.GetTouch(0);
        if(fireButton != null)
        {
            Fire();
        }
    }
    */

    /*
    void Fire()
    {
        CreateBullet();
        StartCoroutine(this.ShowMuzzleFlash());
        source.PlayOneShot(fireSfx, 0.9f);
    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
    }

    IEnumerator ShowMuzzleFlash()
    {
        float scale = Random.Range(0.3f, 0.5f);
        muzzleFlash.transform.localScale = Vector3.one * scale;
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.08f));
        muzzleFlash.enabled = false;
    }
    */


    
    public void OnBeginDrag()
    {
        touch = Input.GetTouch(0);
        beginPosition = touch.position.x;
    }

    public void OnDrag()
    {
        touch = Input.GetTouch(0);
        Vector3 rotDir = Vector3.zero;
        rotDir.y = touch.position.x - beginPosition;

        //if (rotDir.sqrMagnitude > 1)
        //{
        //    rotDir.Normalize();
        //}
        tr.Rotate(rotDir * Time.deltaTime * rotSpeed);
        // tr.Rotate(Time.deltaTime * rotSpeed * Vector3.up);
        // tr.RotateAround(tr.transform.position, Vector3.up, beginPosition - touch.position.x);
        beginPosition = touch.position.x;
        //Debug.Log(beginPosition + ", " + rotDir);
    }


    //public void OnDrag()
    //{
    //    if(Stick == null)
    //    {
    //        return;
    //    }

    //    Touch touch = Input.GetTouch(0);
    //    Vector3 dir = (new Vector3(touch.position.x, touch.position.y, originPos.z) - originPos).normalized;
    //    moveDir = new Vector3(touch.position.x - originPos.x, 0, touch.position.y - originPos.y);
    //    float touchAreaRadius = Vector3.Distance(originPos, new Vector3(touch.position.x, touch.position.y, originPos.z));
    //    if(touchAreaRadius > StickRadius)
    //    {
    //        Stick.rectTransform.position = originPos + (dir * StickRadius);
    //    }
    //    else
    //    {
    //        Stick.rectTransform.position = touch.position;
    //    }
    //}

    //public void OnEndDrag()
    //{
    //    if(Stick != null)
    //    {
    //        Stick.rectTransform.position = originPos;
    //        moveDir = Vector3.zero;
    //    }
    //}

    
        //// 2016-5-30 using acceleration for movement
        //float accelX = Input.acceleration.x;
        //float accelY = -Input.acceleration.y;

        //// character movement for left and right
        //if(accelX > -0.15f && accelX < 0.15f)
        //{
        //    accelX = 0.0f;
        //}
        //else if(accelX > 0.8f || accelX < -0.8f)
        //{
        //    accelX = 0.0f;
        //}

        //// character movement for forward and backward
        //if(accelY < 0.0f)
        //{
        //    accelY = 0.0f;
        //}
        //else if(accelY < 0.65f)
        //{

        //}
        //else if(accelY < 0.85f)
        //{
        //    accelY = 0.0f;
        //}
        //else if(accelY < 1.0f)
        //{
        //    accelY = -accelY + 0.5f;
        //}
        //else
        //{
        //    accelY = 0.0f;
        //}

        //moveDir = (new Vector3(accelX, 0, accelY)).normalized;
        //tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        // Debug.Log(moveDir);

        //Touch touch = Input.GetTouch(0);
        //Vector3 rotateDir = (new Vector3(touch.position.y, 0, 0)).normalized;
        //tr.Rotate(rotateDir * Time.deltaTime * rotSpeed);

        // rb.AddForce(Input.gyro.userAcceleration.y * forceVec);
        //Vector3 test = new Vector3(0, 0, Input.gyro.userAcceleration.y);
        //tr.Translate(test);

        //// for rotation
        //Vector3 dir = Vector3.zero;

        //// dir.x = -Input.acceleration.y;
        //dir.x = Input.acceleration.x;
        //if(dir.sqrMagnitude > 1)
        //{
        //    dir.Normalize();
        //}
        //// tr.Translate(dir * Time.deltaTime * rotSpeed);
        //// tr.Rotate(dir * Time.deltaTime * rotSpeed);

        // for animation
        //if (moveDir.x > 0.0f && moveDir.z >= 0.0f && moveDir.x > moveDir.z)
        //{
        //    _animation.CrossFade(anim.runRight.name, 0.1f);
        //}
        //else if (moveDir.x >= 0.0f && moveDir.z > 0.0f && moveDir.x <= moveDir.z)
        //{
        //    _animation.CrossFade(anim.runFoward.name, 0.1f);
        //}
        //else if (moveDir.x > 0.0f && moveDir.z <= 0.0f && moveDir.x > -moveDir.z)
        //{
        //    _animation.CrossFade(anim.runRight.name, 0.1f);
        //}
        //else if (moveDir.x >= 0.0f && moveDir.z < 0.0f && moveDir.x <= -moveDir.z)
        //{
        //    _animation.CrossFade(anim.runBackward.name, 0.1f);
        //}
        //else if (moveDir.x < 0.0f && moveDir.z >= 0.0f && -moveDir.x > moveDir.z)
        //{
        //    _animation.CrossFade(anim.runLeft.name, 0.1f);
        //}
        //else if (moveDir.x <= 0.0f && moveDir.z > 0.0f && -moveDir.x <= moveDir.z)
        //{
        //    _animation.CrossFade(anim.runFoward.name, 0.1f);
        //}
        //else if (moveDir.x < 0.0f && moveDir.z <= 0.0f && -moveDir.x > -moveDir.z)
        //{
        //    _animation.CrossFade(anim.runLeft.name, 0.1f);
        //}
        //else if (moveDir.x <= 0.0f && moveDir.z < 0.0f && -moveDir.x <= -moveDir.z)
        //{
        //    _animation.CrossFade(anim.runBackward.name, 0.1f);
        //}
        //else if (moveDir.x == 0.0f && moveDir.z == 0.0f)
        //{
        //    _animation.CrossFade(anim.idle.name, 0.1f);
        //}
        //else
        //{
        //    _animation.CrossFade(anim.idle.name, 0.1f);
        //}
}