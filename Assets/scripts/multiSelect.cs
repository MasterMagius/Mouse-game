using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class multiSelect : MonoBehaviour
{
    int leftInt = 0;
    int rightInt = 0;
    

    public Text leftText;
    public Text rightText;
    public Text scoreText;
    public Slider scoreSlider;
    private bool gameStarted = false;

    int score = 0;
    public Text finalScore;
    public Text CurrectScore;
    public float timer = 15;

    public int timerIncrease;

    public int ninjasInLeft = 0;
    public int ninjasInRight = 0;
    public int ninjasInBottom = 0;

    public bool correctAns;

    public ParticleSystem boxParticle;
    public AudioSource collectSound;


    public GameObject sceneManager;



    // Start is called before the first frame update
    void Start()
    {
        scoreSlider.maxValue = 10;
        correctAns = false;
        PickNumbers();
    }

    // Update is called once per frame
    void Update()
    {
        if (ninjasInLeft == leftInt && ninjasInRight == rightInt && !Ninja.mouseOverNinja)
        {
            score += 1;
            scoreSlider.value += 1;
            collectSound.Play();
            boxParticle.Play();
            correctAns = true;
            PickNumbers();
        }

        if (ninjasInBottom == 5)
        {
            correctAns = false;
        }
        Debug.Log(ninjasInBottom);

        if (scoreSlider.value == scoreSlider.maxValue)
        {
            scoreSlider.maxValue += 10;

            timer += timerIncrease;
        }

        if (timer <= 0.0f)
        {
            timerEnded();
        }

        if (sceneManager.GetComponent<sceneManager>().scoreScreen.activeInHierarchy)
        {
            gameStarted = true;
        }

        if (timer > 0.0f && gameStarted == true)
        {
            timer -= Time.deltaTime;
        }

    }
    private void FixedUpdate()
    {
        scoreText.text = "Time: " + timer;
        CurrectScore.text = score + "/" + scoreSlider.maxValue;

    }


    void PickNumbers()
    {
        leftInt = Random.Range(0, 5);
        rightInt = 5- leftInt;
        leftText.text = leftInt + " ninjas";
        rightText.text = rightInt + " ninjas";
    }


    void timerEnded()
    {
        gameStarted = false;
        finalScore.text = "Score: " + score;
        sceneManager.GetComponent<sceneManager>().scoreScreen.SetActive(false);
        sceneManager.GetComponent<sceneManager>().GameOverScreen.SetActive(true);
    }

}
