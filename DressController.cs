using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressController : MonoBehaviour
{
    public static DressController dressController;
    [SerializeField]
    GameObject[] dresses;
    // Start is called before the first frame update
    void Start()
    {
        if (dressController == null)
            dressController = this;


        //  changeHair(GameMnager.gManager.hairNumber);
    }

    public void changeHair(int x)
    {

        for (int i = 0; i < dresses.Length; i++)
        {
            if (i == x)
            {
                dresses[i].SetActive(true);
            }
            else
            {
                dresses[i].SetActive(false);
            }
        }


    }



    // Update is called once per frame
    void Update()
    {


        for (int i = 0; i < dresses.Length; i++)
        {
            if (i == GameMnager.gManager.dressNumber)
            {
                if (!dresses[i].activeSelf)
                {

                    //GameMnager.gManager.magicEffect.SetActive(false);
                    //GameMnager.gManager.magicEffect.SetActive(true);

                    dresses[i].SetActive(true);
                    //GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
                    //Invoke("cancelHairAnimation", 0.1f);
                }


            }
            else
            {
                dresses[i].SetActive(false);

            }
        }
    }


    void cancelHairAnimation()
    {
        //CancelInvoke("cancelHairAnimation");
        //GameMnager.gManager.activeAnimator.SetBool("isSpin", false);

    }
}
