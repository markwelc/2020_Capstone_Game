using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    private Animator doorAnim;
    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        //doorAnim.SetBool("character_nearby", true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            doorAnim.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            doorAnim.SetBool("character_nearby", false);
        }
    }

}
