using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour {

    public float Health = 150f;
    public Sprite SpriteShownWhenHurt;
    private float HalfHealth;
	// Use this for initialization
	void Start () {
        HalfHealth = Health / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        //if we are hit by a bird
        if (col.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
        }
        else //we're hit by something else
        {

            Health -= col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

            if (Health < HalfHealth)
            {
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }
            if (Health <= 0) Destroy(this.gameObject);
        }
    }
}
