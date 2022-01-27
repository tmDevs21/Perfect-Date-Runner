using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDressChange : MonoBehaviour
{

    public GameObject[] rndDress;

    // Start is called before the first frame update
    void Start()
    {

        int i;
        float rndNumber = Random.RandomRange(0, rndDress.Length);
        i = (int)rndNumber;


        for (int x = 0; x < rndDress.Length; x++)
        {
            if (x == i)
            {
                rndDress[x].SetActive(true);
            }
            else
            {
                rndDress[x].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
