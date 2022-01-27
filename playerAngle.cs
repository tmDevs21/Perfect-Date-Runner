using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAngle : MonoBehaviour
{

    private Touch touch;

    float smooth = 5.0f;
    float tiltAngle = 0f;
    public float playerRotateAngle = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       

        if (this.transform.localRotation.z <= -0.2)
        {
            Debug.Log(this.transform.localRotation.z);

            Debug.Log(PlayerController.playerController.charcters.transform.rotation.z);
            //   PlayerController.playerController.charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
              PlayerController.playerController.GetComponent<CapsuleCollider>().enabled = false;
            PlayerController.playerController.GetComponent<BoxCollider>().enabled = false;

            GameMnager.gManager.LevelFail();
            GameMnager.gManager.activeAnimator.SetBool("isGround", false);
        }
        if (this.transform.localRotation.z >= 0.2)
        {
            Debug.Log(this.transform.localRotation.z);

            Debug.Log(PlayerController.playerController.charcters.transform.rotation.z);
           //    PlayerController.playerController.charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
           PlayerController.playerController.GetComponent<CapsuleCollider>().enabled = false;
            PlayerController.playerController.GetComponent<BoxCollider>().enabled = false;
            GameMnager.gManager.activeAnimator.SetBool("isGround", false);

            GameMnager.gManager.LevelFail();
        }
    }


    /*private void FixedUpdate()
    {
            if (GameManager.gameManager.victoryFlag == 0)
            {
                if (Input.touchCount > 0 && Input.touchCount <= 1)
                {
                    touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        Quaternion target = Quaternion.Euler(0, GameManager.gameManager.tiltAngle + touch.deltaPosition.x * 2, 0);
                       transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * GameManager.gameManager.smooth);
                        //rb.velocity = new Vector3(transform.position.x + touch.deltaPosition.x * .02f, transform.position.y, transform.position.z + Time.fixedDeltaTime);
                    }

                    else
                    {
                          Quaternion target = Quaternion.Euler(Player._Instance.transform.position.x, 0, Player._Instance.transform.position.x);
                          transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * GameManager.gameManager.smooth);
                    }
                }

            }
            else
            {
                Quaternion target = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * GameManager.gameManager.smooth);
            }
        

    }*/

    private void FixedUpdate()
    {
   
            

  


    }
}
