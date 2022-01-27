using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class pushWall : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro numberText;
    [SerializeField]
    private int score,subScore;



    [SerializeField]
    float dist;

    [SerializeField]
    GameObject wallGrp;

    bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        float rndNumber = Random.RandomRange(10.0f,25.0f);
        score = (int)rndNumber;
        subScore = score;
        numberText.text = "-"+ score+"K";
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector3.Distance(GameMnager.gManager.MainCamera.transform.position, transform.position);
        if (dist < 20.0f && !isTrigger)
        {

            Color color = this.GetComponent<Renderer>().material.color;
            if (color.a > 0.5f)
            {
                color.a -= 0.5f * Time.deltaTime;
            }
          
            this.GetComponent<Renderer>().material.color = color;

           // this.GetComponent<Renderer>().material.SetColor("Color", Color.red);
        }
       
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (score > 1)
            {

                GameMnager.gManager.activeAnimator.SetBool("isPush", true);

                Invoke("reduceNumber", 0.05f);

                Color color = this.GetComponent<Renderer>().material.color;
                color.a = 1.0f;
                this.GetComponent<Renderer>().material.color = color;
                isTrigger = true;
                  GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            }
        }

    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (score > 1)
            {

                GameMnager.gManager.activeAnimator.SetBool("isPush", true);

                Invoke("reduceNumber", 0.05f);

                Color color = this.GetComponent<Renderer>().material.color;
                color.a = 1.0f;
                this.GetComponent<Renderer>().material.color = color;
                isTrigger = true;
              //  GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            }
            else
            {
                

               
                Destroy(wallGrp, 0.0001f);
               
                GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
                
                GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
                GameMnager.gManager.activeAnimator.SetBool("isPush", false);

                GameMnager.gManager.failText.SetActive(false);
                GameMnager.gManager.failText.transform.GetChild(0).gameObject.GetComponent<Text>().text = "- " + subScore +"K" ;
                GameMnager.gManager.failText.SetActive(true);
            }
        }
    }

    /*
    private void OnCollisionStay(Collision collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            if (score > 0)
            {
           
                GameMnager.gManager.activeAnimator.SetBool("isPush", true);
              
                Invoke("reduceNumber", 0.05f);

                Color color = this.GetComponent<Renderer>().material.color;
                color.a = 1.0f;
                this.GetComponent<Renderer>().material.color = color;
                isTrigger = true;
                GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = false;
            }
            else
            {
                GameMnager.gManager.pRunController.GetComponent<PathSystem.PathSystem_Object>().enabled = true;
                Destroy(this.gameObject,0.05f);
                GameObject moneyEffect = Instantiate(GameMnager.gManager.poofs, transform.position, transform.rotation);
                GameMnager.gManager.activeAnimator.SetBool("isPush", false);
            }
        }
       
    }

    */


    void reduceNumber()
    {

        CancelInvoke("reduceNumber");
        GameMnager.gManager.subtractHealth(1.0f);
        score  -= 1;
        numberText.text = "-" + score+"K";
    }
}
