using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private void Update()
    {
        if (this.transform.position.y < -15f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //destroy bullet
        Destroy(collision.gameObject);
    }
}
