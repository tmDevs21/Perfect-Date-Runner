using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject slideAnimation;
    public bool isDouble;

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
}
