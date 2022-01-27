using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;
using UnityEngine.SceneManagement;



public class GameMnager : MonoBehaviour
{




    public static GameMnager gManager;



    public AudioClip[] audioClips;


    public GameObject passtikTalkPanel, failTiktalkPanel,levelCompleteBanner,levelFailBanner,towelDrop;

    public GameObject endPose, pRunController,MainCamera;
    public GameObject moneyEffect, poofs, lovePoofs, brokenLovePoofs,magicEffect;

    // Female and Male controller Script
    public FemaleController femaleController;


    public GameObject[] FemalePlayer;
    public GameObject[] MiddleAge,YoungAge;

    public Material[] hairMaterials;


    public GameObject activeCharacter,chracterGrp;

    public Animator activeAnimator;


    public GameObject faillCamera,passCamera,FollowCamer,GoldDriggerCamera,passText,failText;

    public TextMeshPro titleText, pScoreText;


    public int activeNumber,totalFollowers,currentFollowes,playerNumber,
        hairNumber, dressNumber,hairColorNumber,GlasseNumber,levelNumber,score,likeScore,lastLevelNumber;

    public float playerSpeed,hValue;
    public float curProgress, maxProgress;

    public Image progressMeter;

    public Text[] modeText;

    public Text levelText,scoreText,likeText;

    public bool isLevelFinsih;

    //Female and Male Level finish point
    public GameObject playerFemaleWinPoint, maleWinPoint;
    // Level win effect
    public Transform winEffectSpawner;
    public GameObject winEffect;


    private void Awake()
    {

        activeNumber = 0;
        playerNumber = 0;
        hairNumber = 0;
        GlasseNumber = 0;

        FollowCamer.SetActive(true);
        GoldDriggerCamera.SetActive(false);

        if (levelCompleteBanner.activeSelf)
        {
            levelCompleteBanner.SetActive(false);
        }

        if (levelFailBanner.activeSelf)
        {
            levelFailBanner.SetActive(false);
        }

        curProgress = 15;

        isLevelFinsih = false;


        if (magicEffect.activeSelf)
        {
            magicEffect.SetActive(false);
        }
       
    }


    Scene sceneLoaded;

    // Start is called before the first frame update
    void Start()
    {

        progressMeter.GetComponent<Image>().fillAmount = 0;

        score = PlayerPrefs.GetInt("highScore");
        likeScore = PlayerPrefs.GetInt("likeScore");

        scoreText.text = "" + score;

        levelNumber = PlayerPrefs.GetInt("levelNumber");

        if (levelNumber == 0)
        {

            PlayerPrefs.SetInt("levelNumber", 1);

          

            levelNumber = PlayerPrefs.GetInt("levelNumber");
            levelText.text = "Level " + levelNumber;
            Debug.Log(PlayerPrefs.GetInt("levelNumber"));
            
     
        }

        else
        {
            levelNumber = PlayerPrefs.GetInt("levelNumber");
            levelText.text = "Level " + levelNumber;
            Debug.Log(PlayerPrefs.GetInt("levelNumber"));

        }
        Debug.Log( "Level Number "+ levelNumber);

        sceneLoaded = SceneManager.GetActiveScene();

        gManager = this;

        if (passtikTalkPanel.activeSelf)
        {
            passtikTalkPanel.SetActive(false);
        }
        if (failTiktalkPanel.activeSelf)
        {
            failTiktalkPanel.SetActive(false);
        }

     

        for (int i = 0; i< FemalePlayer.Length; i++)
        {
            if (i == 0)
            {
                FemalePlayer[0].SetActive(true);
            }
            else
            {
                FemalePlayer[i].SetActive(false);
            }
        
        }

     //   EventPlayerFailActivity();
    }

    // Update is called once per frame
    void Update()
    {

        activeAnimator.SetFloat("isLevel", hValue);
        pScoreText.text = (int)curProgress + "K";

        if (curProgress < 0 && !isLevelFinsih)
        {
            activeAnimator.SetBool("isFail", true);
            pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            isLevelFinsih = true;

            LevelFail();
        }
    }


    public void GameWin()
    {
        isLevelFinsih = true;
        activeAnimator.SetBool("isWin", true);
        // chracterGrp.transform.localRotation = Quaternion.Euler(0,180.0f, 0);
        //GameObject winEffectGo = Instantiate(winEffect ,
        //            winEffectSpawner.transform.position, winEffectSpawner.transform.rotation);
        winEffect.SetActive(true);


        StartCoroutine(levelPassBannerActive());
   
    }


    public void LevelFail()
    {


      
        isLevelFinsih = true;
        StartCoroutine(levelFailBannerActive());
    }


