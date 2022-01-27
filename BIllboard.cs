using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIllboard : MonoBehaviour
{

    [SerializeField]
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(GameMnager.gManager.chracterGrp.transform.position, transform.position);
        if (dist < 5.410384f)
        {

            Color color = this.GetComponent<Renderer>().material.color;
            if (color.a > 0.01f)
            {
                color.a -= 0.5f * Time.deltaTime;
            }

            this.GetComponent<Renderer>().material.color = color;

            // this.GetComponent<Renderer>().material.SetColor("Color", Color.red);
        }
    }
}
