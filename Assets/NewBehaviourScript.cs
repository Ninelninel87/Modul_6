using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
   
    public GameObject firstKey;
    public GameObject secondKey;
    public GameObject thirdKey;
    public GameObject winScreen;
    public GameObject loseScreen;
    public Text TimeCount;
    private float startTime;
    private int roundTimeDuration = 20;
    private bool endGame = false;

    private int[] keysPosition = {0,3,5 };
    private int transformMoveStep = 25;
    private int transformStartPosition = 100;
    private int[] firstButtChangePos = { 1, -1,0 };
    private int[] secondButtChangePos = { -1, 2, -1 };
    private int[] thirdButtChangePos = { -1, 1, 1 };

    private void Start()
    {
        updateKeysPosition();
        startTime = Time.time;
    }

    private void Update()
    {
        if (endGame) return;
        CheckTime();
    }

    public void ReloadLevel1()
    {
        keysPosition = new int[] { 0, 3, 5 };
        endGame = false;
        startTime = Time.time;
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        updateKeysPosition();
    }

    private void CheckTime()
    {
        if (startTime + roundTimeDuration < Time.time)
        {
            endGame = true;
            loseScreen.SetActive(true);
        }

        TimeCount.text = ((int)(roundTimeDuration - (Time.time - startTime))).ToString();
        Debug.Log(TimeCount);
    }

    private void updateKeysPosition()
    {
        CheckKeysPosition();
        SetTransformY(firstKey, yPosition:transformStartPosition - transformMoveStep * keysPosition[0]);
        SetTransformY(secondKey, yPosition: transformStartPosition - transformMoveStep * keysPosition[1]);
        SetTransformY(thirdKey, yPosition: transformStartPosition - transformMoveStep * keysPosition[2]);
    }

    private void CheckKeysPosition()
    {
        for(int i = 0; i<3; i++)
        {
            if (keysPosition[i] != 6)
            {
                return;
            }
        }
        endGame = true;
        winScreen.SetActive(true);
    }


    void SetTransformY(GameObject key, int yPosition)
    {
        var keyPosition = key.transform.localPosition;
        key.transform.localPosition = new Vector3(keyPosition.x, yPosition);
    }

    private void SetNewKeysPosition(int[] newPositions)
    {
        for (int i = 0; i<3;i++)
        {
            keysPosition[i] += newPositions[i];
            if (keysPosition[i] < 0) keysPosition[i] = 0;
            if (keysPosition[i] > 9) keysPosition[i] = 9;
        }
        updateKeysPosition();

    }
      public void HandleNewKeysPosition(int buttNumber)
    {
        switch (buttNumber)
        {
            case 1:
                {
                    SetNewKeysPosition(firstButtChangePos);
                    break;
                }
            case 2:
                {
                    SetNewKeysPosition(secondButtChangePos);
                    break;
                }
            case 3:
                {
                    SetNewKeysPosition(thirdButtChangePos);
                    break;
                }
            default:
                {
                    Debug.Log(message: "Unhandled button number");
                    break;
                }

        }
    }
  
}
