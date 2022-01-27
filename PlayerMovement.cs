using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private bool isBlance, isTouch, isDouble, isStay;
    private Touch touch;

    public GameObject charcters, moveSlide;
    public PathSystem.PathSystem_Object PathSystem_Object;
    [SerializeField]
    public int follower, slideValue;

    // Start is called before the first frame update
    void Start()
    {
        isStay = false;
        isDouble = false;
        isBlance = false;
        
        if (!moveSlide.activeSelf)
        {
            moveSlide.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        GameMnager.gManager.activeAnimator.SetBool("isDouble", isDouble);

        if (GameMnager.gManager.isLevelFinsih == false && isStay == false)
        {
            if (Input.touchCount > 0 && Input.touchCount <= 1)
            {
                touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Ended)
                {
                    if (!isBlance)
                    {
                        // charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    isTouch = false;
                    //   Quaternion target = Quaternion.Euler(0, -1, 0);
                    //  GameMnager.gManager.chracterGrp.transform.rotation = Quaternion.Slerp(GameMnager.gManager.chracterGrp.transform.rotation, target, Time.deltaTime * 10);
                }
                if (touch.phase == TouchPhase.Began)
                {
                    if (!isBlance)
                    {
                        // charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }

                    //   isTouch = true;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    isTouch = true;

                    if (moveSlide.activeSelf)
                    {
                        GameMnager.gManager.activeAnimator.SetBool("isRun", true);

                        moveSlide.SetActive(false);
                        if (!PathSystem_Object.enabled)
                        {
                            PathSystem_Object.enabled = true;
                        }
                    }


                    if (touch.deltaPosition.x > 0)
                    {

                        if (GameMnager.gManager.transform.rotation.y < 25.0f)
                        {
                            // GameMnager.gManager.chracterGrp.transform.Rotate(0.0f, 50 * Time.deltaTime * GameMnager.gManager.playerSpeed, 0.0f, Space.Self);
                        }


                        if (isBlance)
                        {
                            //  charcters.transform.Rotate(new Vector3(0, 0, -50f) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);

                            slideValue = 1;
                        }
                        else
                        {
                            //  rb.velocity = (Vector3.right * touch.deltaPosition.x * GameMnager.gManager.playerSpeed * Time.deltaTime);
                            transform.Translate(new Vector3(touch.deltaPosition.x, 0, 0) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
                            //  charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }




                    }

                    if (touch.deltaPosition.x < 0)
                    {
                        slideValue = 0;
                        if (GameMnager.gManager.transform.rotation.y > -25.0f)
                        {
                            //GameMnager.gManager.chracterGrp.transform.Rotate(0.0f, -50 * Time.deltaTime * GameMnager.gManager.playerSpeed, 0.0f, Space.Self);
                        }

                        if (isBlance)
                        {
                            charcters.transform.Rotate(new Vector3(0, 0, 50f) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
                        }
                        else
                        {
                            //  rb.velocity = (-Vector3.left * touch.deltaPosition.x * GameMnager.gManager.playerSpeed * Time.deltaTime);
                            transform.Translate(new Vector3(touch.deltaPosition.x, 0, 0) * Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
                            charcters.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }



                    }

                }



            }


        }
    }

    

}
