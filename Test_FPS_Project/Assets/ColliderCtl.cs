using UnityEngine;
using System.Collections;

public class ColliderCtl : MonoBehaviour {
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET")
        {
            Destroy(coll.gameObject);
        }
    }
}
