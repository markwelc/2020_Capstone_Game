using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    
    protected float speed; //the speed of the bullet
    public float damage; //the damage of the bullet
    protected float lifespan; //how long until the bullet vanishes

    public Collider source; //the guy that shot the bullet
    private Character targetCharacter;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bulletRigidbody = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifespan);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveBullet();
    }


    /**
     * move the bullet forward at the correct speed
     * use raycasting method to be sure to not go through things
     * gravity and bullet drop are not handled here, the unity engine handles those
     */
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

    /**
     * When we hit something, if it's a character, tell that character to take damage
     * also plays a sound
     */
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "enemyOverall")
        {
            //Debug.LogWarning(col.gameObject.name);
            if (source != col && col.gameObject.name != "player_entry_door")
            {
                Destroy(this.gameObject);
            }

            if (LayerMask.LayerToName(col.gameObject.layer) == "enemy" || LayerMask.LayerToName(col.gameObject.layer) == "player")
            {
                targetCharacter = col.gameObject.GetComponentInParent<Character>();
                if (targetCharacter)
                {
                    targetCharacter.playerHealthManager.TakeDamage(col.gameObject.tag, 1);
                    GameObject temp = new GameObject();
                    Instantiate(temp, bulletRigidbody.transform.position, Quaternion.identity);
                    FindObjectOfType<AudioManager>().Play(temp.gameObject, "gun impact");
                }
            }
        }

    }
}
