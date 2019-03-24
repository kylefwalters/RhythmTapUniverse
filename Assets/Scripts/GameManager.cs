﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public AudioSource theMusic;
    public bool startPlaying;

    public BeatScroller theBS;
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;
    public int currentMulti;
    public int multiTracker;
    public int failTracker=0;
    public int[] multiThresholds;
    public Text scoreText;
    public Text multiText;
    public GameObject failText;
    public GameObject winText;
    public GameObject startText;
    public AudioSource winAudio;
    public float timer;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       scoreText.text = "Score: 0";
        currentMulti = 1;
    }

    // Update is called once per frame
    void Update()
    {
       if(!startPlaying)
        {
            if (Input.anyKeyDown) { 
            startPlaying = true;
            theBS.hasStarted = true;
            theMusic.Play();
                //startText.SetActive(false);


            }
        }

       if (startPlaying)
        {
            timer += Time.deltaTime;
        }

        if (failTracker >= 5)
        {
          
            theBS.failed = true;
            theMusic.pitch = 0.75f;
            failText.SetActive(true);
            StartCoroutine("waitTwo");
            startPlaying = false;
        }

       if ((failTracker < 5) && (timer >= 45))
        {
            winText.SetActive(true);
            winAudio.Play();
            startPlaying = false;
        }

    }
    public void NoteHit()
    {
        Debug.Log("Hit On Time");

            currentScore += scorePerNote * currentMulti;
            scoreText.text = "Score: " + currentScore;
        }


    public void NoteMissed()
    {

        Debug.Log("Missed Note");
        currentMulti = 1;
        //multiTracker = 0;
        //multiText.text = "Combo: x" + currentMulti;
        failTracker++;
       


    }

    IEnumerator waitTwo()
    {
        yield return new WaitForSeconds(2);
        theMusic.Stop();

    }
}
