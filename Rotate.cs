using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //  transform.Rotate(new Vector3(0f, 50f, 0f) * Time.deltaTime);


        Vector3 vector3Variable = new Vector3(0, 5.0f, 0);

        transform.Rotate (vector3Variable);

    }
}
