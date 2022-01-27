using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairControler : MonoBehaviour
{

    public static HairControler hairControler;
    [SerializeField]
    GameObject [] hairs;
    // Start is called before the first frame update
    void Start()
    {
        if (hairControler == null)
            hairControler = this;


      //  changeHair(GameMnager.gManager.hairNumber);
    }

    //public void changeHair(int x)
    //{
       
    //            for (int i = 0; i < hairs.Length;i++)
    //            {
    //                if (i == x)
    //                {
    //                    hairs[i].SetActive(true);
    //                }
    //                else
    //                {
    //                    hairs[i].SetActive(false);
    //                }
    //            }

        
    //    }



    // Update is called once per frame



    void Update()
    {


        for (int i = 0; i < hairs.Length; i++)
        {
            if (i == GameMnager.gManager.hairNumber)
            {
                if (!hairs[i].activeSelf)
                {

                    GameMnager.gManager.magicEffect.SetActive(false);
                    GameMnager.gManager.magicEffect.SetActive(true);
                    hairs[i].SetActive(true);
                    //GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
                    //Invoke("cancelHairAnimation", 0.1f);
                }
            }
            else
            {
                hairs[i].SetActive(false);
                
            }
        }
    }


    void cancelHairAnimation()
    {
        //CancelInvoke("cancelHairAnimation");
        //GameMnager.gManager.activeAnimator.SetBool("isSpin", false);

    }
}
