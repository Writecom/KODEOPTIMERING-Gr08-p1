using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayToDay : MonoBehaviour
{
    public Animator dayTransitionAnim;
    public GameObject dayToDayCanvas;
    public TMP_Text dayDisplay;
    public int dayCount;
    public AnimationClip DayEnd;

    public void NextDay()
    {
        StartCoroutine(NextDaySequence());
    }

    IEnumerator NextDaySequence()
    {
        dayToDayCanvas.SetActive(true);

        dayCount++;

        dayDisplay.text = "Day " + dayCount.ToString();

        dayTransitionAnim.SetTrigger("DayStart");

        yield return new WaitForSeconds(4);

        dayTransitionAnim.SetTrigger("DayEnd");

        yield return new WaitForSeconds(DayEnd.length);


        dayToDayCanvas.SetActive(false);
    }

}
