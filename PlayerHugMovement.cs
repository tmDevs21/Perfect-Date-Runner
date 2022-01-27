using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHugMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerController playerController;
    
    public Transform targetedHugPOS, targetedSitPOS;
    public bool isPlayerHugged;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //if( playerController.isPlayerFindEndGate && isPlayerHugged == false )
       //{
       //    StartCoroutine(PlayerWinSEQ() );
       //}
    }

    public void PlayerHugSeQ()
    {
        
    }
     
    public IEnumerator PlayerWinSEQ()
    {
        //playerManager.player.GetComponent<PlayerController>().enabled = false;
        //this.transform.position = Vector3.Lerp(playerController.playerTrans.position, targetedHugPOS.position,
        //    1.0f * Time.deltaTime);
        //this.transform.rotation = Quaternion.Lerp(playerController.playerTrans.rotation, targetedHugPOS.rotation, 
        //    0.7f * Time.deltaTime);    
        //yield return new WaitForSeconds(1f);
        //playerManager.player.GetComponent<PlayerController>().enabled = true;
        //yield return new WaitForSeconds(0.5f);
        //isPlayerHugged = true;

        yield return new WaitForSeconds(0.5f);
    }

}
