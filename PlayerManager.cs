using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerManager playerManager;

    public PlayerMovement playerMovement;
    public PlayerAnimationController playerAnimationController;
    public GameObject[] boyFriendGO; 

    private void Awake()
    {
        playerManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
