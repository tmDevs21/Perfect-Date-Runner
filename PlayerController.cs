using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public static PlayerController playerController;
    public PlayerManager playerManager;
    public GameObject charcters,moveSlide;

    public GameObject Angry;

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
    public bool isBlance,isTouch,isDouble,isStay, isBoyFriendFound;

    private Rigidbody rb;

    private AudioSource audioSource;
    //Female  Level finish point
    public GameObject playerFemaleWinPoint, togetherGate_Effect, playerFollowCamera, playerWinCamera;
    public Transform playerSitPoint;
    //
    public Transform togetherGate_Spawn;
    bool touchControl;



    private void Awake()
    {
        if (playerController == null)
            playerController = this;

        if (PathSystem_Object.enabled)
        {
            PathSystem_Object.enabled = false;
        }

        slideAnimation.SetActive(false);

    }

    void Start()
    {
        isBoyFriendFound = false;

        isStay = false;
        isDouble = false;
        isBlance = false;
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();

        if (!moveSlide.activeSelf)
        {
            moveSlide.SetActive(true);
           
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (!touchControl)
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
                                transform.Translate(new Vector3(touch.deltaPosition.x, 0, 0) *
                                    Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
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

                            }



                        }

                    }



                }


            }

            if (GameMnager.gManager.isLevelFinsih)
            {
                //StartCoroutine(PlayerSitSeQ()); 
            }
        }
        

    }


    public IEnumerator PlayerSitSeQ()
    {
        //transform.Translate(new Vector3(touch.deltaPosition.x, 0, 0) *
        //Time.deltaTime * GameMnager.gManager.playerSpeed, Space.Self);
        yield return new WaitForSeconds(1f);
        this.transform.position = playerFemaleWinPoint.transform.position;
        this.transform.rotation = playerFemaleWinPoint.transform.rotation;
        yield return new WaitForSeconds(1f);
        playerFemaleWinPoint.SetActive(false);
        //GameMnager.gManager.isLevelFinsih = false;  
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

            // DisLike or Broken Heart or Fail Door
            if(other.gameObject.CompareTag("BrokenHeart"))
            {
                StartCoroutine(cancelHairAnimation());
                audioSource.clip = GameMnager.gManager.audioClips[1];
                audioSource.Play();

                audioSource.clip = GameMnager.gManager.audioClips[3];
                audioSource.Play();

                Destroy(other.gameObject, 0.01f);
            }


            if (other.gameObject.tag == "dislike")
            {

                StartCoroutine(cancelHairAnimation());
                audioSource.clip = GameMnager.gManager.audioClips[1];
                audioSource.Play();
                GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, 
                 other.gameObject.transform.position, other.gameObject.transform.rotation);
                GameMnager.gManager.subtractHealth(5);
                if (GameMnager.gManager.likeScore > 0)
                {
                    GameMnager.gManager.likeScore -= 1;

                    GameMnager.gManager.failText.SetActive(false);
                    GameMnager.gManager.failText.transform.GetChild(0).gameObject.
                        GetComponent<Text>().text = "-1";
                    GameMnager.gManager.failText.SetActive(true);

                    GameMnager.gManager.likeText.text = "" + GameMnager.gManager.likeScore;

                    Destroy(moneyEffect, 1.0f);
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
                  this.transform.position, transform.rotation);

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


            if (other.gameObject.CompareTag("GoldDrigger"))
            {
                //StartCoroutine(SlapSeq()); 
            }

            if (other.gameObject.CompareTag("Go_Together"))
            {
                GameMnager.gManager.magicEffect.SetActive(false);
                GameMnager.gManager.magicEffect.SetActive(true);
                GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
                StartCoroutine(cancelHairAnimation());

                GameObject moneyEffect = Instantiate(togetherGate_Effect,
                togetherGate_Spawn.transform.position, togetherGate_Spawn.transform.rotation);
                isBoyFriendFound = true ; 
            }

            if (other.gameObject.tag == "Should_Go_Alone")
            {
                Debug.Log("tag Name " + other.gameObject.tag);

                GameMnager.gManager.magicEffect.SetActive(false);
                GameMnager.gManager.magicEffect.SetActive(true);
                GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
                StartCoroutine(cancelHairAnimation());


                playerManager.boyFriendGO[0].gameObject.GetComponent<MenController>().enabled = false;
                playerManager.boyFriendGO[1].gameObject.GetComponent<MenController>().enabled = false;
                playerManager.boyFriendGO[0].gameObject.transform.parent = null;
                playerManager.boyFriendGO[1].gameObject.transform.parent = null;
            }


            if (other.gameObject.tag == "Gate")
            {

                // then "plyer & Male"" hug each other with sequence
                //GameMnager.gManager.isLevelFinsih = true;
                touchControl = true;

                if (!isBoyFriendFound || isDouble == false)
                {
                    GameMnager.gManager.LevelFail();
                    Debug.Log("Call Level Fail");
                    //GameMnager.gManager.activeAnimator.SetBool("isFail", true);

                }
                else
                {
                    Debug.Log("call hug");
                    other.gameObject.GetComponent<Animator>().enabled = true;
                    StartCoroutine(ReachedGate());

                }


            }


            if (other.gameObject.tag == "HugTag")
            {
                Debug.Log("You Reach the HugTag");
                StartCoroutine(HugAnimSeq());
 
            }

            if (other.gameObject.tag == "FinishLine")
            {
                follower = 0;         // then "Player & Male " sit on chair & few second later Level complete Appeared with sequence
                //GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                 //   enabled = false;
                 if(isBoyFriendFound)
                {
                    
                    //StartCoroutine(ReachedFinishedLine()); 
                }
                
                Debug.Log("You Reach the finish line");
            }

  

        }

    }

    // When Player collide with tag
   // private IEnumerator PlayerHugSeq()
   // {
   //
   // }

    private IEnumerator ReachedGate()
    {
        yield return new WaitForSeconds(1.5f);
        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                    enabled = false;

        StartCoroutine(HugAnimSeq());
    }

    private IEnumerator HugAnimSeq()
    {
        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                  enabled = false;
        yield return new WaitForSeconds(0.5f);
        // BFPosition.transform.localPosition = new Vector3(0, BFPosition.transform.position.y, BFPosition.transform.position.z);
        GameMnager.gManager.activeAnimator.SetBool("isWin", true);
        GameMnager.gManager.activeAnimator.SetBool("isRun", false);
        Debug.Log("call Hug");

        playerManager.boyFriendGO[0].GetComponent<Animator>().SetBool("isHug", true);
        playerManager.boyFriendGO[1].GetComponent<Animator>().SetBool("isHug", true);

        yield return new WaitForSeconds(1f);
        GameMnager.gManager.activeAnimator.SetBool("isWin", false);
        playerManager.boyFriendGO[0].GetComponent<Animator>().SetBool("isHug", false);
        playerManager.boyFriendGO[1].GetComponent<Animator>().SetBool("isHug", false);

      
        playerManager.boyFriendGO[0].gameObject.GetComponent<MenController>().enabled = false;
        playerManager.boyFriendGO[1].gameObject.GetComponent<MenController>().enabled = false;


        playerManager.boyFriendGO[0].gameObject.transform.parent = null;
        playerManager.boyFriendGO[1].gameObject.transform.parent = null;



        GameMnager.gManager.activeAnimator.SetBool("isWalk", true);
        yield return new WaitForSeconds(0.8f);

    
        FindObjectOfType<PlayerSitController>().isHugComplete = true;

        if (playerManager.boyFriendGO[0].gameObject.GetComponent<MaleFollowPoint>().enabled)
        {
            playerManager.boyFriendGO[0].gameObject.GetComponent<MaleFollowPoint>().isHugComplete = true;
        }

        if (playerManager.boyFriendGO[1].gameObject.GetComponent<MaleFollowPoint>().enabled)
        {
            playerManager.boyFriendGO[1].gameObject.GetComponent<MaleFollowPoint>().isHugComplete = true;
        }
        
        yield return new WaitForSeconds(1.5f);
        GameMnager.gManager.FollowCamer.SetActive(false);
        GameMnager.gManager.passCamera.SetActive(true);
        yield return new WaitForSeconds(3.0f);


        GameMnager.gManager.activeAnimator.SetBool("WalkAndSit", true);
        playerManager.boyFriendGO[0].GetComponent<Animator>().SetBool("WalkAndSit", true);
        playerManager.boyFriendGO[1].GetComponent<Animator>().SetBool("WalkAndSit", true);
        yield return new WaitForSeconds(0.7f);



        //GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
        //            enabled = true;
        //yield return new WaitForSeconds(0.2f);
        if (isBoyFriendFound)
        {
            GameMnager.gManager.GameWin();
        }
        if (!isBoyFriendFound)
        {
            GameMnager.gManager.LevelFail();
        }

        follower = 0;
        //GameMnager.gManager.activeAnimator.SetBool("isRun", true);
        //GameMnager.gManager.activeAnimator.SetBool("isWin", false);
        // yield return new WaitForSeconds(0.1f);
        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                     enabled = false;
    }

    private IEnumerator ReachedFinishedLine()
    {
        GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                    enabled = false;
        GameMnager.gManager.activeAnimator.SetBool("isRun", false);
        GameMnager.gManager.activeAnimator.SetBool("WalkAndSit", true);
        playerManager.boyFriendGO[0].GetComponent<Animator>().SetBool("WalkAndSit", true);
        playerManager.boyFriendGO[1].GetComponent<Animator>().SetBool("WalkAndSit", true);
        playerFollowCamera.SetActive(false);  
        playerWinCamera.SetActive(true);  
        yield return new WaitForSeconds(1f);
        
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
        }

        if(other.gameObject.CompareTag("BF"))
        {
     
            isBoyFriendFound = true;
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

        if (other.gameObject.tag == "Negative")
        {

            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());

            StartCoroutine(AngryAnim());


            GameMnager.gManager.addHealth(-15);
        }

        if (other.gameObject.tag == "Positive")
        {
            GameMnager.gManager.magicEffect.SetActive(false);
            GameMnager.gManager.magicEffect.SetActive(true);
            GameMnager.gManager.activeAnimator.SetBool("isSpin", true);
            StartCoroutine(cancelHairAnimation());


            GameMnager.gManager.addHealth(15);
        }



    }

    private IEnumerator AngryAnim()
    {
        Angry.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Angry.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameMnager.gManager.activeAnimator.SetBool("isGround", true);
        }



        }



     IEnumerator cancelHairAnimation()
    {

        slideAnimation.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        GameMnager.gManager.activeAnimator.SetBool("isSpin", false);
      //  Quaternion characterrot = Quaternion.EulerAngles(transform.rotation.x,0,transform.rotation.z);

      //  this.transform.rotation = characterrot;
        yield return new WaitForSeconds(0.1f);
        slideAnimation.SetActive(false);
        GameMnager.gManager.activeAnimator.SetBool("isRun", true);

        for (int x = 0; x < GameMnager.gManager.FemalePlayer.Length; x++)
        {
        // GameMnager.gManager.FemalePlayer[x].transform.rotation = Quaternion.EulerAngles(0, 0.0f,0);
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

        GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, Quaternion.identity);
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