    IEnumerator levelPassBannerActive()
    {
        yield return new WaitForSeconds(0.3f);
        levelCompleteBanner.SetActive(true);

        PlayerPrefs.SetInt("highScore", score);
       PlayerPrefs.SetInt("likeScore",likeScore);
    }

    IEnumerator levelFailBannerActive()
    {
        yield return new WaitForSeconds(0.7f);
        pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
        activeAnimator.SetBool("isFail", true);
        yield return new WaitForSeconds(0.7f);
        levelFailBanner.SetActive(true);

        PlayerPrefs.SetInt("highScore", score);
        PlayerPrefs.SetInt("likeScore", likeScore);
    }


    public void addHealth(float damage)
    {

        

        //   activeAnimator.SetBool("isSpin", false);
        curProgress += damage;
        hValue = (1.0f / 300.0f) * curProgress;
        progressMeter.GetComponent<Image>().fillAmount = hValue;



        if (curProgress <= 30f)
        {
            titleText.text = "Canceled";
           // activeNumber = 0;
            //   Invoke("inActive", 0.1f);
            activeAnimator.SetFloat("isLevel", hValue);
        }
        else if (curProgress <= 60f)
        {
            titleText.text = "Normie";
            Invoke("inActive", 0.1f);
            activeAnimator.SetFloat("isLevel", hValue);
            //   activeAnimator.SetBool("isSpin", true);
        }
        else if (curProgress <= 90.0f)
        {
          //  activeNumber = 1;
            titleText.text = "BombShell";
            activeAnimator.SetFloat("isLevel", hValue);
            //    activeAnimator.SetBool("isSpin", true);
        }
        else if (curProgress >= 150.0f)
        {
         //   activeNumber = 2;
            titleText.text = "Diva";
            activeAnimator.SetFloat("isLevel", hValue);
            //    activeAnimator.SetBool("isSpin", true);

        }

        chagePlayerActivity();
    }



    public void subtractHealth(float damage)
    {
        //  activeAnimator.SetBool("isSpin", false);
        curProgress -= damage;
        hValue = (1.0f / 300.0f) * curProgress;
        progressMeter.GetComponent<Image>().fillAmount = hValue;



        if (curProgress <= 30f)
        {
            titleText.text = "Canceled";
           // activeNumber = 0;
            //   Invoke("inActive", 0.1f);
            activeAnimator.SetFloat("isLevel", hValue);
        }
        else if (curProgress <= 60f)
        {
          //  activeNumber = 0;
            titleText.text = "Normie";
            Invoke("inActive", 0.1f);
            activeAnimator.SetFloat("isLevel", hValue);
            //   activeAnimator.SetBool("isSpin", true);
        }
        else if (curProgress <= 90.0f)
        {
          //  activeNumber = 1;
            titleText.text = "BombShell";
            activeAnimator.SetFloat("isLevel", hValue);
            //    activeAnimator.SetBool("isSpin", true);
        }
        else if (curProgress >= 120.0f)
        {
          //  activeNumber = 2;
            titleText.text = "Diva";
            activeAnimator.SetFloat("isLevel", hValue);
            //    activeAnimator.SetBool("isSpin", true);

        }
    }



    public void chagePlayerActivity()
    {
            //    Debug.Log("chnage Bikini Iteam");

                for (int i = 0; i < FemalePlayer.Length; i++)
                {
                    if (i == playerNumber)
                    {
                        activeAnimator = FemalePlayer[i].GetComponent<Animator>();
                        if (!FemalePlayer[i].activeSelf)
                        {
                            magicEffect.SetActive(false);
                            magicEffect.SetActive(true);
                           
                           
                            FemalePlayer[i].SetActive(true);
                            activeAnimator = FemalePlayer[i].GetComponent<Animator>();
                            activeAnimator.SetBool("isRun", true);
                            activeAnimator.SetBool("isSpin", true);
                           
                        }
                       
                    }
                    else
                    {
                       FemalePlayer[i].SetActive(false);
                    }
                }
    }


    public void StartRoted()
    {
      //  magicEffect.SetActive(false);
      //  magicEffect.SetActive(true);
       // activeAnimator.SetBool("isSpin", true);

        Invoke("inActive", 0.1f);
    }

    //public void EventPlayerPassActivity()
    //{




