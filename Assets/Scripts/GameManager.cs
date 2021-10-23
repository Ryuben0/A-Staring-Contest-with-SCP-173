using System.Collections;
using System.Collections.Generic;
//This is required to use lists^
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int colorSelect;
    private int rumbleSelect;
    private int snapSelect;

    public SpriteRenderer[] colors;

    public float stayLit;
    private float stayLitCounter;

    private bool shouldBeLit;
    private bool shouldBeDark;

    public float waitBetweenLights;
    private float waitBetweenCounter;

    public List<int> sequenceList;
    private int positionInSequence;
    private int inputInSequence;

    private bool gameActive;

    public AudioSource correct;
    public AudioSource[] incorrect;
    public AudioSource[] beeps;
    public AudioSource[] neckSnap;

    public SpriteRenderer SCP;
    public Rigidbody2D scpRigidbody;
    public float maxSize = 7f;
    private Vector2 startPosition;

    public SpriteRenderer Blink;

    public float growDistance = 1f;
    public float growFactor = 3f;

    public float shrinkDistance = .5f;
    public float shrinkFactor = .9f;

    private bool hardMode;


    private void Start()
    {
        startPosition = scpRigidbody.position;
        StartGame();
    }

    private void Update()
    {
        if (shouldBeLit == true)
        {
            stayLitCounter -= Time.deltaTime;
            if (stayLitCounter < 0)
            {
                colors[sequenceList[positionInSequence]].color = new Color(colors[sequenceList[positionInSequence]].color.r, colors[sequenceList[positionInSequence]].color.g, colors[sequenceList[positionInSequence]].color.b, .5f);
                beeps[sequenceList[positionInSequence]].Stop();

                shouldBeLit = false;

                shouldBeDark = true;

                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
            }
        }

        if (shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if (positionInSequence >= sequenceList.Count)
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if (waitBetweenCounter <= 0)
                {


                    colors[sequenceList[positionInSequence]].color = new Color(colors[sequenceList[positionInSequence]].color.r, colors[sequenceList[positionInSequence]].color.g, colors[sequenceList[positionInSequence]].color.b, 1f);
                    beeps[sequenceList[positionInSequence]].Play();

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }

    public void StartGame()
    {
        sequenceList.Clear();

        positionInSequence = 0;
        inputInSequence = 0;

        colorSelect = Random.Range(0, colors.Length);

        sequenceList.Add(colorSelect);

        colors[sequenceList[positionInSequence]].color = new Color(colors[sequenceList[positionInSequence]].color.r, colors[sequenceList[positionInSequence]].color.g, colors[sequenceList[positionInSequence]].color.b, 1f);
        beeps[sequenceList[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    public void colorInput(int whichButton)
    {
        if (gameActive && inputInSequence < sequenceList.Count)
        {
            if (sequenceList[inputInSequence] == whichButton)
            {
                inputInSequence++;
                if (inputInSequence >= sequenceList.Count)
                {
                    Invoke("correctAnswer", .5f);
                }
            }
            else
            {
                Invoke("incorrectAnswer", .5f);
            }
        }
    }

    public void correctAnswer()
    {
        positionInSequence = 0;
        inputInSequence = 0;

        colorSelect = Random.Range(0, colors.Length);

        sequenceList.Add(colorSelect);

        colors[sequenceList[positionInSequence]].color = new Color(colors[sequenceList[positionInSequence]].color.r, colors[sequenceList[positionInSequence]].color.g, colors[sequenceList[positionInSequence]].color.b, 1f);
        beeps[sequenceList[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;

        shrink();

        correct.Play();

        gameActive = false;
    }

    public void incorrectAnswer()
    {
        blink();

        rumbleSelect = Random.Range(0, incorrect.Length);
        incorrect[rumbleSelect].Play();

        grow();

        if (SCP.transform.localScale.x > maxSize)
        {
            snapSelect = Random.Range(0, neckSnap.Length);
            neckSnap[snapSelect].Play();
        }

        Invoke("unblink", 1f);

        if (SCP.transform.localScale.x > maxSize)
        {
            gameActive = false;
        }
        else
        {
            positionInSequence = 0;
            inputInSequence = 0;

            Invoke("restart", 2f);
        }
    }

    private void blink()
    {
        Blink.enabled = true;
    }

    private void unblink()
    {
        Blink.enabled = false;
    }

    private void grow()
    {
        SCP.transform.localScale = new Vector3((SCP.transform.localScale.x * growFactor), (SCP.transform.localScale.y * growFactor), SCP.transform.localScale.y);

        scpRigidbody.transform.position = new Vector2(scpRigidbody.transform.position.x, scpRigidbody.transform.position.y - (scpRigidbody.transform.localScale.y * growDistance));
    }

    private void shrink()
    {
        SCP.transform.localScale = new Vector3((SCP.transform.localScale.x * shrinkFactor), (SCP.transform.localScale.y * shrinkFactor), SCP.transform.localScale.y);

        if(scpRigidbody.position != startPosition)
        {
            scpRigidbody.transform.position = new Vector2(scpRigidbody.transform.position.x, scpRigidbody.transform.position.y + shrinkDistance);
        }
    }

    public void restart()
    {
        colors[sequenceList[positionInSequence]].color = new Color(colors[sequenceList[positionInSequence]].color.r, colors[sequenceList[positionInSequence]].color.g, colors[sequenceList[positionInSequence]].color.b, 1f);
        beeps[sequenceList[positionInSequence]].Play();

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }
}
