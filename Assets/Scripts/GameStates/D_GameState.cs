using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public E_GameState eGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionD;
    public GameObject impactD1;
    public GameObject impactD2;
    public GameObject factD;
    public GameObject continueButtonD;

    [Header("Bools")]
    public bool choiceD1Picked;
    public bool choiceD2Picked;

    [Header("Buttons")]
    public Button buttonD1;
    public Button buttonD2;

    [Header("Choice1")]
    public float costOfChoiceD1;
    public float healthOfChoiceD1;
    public float moodOfChoiceD1;
    public float temporaryIncomeChangeD1;
    public float permanentIncomeChangeD1;

    [Header("Choice 2")]
    public float costOfChoiceD2;
    public float unexpectedExpenseD2;
    public float healthOfChoiceD2;
    public float moodOfChoiceD2;
    public float temporaryIncomeChangeD2;
    public float permanentIncomeChangeD2;

    public void ButtonD1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionD, impactD1, continueButtonD);
        barManager.UpdateHealthBar(healthOfChoiceD1);
        barManager.UpdateMoodBar(moodOfChoiceD1);
        barManager.UpdateMoneyBar(costOfChoiceD1);
        choiceD1Picked = true;
    }

    public void ButtonD2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionD, impactD2, continueButtonD);
        barManager.UpdateHealthBar(healthOfChoiceD2);
        barManager.UpdateMoodBar(moodOfChoiceD2);
        barManager.UpdateMoneyBar(unexpectedExpenseD2);

        choiceD2Picked = true;
    }

    public void ContinueButtonDClick()
    {
        if (impactD1.activeInHierarchy || impactD2.activeInHierarchy)
        {
            //1: fra ConsequenceC til factC
            buttonManager.ContinueToFact(impactD1, impactD2, factD);
        }
        else if (factD.activeInHierarchy)
        {
            //2: knapper, der ikke er råd til ved decisionD gøres non-interactable
            buttonManager.DisableButton(eGamestate.costOfChoiceE1, eGamestate.buttonE1, eGamestate.costOfChoiceE2, eGamestate.buttonE2);

            //3: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay2(choiceD1Picked, temporaryIncomeChangeD1, temporaryIncomeChangeD2);
            endOfDayManager.UpdateAverageDailyIncome2(choiceD1Picked, permanentIncomeChangeD1, permanentIncomeChangeD2);

            //4: fra factD til endOfTheDayScreen og tilføjer daily income til ens moneybar 
            endOfDayManager.EnableEndOfDayScreen(factD);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceD1Picked, eGamestate.decisionE, eGamestate.decisionE, continueButtonD);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
            buttonManager.CheckLoosingCondition(eGamestate.buttonE1, eGamestate.buttonE2);
        }
    }
}
