﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeColourChange : MonoBehaviour
{
    public float elapsedTime = .0f;
    public float speed = .5f;
    public float fillStartPosition = 0f;
    public float fillEndPosition = .5f;

    public bool isNowOrrange;
    public bool isStartBattery;

    //PLEASE do not rename the following. Otherwise they will ALL need to be linked up AGAIN in Inspector.
    public GameObject previousObject1;
    //Needed for nodes with mulitple inputs.
    public GameObject previousObject2;
    public GameObject previousObject3;
    public GameObject previousObject4;

    public float triggerAngle1;
    public float triggerAngle2;
    public float triggerAngle3;
    public float triggerAngle4;

    public float badAngle1 = 10;
    public float badAngle2 = 10;
    public float badAngle3 = 10;
    public float badAngle4 = 10;

    Renderer rend;

    private bool hasFired = false;
    public bool isBattery = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        AudioManager.audioProgression = 0f;

        if (isStartBattery == true)
        {
            StartCoroutine(Fill());

        }
    }
    void Update()
    {

        //If this object is NOT the starting node AND is EITHER one of the trigger angles, do the thing.
        if (isStartBattery == false
            && (triggerAngle1 + 2 >= this.gameObject.transform.eulerAngles.z && triggerAngle1 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (triggerAngle2 + 2 >= this.gameObject.transform.eulerAngles.z && triggerAngle2 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (triggerAngle3 + 2 >= this.gameObject.transform.eulerAngles.z && triggerAngle3 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (triggerAngle4 + 2 >= this.gameObject.transform.eulerAngles.z && triggerAngle4 - 2 <= this.gameObject.transform.eulerAngles.z))
        {
            if (previousObject1.GetComponent<PipeColourChange>().isPipeNowOrange == true
                && previousObject2.GetComponent<PipeColourChange>().isPipeNowOrange == true
                && previousObject3.GetComponent<PipeColourChange>().isPipeNowOrange == true
                && previousObject4.GetComponent<PipeColourChange>().isPipeNowOrange == true)
            {
                if (!hasFired)
                {
                    hasFired = true;
                    StartCoroutine(Fill());
                    AudioProgression();
                }
                
            }

        }

        if (isStartBattery == false
            && (badAngle1 + 2 >= this.gameObject.transform.eulerAngles.z && badAngle1 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (badAngle2 + 2 >= this.gameObject.transform.eulerAngles.z && badAngle2 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (badAngle3 + 2 >= this.gameObject.transform.eulerAngles.z && badAngle3 - 2 <= this.gameObject.transform.eulerAngles.z)
            || (badAngle4 + 2 >= this.gameObject.transform.eulerAngles.z && badAngle4 - 2 <= this.gameObject.transform.eulerAngles.z))
        {
            StartCoroutine(Empty());
        }

    }
  

    IEnumerator Fill()
    {
        while (!isNowOrrange)
        {
            //rend.material.SetTextureOffset("_MainTex", new Vector2(Mathf.Lerp(fillStartPosition, fillEndPosition, elapsedTime), 0));
            //Vector1_776A8DBC
            rend.material.SetFloat("_Lerp", Mathf.Lerp(0, 1, elapsedTime));
            elapsedTime += speed * Time.deltaTime;

            {
                if (elapsedTime >= 1f)
                {
                    isNowOrrange = true;
                    elapsedTime = 0f;
                }
                else
                {

                }
                yield return isNowOrrange;
            }
        }
    }

    IEnumerator Empty()
    {
        while (isNowOrrange)
        {
            //Start at fill end, move to fill start

            //rend.material.SetTextureOffset("_MainTex", new Vector2(Mathf.Lerp(0f, -fillEndPosition, elapsedTime), 0));
            rend.material.SetFloat("_Lerp", Mathf.Lerp(1, 0, elapsedTime));
            elapsedTime += speed * Time.deltaTime;

            {
                if (elapsedTime >= 1f)
                {
                    isNowOrrange = false;
                    elapsedTime = 0f;
                }
                else
                {

                }
                
            }
            yield return !isNowOrrange;
        }
    }

   public void AudioProgression()
    {
        if(isBattery) AudioManager.audioProgression += 10f; // this will activate one stem in the audio everytime a battery turns on
        
        Debug.Log(AudioManager.audioProgression);
    }
}
