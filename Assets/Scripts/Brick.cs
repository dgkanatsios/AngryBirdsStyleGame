using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        Health -= col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

        if (Health <= 0) Destroy(this.gameObject);
    }

    public float Health = 70f;
}
