using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cubeManager : MonoBehaviour
{

    public GameObject[] cubes;
    GameObject currentCube;
    int index;
    float randomScale;

    public Slider scoreSlider;

    int score = 0;
    public Text scoreText;
    public Text finalScore;
    public Text CurrectScore;

    public float timer = 15;

    public int timerIncrease;


    public GameObject scoreScreen;
    public GameObject GameOverScreen;
    public GameObject StartScreen;

    public AudioSource collectSound;

    private bool gameStarted = false;

    public ParticleSystem boxParticle;

    // Start is called before the first frame update
    void Start()
    {

        scoreSlider.maxValue = 10;
        StartScreen.SetActive(true);
        scoreScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        pickCube();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreScreen.activeInHierarchy == true)
        {
            gameStarted = true;
            
        }
        if (timer > 0.0f && gameStarted == true)
        {
            timer -= Time.deltaTime;
        }
        

        if (Input.GetMouseButtonDown(0) && gameStarted == true)
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 1000f))
            {
                if (raycastHit.transform == currentCube.transform)
                {
                    //Our custom method. 
                    clicked();
                }
            }
        }


        if (timer <= 0.0f)
        {
            timerEnded();
        }


        if(scoreSlider.value == scoreSlider.maxValue)
        {
            scoreSlider.maxValue += 10;

            timer += timerIncrease;
        }



    }

    private void FixedUpdate()
    {
        scoreText.text = "Time: " + timer;
        CurrectScore.text = score + "/" + scoreSlider.maxValue;

    }
    void pickCube()
    {
        index = Random.Range(0, cubes.Length);
        currentCube = cubes[index];

        currentCube.GetComponent<Renderer>().material.color = new Color(0, 100, 100);
        currentCube.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        DynamicGI.UpdateEnvironment();


        currentCube.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.cyan);


        randomScale = Random.Range(-0.75f,1.0f);
        currentCube.transform.localScale += new Vector3(randomScale, randomScale, randomScale); ;
        
    }

    void clicked()
    {
        score += 1;
        scoreSlider.value += 1;
        collectSound.Play();
        boxParticle.transform.position = currentCube.transform.position;
        boxParticle.Play();
        currentCube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        currentCube.transform.localScale = new Vector3(1, 1, 1); ;
        currentCube.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        DynamicGI.UpdateEnvironment();


        currentCube.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        pickCube();

    }

    void timerEnded()
    {
        gameStarted = false;
        finalScore.text = "Score: " + score;
        scoreScreen.SetActive(false);
        GameOverScreen.SetActive(true);
    }


}
