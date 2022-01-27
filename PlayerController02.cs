using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class PlayerController02 : MonoBehaviour
{

    public static PlayerController02 playerController02;
    public GameObject charcters, moveSlide;

    public PathSystem.PathSystem_Object PathSystem_Object;

    public float isWaitTime = 5.0f;

    private NavMeshAgent playerAgent;

    [SerializeField]
    private GameObject slideAnimation;

    private Touch touch;

    [SerializeField]
    public int follower, slideValue;


    public Transform BFPosition;

    [SerializeField]
    public bool isBlance, isTouch, isDouble, isStay;

    private Rigidbody rb;

    private AudioSource audioSource;


    private void Awake()
    {
        if (playerController02 == null)
            playerController02 = this;

        if (PathSystem_Object.enabled)
        {
            PathSystem_Object.enabled = false;
        }

        slideAnimation.SetActive(false);

    }

    void Start()
    {
        isStay = false;
        isDouble = false;
        isBlance = false;
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        if (!moveSlide.activeSelf)
        {
            moveSlide.SetActive(true);

        }

        //   Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.None;
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



    public void ChnagePlayer()
    {
        isDouble = true;
        GameMnager.gManager.magicEffect.SetActive(false);
        GameMnager.gManager.magicEffect.SetActive(true);
        GameMnager.gManager.activeAnimator.SetBool("isDouble", isDouble);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
        StartCoroutine(cancelHairAnimation());
        GameMnager.gManager.addHealth(40);

    }



    public void ChangeEffect()
    {
        StartCoroutine(cancelHairAnimation());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (GameMnager.gManager.isLevelFinsih == false)
        {
            if (other.gameObject.tag == "Pipe")
            {
                isBlance = true;
            }

            if (other.gameObject.tag == "dislike")
            {

                StartCoroutine(cancelHairAnimation());
                audioSource.clip = GameMnager.gManager.audioClips[1];
                audioSource.Play();
                GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs,
                    transform.position, transform.rotation);
                GameMnager.gManager.subtractHealth(5);
                if (GameMnager.gManager.likeScore > 0)
                {
                    GameMnager.gManager.likeScore -= 1;

                    GameMnager.gManager.failText.SetActive(false);
                    GameMnager.gManager.failText.transform.GetChild(0).gameObject.
                        GetComponent<Text>().text = "-1";
                    GameMnager.gManager.failText.SetActive(true);

                    GameMnager.gManager.likeText.text = "" + GameMnager.gManager.likeScore;
                }


                audioSource.clip = GameMnager.gManager.audioClips[3];
                audioSource.Play();

                Destroy(other.gameObject, 0.01f);


            }

            if (other.gameObject.tag == "like")
            {

                StartCoroutine(cancelHairAnimation());

                audioSource.clip = GameMnager.gManager.audioClips[1];
                audioSource.Play();
                GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs,
                    transform.position, transform.rotation);
                GameMnager.gManager.addHealth(5);
                GameMnager.gManager.likeScore += 1;

                //   Debug.Log();
                GameMnager.gManager.passText.SetActive(false);
                GameMnager.gManager.passText.transform.GetChild(0).
                    gameObject.GetComponent<Text>().text = "+1";
                GameMnager.gManager.passText.SetActive(true);

                GameMnager.gManager.likeText.text = "" + GameMnager.gManager.likeScore;
                Destroy(other.gameObject, 0.01f);
            }


            //if (other.gameObject.tag == "3")
            //{
            //    GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            //    GameMnager.gManager.playerNumber = 3;
            //    GameMnager.gManager.chagePlayerActivity();

            //    Destroy(other.gameObject, 0.01f);
            //    audioSource.clip = GameMnager.gManager.audioClips[2];
            //    audioSource.Play();
            //}

            //if (other.gameObject.tag == "4")
            //{
            //    GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            //    GameMnager.gManager.playerNumber = 4;
            //    GameMnager.gManager.chagePlayerActivity();
            //    audioSource.clip = GameMnager.gManager.audioClips[2];
            //    audioSource.Play();
            //    Destroy(other.gameObject, 0.01f);
            //}

            //if (other.gameObject.tag == "5")
            //{
            //    GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            //    GameMnager.gManager.playerNumber = 5;
            //    GameMnager.gManager.chagePlayerActivity();


            //    Destroy(other.gameObject, 0.01f);
            //    audioSource.clip = GameMnager.gManager.audioClips[2];
            //    audioSource.Play();
            //}

            //if (other.gameObject.tag == "6")
            //{
            //    GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            //    GameMnager.gManager.playerNumber = 6;
            //    GameMnager.gManager.chagePlayerActivity();

            //    Destroy(other.gameObject, 0.01f);
            //    audioSource.clip = GameMnager.gManager.audioClips[2];
            //    audioSource.Play();
            //}
            if (other.gameObject.CompareTag("GoldDrigger"))
            {
                //StartCoroutine(SlapSeq()); 
            }


            if (other.gameObject.tag == "Gate")
            {
                // other.gameObject.GetComponent<Animator>().enabled = true;
            }

            if (other.gameObject.tag == "FinishLine")
            {
                follower = 0;
                GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                    enabled = false;

                GameMnager.gManager.GameWin();
                Debug.Log("You Reach the finish line");
            }

            //if (other.gameObject.tag == "Fail")
            //{

            //    follower = 0;
            //    GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;


            //    //GameMnager.gManager.EventPlayerFailActivity();

            //    Debug.Log("You enter at fail Trigger");

            //    if (!GameMnager.gManager.failTiktalkPanel.activeSelf)
            //    {
            //        GameMnager.gManager.failTiktalkPanel.SetActive(true);

            //    }
            //}
            //if (other.gameObject.tag == "Pass")
            //{

            ////  other.gameObject.GetComponent<BoxCollider>().enabled = false;
            //    follower = 0;
            //    GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
            //    GameMnager.gManager.addHealth(5);

            //    if (!GameMnager.gManager.passtikTalkPanel.activeSelf)
            //    {
            //        GameMnager.gManager.passtikTalkPanel.SetActive(true);
            //    }
            //    //GameMnager.gManager.EventPlayerPassActivity();
            //    GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            //    Debug.Log("You enter at Pass Trigger");

            //}

            //if (other.gameObject.tag == "TowelDrop")
            //{
            //    GameMnager.gManager.EventPlayerTowelActivity();
            //    follower = 0;
            //    GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
            //    GameMnager.gManager.addHealth(5);

            //    if (!GameMnager.gManager.passtikTalkPanel.activeSelf)
            //    {
            //        GameMnager.gManager.passtikTalkPanel.SetActive(true);
            //    }

            //    GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            //    Debug.Log("You enter at Pass Trigger");

            //}



        }



    }

    private IEnumerator SlapSeq()
    {
        PlayerController.playerController.PathSystem_Object.enabled = false;
        GameMnager.gManager.FollowCamer.SetActive(false);
        GameMnager.gManager.GoldDriggerCamera.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        GameMnager.gManager.activeAnimator.SetBool("isDouble", isDouble);
        GameMnager.gManager.activeAnimator.SetBool("isSlap", true);
        yield return new WaitForSeconds(0.11f);
        GameMnager.gManager.femaleController._Animator.SetBool("isFail", true);
        yield return new WaitForSeconds(0.2f);
        GameMnager.gManager.activeAnimator.SetBool("isSlap", false);
        //GameMnager.gManager.femaleController.PlayerRunEnabler();    
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameMnager.gManager.isLevelFinsih == false)
        {
            if (other.gameObject.tag == "Pipe")
            {
                isBlance = true;
            }





            //if (other.gameObject.tag == "Pass" ||  other.gameObject.tag == "TowelDrop")

            //{
            //    if (follower <= 150)
            //    {
            //        follower += 1;

            //    }
            //    else
            //    {
            //        if (other.gameObject.GetComponent<BoxCollider>().enabled)
            //        {
            //            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            //            StartCoroutine(ActivePassAnim());
            //        }



            //        for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
            //        {
            //            GameMnager.gManager.eventBikini[i].SetActive(false);
            //            GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
            //            GameMnager.gManager.eventTankTop[i].SetActive(false);
            //            GameMnager.gManager.eventWedingDress[i].SetActive(false);
            //            GameMnager.gManager.eventSmallBikini[i].SetActive(false);
            //            GameMnager.gManager.eventshortScourts[i].SetActive(false);
            //            GameMnager.gManager.eventBackless[i].SetActive(false);

            //            GameMnager.gManager.towelDrop.SetActive(false);

            //        }



            //        /*    for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
            //        {
            //            GameMnager.gManager.eventBikini[i].SetActive(false);
            //            GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
            //            GameMnager.gManager.eventTankTop[i].SetActive(false);
            //            GameMnager.gManager.eventWedingDress[i].SetActive(false);
            //            GameMnager.gManager.eventSmallBikini[i].SetActive(false);
            //            GameMnager.gManager.eventshortScourts[i].SetActive(false);
            //            GameMnager.gManager.eventBackless[i].SetActive(false);
            //        }
            //        */

            //    }





            //    Debug.Log("You enter at Pass Trigger");

            //}


            //if (other.gameObject.tag == "Fail")
            //{
            //    if (follower <= 150)
            //    {
            //        follower += 1;
            //        GameMnager.gManager.subtractHealth(0.05f);

            //        //  Invoke("ActivePassEffect", 1.0f);

            //    }
            //    else
            //    {
            //        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
            //        if (GameMnager.gManager.failTiktalkPanel.activeSelf)
            //        {
            //            GameMnager.gManager.failTiktalkPanel.SetActive(false);
            //            StartCoroutine(ActiveFailAnim());
            //        }

            //        /*    for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
            //            {
            //                GameMnager.gManager.eventBikini[i].SetActive(false);
            //                GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
            //                GameMnager.gManager.eventTankTop[i].SetActive(false);
            //                GameMnager.gManager.eventWedingDress[i].SetActive(false);
            //                GameMnager.gManager.eventSmallBikini[i].SetActive(false);
            //                GameMnager.gManager.eventshortScourts[i].SetActive(false);
            //                GameMnager.gManager.eventBackless[i].SetActive(false);
            //            }
            //            */
            //      //  GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
            //    }

            //    Debug.Log("You enter at Fail Trigger");

            //}
        }



    }


    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "1")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            GameMnager.gManager.addHealth(30);

            StartCoroutine(cancelHairAnimation());

            GameMnager.gManager.playerNumber = 1;
            GameMnager.gManager.chagePlayerActivity();


            Destroy(other.gameObject, 0.01f);
            audioSource.clip = GameMnager.gManager.audioClips[2];
            audioSource.Play();
        }
        if (other.gameObject.tag == "2")
        {


            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.playerNumber = 2;
            GameMnager.gManager.chagePlayerActivity();

            Destroy(other.gameObject, 0.01f);

            audioSource.clip = GameMnager.gManager.audioClips[2];
            audioSource.Play();

            GameMnager.gManager.addHealth(30);
        }



        if (other.gameObject.tag == "Formal")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.dressNumber = 2;
            GameMnager.gManager.chagePlayerActivity();

            GameMnager.gManager.addHealth(30);
        }

        if (other.gameObject.tag == "Casual")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.dressNumber = 1;
            GameMnager.gManager.chagePlayerActivity();
            GameMnager.gManager.addHealth(30);
        }

        if (other.gameObject.tag == "Glass1")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.GlasseNumber = 1;

            GameMnager.gManager.addHealth(30);
            //    GameMnager.gManager.chagePlayerActivity();
        }


        if (other.gameObject.tag == "Glass2")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.GlasseNumber = 2;

            GameMnager.gManager.addHealth(30);
            //  GameMnager.gManager.chagePlayerActivity();
        }



        if (other.gameObject.tag == "ShortHair")
        {

            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.hairNumber = 1;
            GameMnager.gManager.addHealth(30);


            //  HairControler.hairControler.changeHair(GameMnager.gManager.hairNumber);

            Destroy(other.gameObject, 0.001f);
            audioSource.clip = GameMnager.gManager.audioClips[2];
            audioSource.Play();

        }
        if (other.gameObject.tag == "LongHair")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());

            GameMnager.gManager.hairNumber = 2;

            //HairControler.hairControler.changeHair(GameMnager.gManager.hairNumber);
            Destroy(other.gameObject, 0.001f);
            audioSource.clip = GameMnager.gManager.audioClips[2];
            audioSource.Play();

            GameMnager.gManager.addHealth(30);

        }


        if (other.gameObject.tag == "Blonde")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.hairColorNumber = 1;

            GameMnager.gManager.addHealth(30);
        }

        if (other.gameObject.tag == "Brunette")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());
            GameMnager.gManager.hairColorNumber = 2;

            GameMnager.gManager.addHealth(30);
        }



        //if (other.gameObject.tag == "exit")
        //{
        //    GameMnager.gManager.activeAnimator.SetBool("isGround", false);
        //    GameMnager.gManager.LevelFail();
        //}


        //if (other.gameObject.tag == "isJump")
        //{
        //    GameMnager.gManager.activeAnimator.SetBool("isGround", false);

        //}


        //if (other.gameObject.tag == "Pipe")
        //{

        //    isBlance = false;
        //    GameMnager.gManager.activeAnimator.SetBool("isGround", true);

        //    GameMnager.gManager.activeAnimator.SetBool("isBlance", false);


        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameMnager.gManager.activeAnimator.SetBool("isGround", true);
        }





        //if (collision.gameObject.tag == "Pipe")
        //{
        //    GameMnager.gManager.activeAnimator.SetBool("isBlance", true);
        //}
    }



    IEnumerator cancelHairAnimation()
    {

        slideAnimation.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", false);
        yield return new WaitForSeconds(0.1f);
        slideAnimation.SetActive(false);

        for (int x = 0; x < GameMnager.gManager.FemalePlayer.Length; x++)
        {
            //  GameMnager.gManager.FemalePlayer[x].transform.position = new Vector3(0.0f, GameMnager.gManager.FemalePlayer[x].transform.position.y, GameMnager.gManager.FemalePlayer[x].transform.position.z);
        }



        //  GameMnager.gManager.activeCharacter.transform.position = new Vector3(0.0f, GameMnager.gManager.activeCharacter.transform.position.y, GameMnager.gManager.activeCharacter.transform.position.z);

    }

    // public void ActiveFailEffect()
    // {

    //     //  CancelInvoke("ActivePassEffect");
    //   //  Debug.Log("Active Fail Button True");
    //     GameMnager.gManager.failTiktalkPanel.SetActive(false);

    //     GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;

    //     GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
    // //    yield return new WaitForSeconds(0.1f);
    //     GameMnager.gManager.subtractHealth(5);
    ////     yield return new WaitForSeconds(0.2f);
    //     GameMnager.gManager.activeAnimator.SetBool("isSpin", true);

    //     GameMnager.gManager.passtikTalkPanel.SetActive(false);
    //     for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
    //     {
    //         GameMnager.gManager.eventBikini[i].SetActive(false);
    //         GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
    //         GameMnager.gManager.eventTankTop[i].SetActive(false);
    //         GameMnager.gManager.eventWedingDress[i].SetActive(false);
    //         GameMnager.gManager.eventSmallBikini[i].SetActive(false);
    //         GameMnager.gManager.eventshortScourts[i].SetActive(false);
    //         GameMnager.gManager.eventBackless[i].SetActive(false);
    //     }


    //  //   yield return new WaitForSeconds(0.2f);
    //     GameMnager.gManager.faillCamera.SetActive(false);
    //     GameMnager.gManager.passCamera.SetActive(false);
    //     GameMnager.gManager.activeAnimator.SetBool("isSpin", false);



    //     GameMnager.gManager.failText.SetActive(false);
    //     GameMnager.gManager.failText.transform.GetChild(0).gameObject.GetComponent<Text>().text = "- 15K";
    //     GameMnager.gManager.failText.SetActive(true);
    //     // StartCoroutine(ActiveFailAnim());
    // }


    // public void ActivePassEffect()
    // {

    //     CancelInvoke("ActivePassEffect");


    //     GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;

    //     GameObject moneyEffect = Instantiate(GameMnager.gManager.moneyEffect, transform.position, transform.rotation);

    //  //   yield return new WaitForSeconds(0.1f);
    //     GameMnager.gManager.addHealth(15);
    //   //  yield return new WaitForSeconds(0.2f);
    //     GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
    //     if (GameMnager.gManager.passtikTalkPanel.activeSelf)
    //     {
    //         GameMnager.gManager.passtikTalkPanel.SetActive(false);
    //     }

    //     for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
    //     {
    //         GameMnager.gManager.eventBikini[i].SetActive(false);
    //         GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
    //         GameMnager.gManager.eventTankTop[i].SetActive(false);
    //         GameMnager.gManager.eventWedingDress[i].SetActive(false);
    //         GameMnager.gManager.eventSmallBikini[i].SetActive(false);
    //         GameMnager.gManager.eventshortScourts[i].SetActive(false);
    //         GameMnager.gManager.eventBackless[i].SetActive(false);

    //         GameMnager.gManager.towelDrop.SetActive(false);
    //     }

    //     GameMnager.gManager.failTiktalkPanel.SetActive(false);
    //   //  yield return new WaitForSeconds(0.2f);
    //     GameMnager.gManager.activeAnimator.SetBool("isSpin", false);
    //     GameMnager.gManager.faillCamera.SetActive(false);
    //     GameMnager.gManager.passCamera.SetActive(false);

    //     GameMnager.gManager.passText.SetActive(false);
    //     GameMnager.gManager.passText.transform.GetChild(0).gameObject.GetComponent<Text>().text = "+ 15K";
    //     GameMnager.gManager.passText.SetActive(true);

    // }

    IEnumerator ActiveFailAnim()
    {


        if (GameMnager.gManager.failTiktalkPanel.activeSelf)
        {
            GameMnager.gManager.failTiktalkPanel.SetActive(false);
        }


        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;

        GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
        Debug.Log("Poofs  Particales");
        yield return new WaitForSeconds(0.1f);
        GameMnager.gManager.subtractHealth(15);
        yield return new WaitForSeconds(0.2f);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", true);

        GameMnager.gManager.failTiktalkPanel.SetActive(false);
        //for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
        //{
        //    GameMnager.gManager.eventBikini[i].SetActive(false);
        //    GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
        //    GameMnager.gManager.eventTankTop[i].SetActive(false);
        //    GameMnager.gManager.eventWedingDress[i].SetActive(false);
        //    GameMnager.gManager.eventSmallBikini[i].SetActive(false);
        //    GameMnager.gManager.eventshortScourts[i].SetActive(false);
        //    GameMnager.gManager.eventBackless[i].SetActive(false);
        //    GameMnager.gManager.towelDrop.SetActive(false);
        //}


        yield return new WaitForSeconds(0.2f);
        GameMnager.gManager.faillCamera.SetActive(false);
        GameMnager.gManager.passCamera.SetActive(false);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", false);


        GameMnager.gManager.failText.SetActive(false);
        GameMnager.gManager.failText.transform.GetChild(0).gameObject.GetComponent<Text>().text = "- 15K";
        GameMnager.gManager.failText.SetActive(true);
    }

    IEnumerator ActivePassAnim()
    {

        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;

        GameObject moneyEffect = Instantiate(GameMnager.gManager.moneyEffect, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.1f);
        GameMnager.gManager.addHealth(15);
        yield return new WaitForSeconds(0.2f);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
        if (GameMnager.gManager.passtikTalkPanel.activeSelf)
        {
            GameMnager.gManager.passtikTalkPanel.SetActive(false);
        }

        //for (int i = 0; i < GameMnager.gManager.eventBackless.Length; i++)
        //{
        //    GameMnager.gManager.eventBikini[i].SetActive(false);
        //    GameMnager.gManager.eventfrogPlayer[i].SetActive(false);
        //    GameMnager.gManager.eventTankTop[i].SetActive(false);
        //    GameMnager.gManager.eventWedingDress[i].SetActive(false);
        //    GameMnager.gManager.eventSmallBikini[i].SetActive(false);
        //    GameMnager.gManager.eventshortScourts[i].SetActive(false);
        //    GameMnager.gManager.eventBackless[i].SetActive(false);
        //    GameMnager.gManager.towelDrop.SetActive(false);
        //}

        GameMnager.gManager.failTiktalkPanel.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", false);
        GameMnager.gManager.faillCamera.SetActive(false);
        GameMnager.gManager.passCamera.SetActive(false);

        GameMnager.gManager.passText.SetActive(false);
        GameMnager.gManager.passText.transform.GetChild(0).gameObject.GetComponent<Text>().text = "+ 15K";
        GameMnager.gManager.passText.SetActive(true);

    }

}
