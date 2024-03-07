using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public GameObject scoreScreen;
    public GameObject GameOverScreen;
    public GameObject StartScreen;
    // Start is called before the first frame update
    void Start()
    {
        StartScreen.SetActive(true);
        scoreScreen.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void retry()
    {
        SceneManager.LoadScene(0);

    }
    public void leftClick()
    {
        SceneManager.LoadScene(1);

    }

    public void Startgame()
    {


        StartScreen.SetActive(false);
        scoreScreen.SetActive(true);
        GameOverScreen.SetActive(false);

        Scene currentScene = SceneManager.GetActiveScene();


        if(currentScene.name == "2")
        {

        }
    }
    public void multiSelect()
    {
        SceneManager.LoadScene(2);
    }
}
