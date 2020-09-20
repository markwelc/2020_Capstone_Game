using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    
    protected float speed;
    public float damage;

    public Collider source;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletRigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveBullet();
    }


    void moveBullet()
    {
        Vector3 direcAndMag = transform.up * speed * Time.deltaTime; //make a vector pointing forwards (the transform is rotated) and scale it

        Ray ray = new Ray(transform.position, direcAndMag); //shoot a ray from current position in direction of travel
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, (direcAndMag * Time.deltaTime).magnitude)) //if the ray didn't hit anything within the range that we're moving
            bulletRigidbody.MovePosition((Vector3)transform.position + direcAndMag * Time.deltaTime); //go ahead and move
        else
        {
            bulletRigidbody.MovePosition(hit.point);//otherwise, move to where the thing we hit was
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (source != col)
            Destroy(this.gameObject);
        Debug.Log("source = " + source);
        Debug.Log("col = =" + col);
    }
}
