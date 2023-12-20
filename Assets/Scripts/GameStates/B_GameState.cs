using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public A_GameState aGamestate;
    public C_GameState cGamestate;
    public D_GameState dGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionB;
    public GameObject impactB1;
    public GameObject impactB2;
    public GameObject factB;
    public GameObject continueButtonB;

    [Header("Bools")]
    public bool choiceB1Picked;
    public bool choiceB2Picked;

    [Header("Buttons")]
    public Button buttonB1;
    public Button buttonB2;

    [Header("Choice 1")]
    public float costOfChoiceB1;
    public float healthOfChoiceB1;
    public float moodOfChoiceB1;
    public float temporaryIncomeChangeB1;
    public float permanentIncomeChangeB1;

    [Header("Choice 2")]
    public float costOfChoiceB2;
    public float healthOfChoiceB2;
    public float moodOfChoiceB2;
    public float temporaryIncomeChangeB2;
    public float permanentIncomeChangeB2;

    public void ButtonB1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionB, impactB1, continueButtonB);
        barManager.UpdateHealthBar(healthOfChoiceB1);
        barManager.UpdateMoodBar(moodOfChoiceB1);
        barManager.UpdateMoneyBar(costOfChoiceB1);
        choiceB1Picked = true;
    }

    public void ButtonB2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionB, impactB2, continueButtonB);
        barManager.UpdateHealthBar(healthOfChoiceB2);
        barManager.UpdateMoodBar(moodOfChoiceB2);
        barManager.UpdateMoneyBar(costOfChoiceB2);
        choiceB2Picked = true;
    }

    public void ContinueButtonBClick()
    {
        if (impactB1.activeInHierarchy || impactB2.activeInHierarchy)
        {
            //1: fra ConsequenceB til factB
            buttonManager.ContinueToFact(impactB1, impactB2, factB);
        }
        else if (factB.activeInHierarchy)
        {
            //2: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay1(choiceB1Picked, temporaryIncomeChangeB1, temporaryIncomeChangeB2);
            endOfDayManager.UpdateAverageDailyIncome1(choiceB1Picked, permanentIncomeChangeB1, permanentIncomeChangeB2);

            //3: fra factB til endOfTheDayScreen og tilføjer daily income til ens moneybar
            endOfDayManager.EnableEndOfDayScreen(factB);

            //4: knapper, der ikke er råd til ved decisionB skal gøres non-interactable for D eller D
            if (aGamestate.choiceA1Picked)
            {
                buttonManager.DisableButton(cGamestate.costOfChoiceC1, cGamestate.buttonC1, cGamestate.costOfChoiceC2, cGamestate.buttonC2);
            }
            else if (aGamestate.choiceA2Picked)
            {
                buttonManager.DisableButton(dGamestate.costOfChoiceD1, dGamestate.buttonD1, dGamestate.costOfChoiceD2, dGamestate.buttonD2);
            }

        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(aGamestate.choiceA1Picked, cGamestate.decisionC, dGamestate.decisionD, continueButtonB);

            if (cGamestate.decisionC.activeInHierarchy)
            {
                //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
                buttonManager.CheckLoosingCondition(cGamestate.buttonC1, cGamestate.buttonC2);
            }
            else if (dGamestate.decisionD.activeInHierarchy)
            {
                //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
                buttonManager.CheckLoosingCondition(dGamestate.buttonD1, dGamestate.buttonD2);
            }

            DayToDayController.NextDay();
        }
    }

}
