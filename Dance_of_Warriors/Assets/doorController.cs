using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    private Animator doorAnim;
    private AudioSource openSound;
    private bool open, close;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        if(doorAnim == null)
        {
            doorAnim = this.GetComponentInParent<Animator>();
        }
        openSound = this.GetComponentInParent<AudioSource>();
        //doorAnim.SetBool("character_nearby", true);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && !open)
        {
            // Just to make sure we dont play is already there
            close = false;
            open = true;
           // openSound.Stop();
            if(openSound != null)
                openSound.PlayOneShot(openSound.clip);
            doorAnim.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 8 && !close )
        {
            close = true;
            open = false;
            //openSound.Stop();
            if(openSound != null)
                openSound.PlayOneShot(openSound.clip);
            doorAnim.SetBool("character_nearby", false);
        }
    }

}
