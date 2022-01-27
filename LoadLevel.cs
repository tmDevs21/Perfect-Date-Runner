using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    private int lastLevelNumber;

    private void Awake()
    {
        //  PlayerPrefs.SetInt("levelNumber",0);
    }

    // Start is called before the first frame update
    void Start()
    {

      
        Scene sceneLoaded = SceneManager.GetActiveScene();
      //  SceneManager.LoadScene(sceneLoaded.buildIndex + lastLevelNumber);

        if (sceneLoaded.buildIndex == 0)
        {
                lastLevelNumber = PlayerPrefs.GetInt("lastLevelNumber");
                Debug.Log("lastLevelNumber  " + lastLevelNumber);
                NextScene();
        }
        else
        {

            PlayerPrefs.SetInt("lastLevelNumber", sceneLoaded.buildIndex);

            Debug.Log("lastLevelNumber  " + sceneLoaded.buildIndex);
        }





        // loadingOperation = SceneManager.LoadSceneAsync(sceneLoaded.buildIndex);
    }


    public void ReloadLevel()
    {
        Scene sceneLoaded = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sceneLoaded.buildIndex);
    }

    public void NextScene()
    {


        if (lastLevelNumber == 0)
        {
            Scene sceneLoaded = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);
        }
        else
        {
            Scene sceneLoaded = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneLoaded.buildIndex + lastLevelNumber);
        }

        ///  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);  
    }

    public void NextLevel()
    {

        //  loadText.text = "Loading Next Level";



        PlayerPrefs.SetInt("levelNumber", GameMnager.gManager.levelNumber + 1);
        GameMnager.gManager.levelNumber = PlayerPrefs.GetInt("levelNumber");
        Debug.Log("Next Level Number " + GameMnager.gManager.levelNumber);

        if (GameMnager.gManager.levelNumber <= 5)
        {
            SceneManager.LoadScene(GameMnager.gManager.levelNumber);
        }
        else
        {
            int nextLevel = Random.Range(0, 5);
            SceneManager.LoadScene(nextLevel);
            Debug.Log("Rnd Next Level Number " + nextLevel);
        }


        // StartCoroutine(StartLoad());
    }





}
