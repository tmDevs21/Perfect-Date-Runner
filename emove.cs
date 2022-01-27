//Can Yuva
//Unity 5 Enemy Follow to Player C# Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emove : MonoBehaviour
{

    Rigidbody _controller;
    Transform target;
    GameObject Player;

    [SerializeField]
    float _moveSpeed = 5.0f;

    [SerializeField]
    private PathSystem.PathSystem_Object follow;

    [SerializeField]
    float dist;


    private Animator animator;

    // Use this for initialization
    void Start()
    {


        Player = PlayerController.playerController.gameObject;
        target = Player.transform;



        _controller = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
     

    }


    void LateUpdate()
    {
        dist = Vector3.Distance(PlayerController.playerController.charcters.transform.position, transform.position);



        if (dist > 5.0f)
        {
         //   follow.enabled = true;

            Vector3 targetPosition = new Vector3(PlayerController.playerController.transform.position.x, PlayerController.playerController.transform.position.y - 1.0f, PlayerController.playerController.transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.03f);

            transform.LookAt(PlayerController.playerController.transform.position);

            animator.SetBool("isRun", true);


            // transform.Translate(new Vector3(PlayerController.playerController.transform.position.x, PlayerController.playerController.transform.position.y - 1.2f, PlayerController.playerController.transform.position.z), Space.Self);
            // transform.position = new Vector3();
        }
        else
        {
            animator.SetBool("isRun", false);
           // follow.enabled = false;
        }
        //  Vector3 newPos = new Vector3(transform.position.x, PlayerController.playerController.transform.position.y, transform.position.z);


    }
}