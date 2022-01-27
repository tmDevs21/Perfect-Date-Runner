using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSitMovement : MonoBehaviour
{
    public PlayerManager playerManager;
    public PlayerHugMovement playerHugMovement;
    public PlayerController playerController;
    public Transform  targetedSitPOS;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHugMovement.isPlayerHugged)
        {
            StartCoroutine(PlayerSitSeQ());
            Debug.Log("Player Sit Sequence");  
        }
    }

    public IEnumerator PlayerSitSeQ()
    {
        //playerManager.player.GetComponent<PlayerHugMovement>().enabled = false;   
        //this.transform.position = Vector3.Lerp(playerController.playerTrans.position, targetedSitPOS.position,
          // 1.0f * Time.deltaTime);
        //this.transform.rotation = Quaternion.Lerp(playerController.playerTrans.rotation, targetedSitPOS.rotation,
           // 0.7f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        //playerManager.player.GetComponent<PlayerController>().enabled = true;
    }
}
