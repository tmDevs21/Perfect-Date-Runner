using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyFriendWinController : MonoBehaviour
{
    public GameObject boyFriendGO;
    public PlayerController playerController;
    public Transform boyFriendTrans;
    public Transform targetedHugPOS_forBF;
    public Transform targetedPOS_BetweenHug_AndSit_forBF;
    public bool isBFHugged;
    public MenController menController;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // if(playerController.isPlayerFindEndGate)
      // {
      //
      //     StartCoroutine(BFWinSeq());
      // }
      // if(playerController.isPlayerReachedFinishLine)
      // {
      //    // StartCoroutine(BFSitAndWinSeq());
      //    // StopCoroutine(BFSitAndWinSeq()); 
      // }
    }

    public void BFHugSeQ()
    {
        Debug.Log("Men win controller ");
        
    }


    IEnumerator BFWinSeq()
    {
        //menController.isDouble = false;
        yield return new WaitForSeconds(0.5f);
        //this.transform.rotation = Quaternion.Lerp(boyFriendTrans.rotation, targetedHugPOS_forBF.rotation,
        //    0.8f * Time.deltaTime);
        //this.transform.position = Vector3.Lerp(boyFriendTrans.position, targetedHugPOS_forBF.position,
        //    0.8f * Time.deltaTime);
        
        yield return new WaitForSeconds(2.3f);
        //menController._Animator.SetBool("isHug", true);
        yield return new WaitForSeconds(0.5f);
        boyFriendGO.GetComponent<MenController>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        isBFHugged = true;
    }

    
}
