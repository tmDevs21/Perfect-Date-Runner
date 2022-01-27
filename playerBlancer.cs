using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlancer : MonoBehaviour
{
    [SerializeField]
    float plasvaluse;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       




        if (PlayerController.playerController.isBlance && PlayerController.playerController.isTouch == false)
        {
            if ( PlayerController.playerController.slideValue == 0)
            {



              

                if (PlayerController.playerController.charcters.transform.rotation.z <= -35.0f)
                {
                    //   PlayerController.playerController.charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    GameMnager.gManager.activeAnimator.SetBool("isGround", false);
                    GameMnager.gManager.LevelFail();
                }   
                else
                {
                    PlayerController.playerController.charcters.transform.Rotate(new Vector3(0, 0, -30f) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
                }
            }




             if (PlayerController.playerController.slideValue >= 1)
            {
                plasvaluse -= 1.9f * Time.deltaTime;



                if (PlayerController.playerController.charcters.transform.rotation.z >= 35.0f)
                {
                    //  PlayerController.playerController.charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    GameMnager.gManager.activeAnimator.SetBool("isGround", false);
                 //   PlayerController.playerController.GetComponent<CapsuleCollider>().enabled = false;

                    GameMnager.gManager.LevelFail();
                }
                else
                {
                    PlayerController.playerController.charcters.transform.Rotate(new Vector3(0, 0, 30f) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
                }

            }
        }
        
    }
}
