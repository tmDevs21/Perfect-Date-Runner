using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameMnager.gManager.GlasseNumber == 1)
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;

        }
        else
        {
            this.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }
}
