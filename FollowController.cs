using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowController : MonoBehaviour
{


    [SerializeField]
    GameObject manCharcter;
    // Start is called before the first frame update



 

    void Start()
    {
        manCharcter.GetComponent<emove>().enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            manCharcter.GetComponent<emove>().enabled = true;
        }
      

    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
