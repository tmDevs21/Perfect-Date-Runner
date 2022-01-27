using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisionController : MonoBehaviour
{
    private AudioSource audioSource;
    public PlayerAnimationController playerAnimationController;
    [SerializeField]
    public int follower, slideValue;
    [SerializeField]
    private bool isBlance,  isDouble, isStay;
    [SerializeField]
    private GameObject slideAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator cancelHairAnimation()
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


            if (other.gameObject.tag == "Gate")
            {
                other.gameObject.GetComponent<Animator>().enabled = true;
            }

 
     
            if (other.gameObject.tag == "FinishLine")
            {
                follower = 0;
                GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().
                    enabled = false;

                GameMnager.gManager.GameWin();
                Debug.Log("You Reach the finish line");
            }

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

}
