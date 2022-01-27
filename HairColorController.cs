using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }





    // Update is called once per frame
    void Update()
    {


        this.gameObject.GetComponent<SkinnedMeshRenderer>().material = GameMnager.gManager.hairMaterials[GameMnager.gManager.hairColorNumber];
    }


    public void ChnageHairColor()
    {


        //this.gameObject.GetComponent<SkinnedMeshRenderer>().material = GameMnager.gManager.hairMaterials[GameMnager.gManager.hairColorNumber];
        //            GameMnager.gManager.magicEffect.SetActive(false);
        //            GameMnager.gManager.magicEffect.SetActive(true);
        //            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
        //            Invoke("cancelHairAnimation", 0.1f);
          


      
    }


    void cancelHairAnimation()
    {
        //CancelInvoke("cancelHairAnimation");
        //GameMnager.gManager.activeAnimator.SetBool("isSpin", false);

    }
}
