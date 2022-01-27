using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyFriendSitWinController : MonoBehaviour
{
    public BoyFriendWinController boyFriendWinController;
    public Transform targetedSitPOS_forBF;
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boyFriendWinController.isBFHugged)
        {
            StartCoroutine(BFSitAndWinSeq()); 
        }
    }


    IEnumerator BFSitAndWinSeq()
    {
        //playerManager.BF_GObj[0].GetComponent<BoyFriendWinController>().enabled = false;   
        yield return new WaitForSeconds(1.5f);
        //boyFriendWinController.menController.isDouble = false;
        boyFriendWinController.boyFriendGO.GetComponent<MenController>().enabled = true;
        this.transform.rotation = Quaternion.Lerp(boyFriendWinController.boyFriendTrans.rotation, 
            targetedSitPOS_forBF.rotation,1.8f * Time.deltaTime);
        this.transform.position = Vector3.Lerp(boyFriendWinController.boyFriendTrans.position, 
            targetedSitPOS_forBF.position, 1.8f * Time.deltaTime);
    }
}
