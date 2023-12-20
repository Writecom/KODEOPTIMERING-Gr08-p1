using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public E_GameState eGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionC;
    public GameObject impactC1;
    public GameObject impactC2;
    public GameObject factC;
    public GameObject continueButtonC;

    [Header("Bools")]
    public bool choiceC1Picked;
    public bool choiceC2Picked;

    [Header("Buttons")]
    public Button buttonC1;
    public Button buttonC2;

    [Header("Choice1")]
    public float costOfChoiceC1;
    public float unexpectedExpenseC1;
    public float healthOfChoiceC1;
    public float moodOfChoiceC1;
    public float temporaryIncomeChangeC1;
    public float permanentIncomeChangeC1;

    [Header("Choice 2")]
    public float costOfChoiceC2;
    public float healthOfChoiceC2;
    public float moodOfChoiceC2;
    public float temporaryIncomeChangeC2;
    public float permanentIncomeChangeC2;

    public void ButtonC1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionC, impactC1, continueButtonC);
        barManager.UpdateHealthBar(healthOfChoiceC1);
        barManager.UpdateMoodBar(moodOfChoiceC1);
        barManager.UpdateMoneyBar(unexpectedExpenseC1);
        choiceC1Picked = true;
    }

    public void ButtonC2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionC, impactC2, continueButtonC);
        barManager.UpdateHealthBar(healthOfChoiceC2);
        barManager.UpdateMoodBar(moodOfChoiceC2);
        barManager.UpdateMoneyBar(costOfChoiceC2);
        choiceC2Picked = true;
    }

    public void ContinueButtonCClick()
    {
        if (impactC1.activeInHierarchy || impactC2.activeInHierarchy)
        {
            //1: fra ConsequenceC til factC
            buttonManager.ContinueToFact(impactC1, impactC2, factC);
        }
        else if (factC.activeInHierarchy)
        {
            //2: knapper, der ikke er råd til ved decisionC skal gøres non-interactable
            buttonManager.DisableButton(eGamestate.costOfChoiceE1, eGamestate.buttonE1, eGamestate.costOfChoiceE2, eGamestate.buttonE2);
            
            //3: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay2(choiceC1Picked, temporaryIncomeChangeC1, temporaryIncomeChangeC2);
            endOfDayManager.UpdateAverageDailyIncome2(choiceC1Picked, permanentIncomeChangeC1, permanentIncomeChangeC2);

            //4: fra factB til endOfTheDayScreen og tilføjer daily income til ens moneybar 
            endOfDayManager.EnableEndOfDayScreen(factC);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceC1Picked, eGamestate.decisionE, eGamestate.decisionE, continueButtonC);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
            buttonManager.CheckLoosingCondition(eGamestate.buttonE1, eGamestate.buttonE2);
        }
    }
}