    //    switch (playerNumber)
    //    {
    //        case 0:


    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventBikini[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventBikini[i].SetActive(true);
    //                        eventBikini[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventBikini[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventBikini[i].SetActive(false);
    //                }

    //                // eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;
    //        case 1:



    //            Debug.Log("chnage Frog Iteam");

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventfrogPlayer[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventfrogPlayer[i].SetActive(true);
    //                        eventfrogPlayer[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventfrogPlayer[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventfrogPlayer[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                //  eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }

    //            break;
    //        case 2:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventTankTop[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventTankTop[i].SetActive(true);
    //                        eventTankTop[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventTankTop[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventTankTop[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                //  eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }

    //            break;
    //        case 3:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventWedingDress[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventWedingDress[i].SetActive(true);
    //                        eventWedingDress[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventWedingDress[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventWedingDress[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                //   eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;


    //        case 4:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventSmallBikini[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventSmallBikini[i].SetActive(true);
    //                        eventSmallBikini[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventSmallBikini[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventSmallBikini[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                // eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;

    //        case 5:

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventshortScourts[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventshortScourts[i].SetActive(true);
    //                        eventshortScourts[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventshortScourts[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventSmallBikini[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                //  eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;

    //        case 6:

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventBackless[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventBackless[i].SetActive(true);
    //                        eventBackless[i].GetComponent<Animator>().SetBool("isPass", true);
    //                        eventBackless[i].GetComponent<Animator>().SetBool("isFail", false);


    //                    }

    //                }
    //                else
    //                {
    //                    eventSmallBikini[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                // eventBackless[i].SetActive(false);
    //            }
    //            break;




    //    }

    //    faillCamera.SetActive(false);
    //    passCamera.SetActive(true);


    //    Invoke("inActive", 0.1f);
    //}
    //public void EventPlayerFailActivity()
    //{

    //    switch (playerNumber)
    //    {
    //        case 0:
             

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {
                      
    //                    if (!eventBikini[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventBikini[i].SetActive(true);
    //                        eventBikini[i].GetComponent<Animator>().SetBool("isPass", false);
    //                        eventBikini[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventBikini[i].SetActive(false);
    //                }

    //               // eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;
    //        case 1:



    //            Debug.Log("chnage Frog Iteam");

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventfrogPlayer[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventfrogPlayer[i].SetActive(true);
    //                        eventfrogPlayer[i].GetComponent<Animator>().SetBool("isPass", false);
    //                       eventfrogPlayer[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventfrogPlayer[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //              // eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }

    //            break;
    //        case 2:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventTankTop[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventTankTop[i].SetActive(true);
    //                        eventTankTop[i].GetComponent<Animator>().SetBool("isPass", false);
    //                        eventTankTop[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventTankTop[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //              //  eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }

    //            break;
    //        case 3:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventWedingDress[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventWedingDress[i].SetActive(true);
    //                        eventWedingDress[i].GetComponent<Animator>().SetBool("isPass", false);
    //                        eventWedingDress[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventWedingDress[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //             //   eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;


    //        case 4:
    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventSmallBikini[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventSmallBikini[i].SetActive(true);
    //                        eventSmallBikini[i].GetComponent<Animator>().SetBool("isPass", false);
    //                        eventSmallBikini[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventSmallBikini[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //               // eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;

    //        case 5:

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventshortScourts[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventshortScourts[i].SetActive(true);
    //                        eventshortScourts[i].GetComponent<Animator>().SetBool("isPass", false);
    //                        eventshortScourts[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventSmallBikini[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //              //  eventshortScourts[i].SetActive(false);
    //                eventBackless[i].SetActive(false);
    //            }
    //            break;

    //        case 6:

    //            for (int i = 0; i < bkiniPlayer.Length; i++)
    //            {
    //                if (i == activeNumber)
    //                {

    //                    if (!eventBackless[i].activeSelf)
    //                    {

    //                        Debug.Log("chnage Fail Eevnt");
    //                        eventBackless[i].SetActive(true);
    //                        eventBackless[i].GetComponent<Animator>().SetBool("isPass", false);
    //                    //    eventBackless[i].GetComponent<Animator>().SetBool("isFail", true);


    //                    }

    //                }
    //                else
    //                {
    //                    eventBackless[i].SetActive(false);
    //                }

    //                eventBikini[i].SetActive(false);
    //                eventfrogPlayer[i].SetActive(false);
    //                eventTankTop[i].SetActive(false);
    //                eventWedingDress[i].SetActive(false);
    //                eventSmallBikini[i].SetActive(false);
    //                eventshortScourts[i].SetActive(false);
    //               // eventBackless[i].SetActive(false);
    //            }
    //            break;




    //    }
    //    passCamera.SetActive(false);
    //    faillCamera.SetActive(true);
        


    //    Invoke("inActive", 0.1f);
    //}

    public void EventPlayerTowelActivity()
    {

        towelDrop.SetActive(true);

        faillCamera.SetActive(false);
        passCamera.SetActive(true);
        Invoke("inActive", 0.1f);

    }

    void inActive()
    {
        //CancelInvoke("isSpin");
        //activeAnimator.SetBool("isSpin", false);
       


    }

    
  
   
}
