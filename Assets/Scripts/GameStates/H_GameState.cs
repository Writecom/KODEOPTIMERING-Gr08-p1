using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class H_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public I_GameState iGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionH;
    public GameObject impactH1;
    public GameObject impactH2;
    public GameObject factH;
    public GameObject continueButtonH;

    [Header("Bools")]
    public bool choiceH1Picked;
    public bool choiceH2Picked;

    [Header("Buttons")]
    public Button buttonH1;
    public Button buttonH2;

    [Header("Choice1")]
    public float costOfChoiceH1;
    public float healthOfChoiceH1;
    public float moodOfChoiceH1;
    public float temporaryIncomeChangeH1;
    public float permanentIncomeChangeH1;

    [Header("Choice 2")]
    public float costOfChoiceH2;
    public float healthOfChoiceH2;
    public float moodOfChoiceH2;
    public float temporaryIncomeChangeH2;
    public float permanentIncomeChangeH2;

    public void ButtonH1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionH, impactH1, continueButtonH);
        barManager.UpdateHealthBar(healthOfChoiceH1);
        barManager.UpdateMoodBar(moodOfChoiceH1);
        barManager.UpdateMoneyBar(costOfChoiceH1);
        choiceH1Picked = true;
        buttonManager.EnableAskANeighborButton();
    }

    public void ButtonH2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionH, impactH2, continueButtonH);
        barManager.UpdateHealthBar(healthOfChoiceH2);
        barManager.UpdateMoodBar(moodOfChoiceH2);
        barManager.UpdateMoneyBar(costOfChoiceH2);
        choiceH2Picked = true;
    }

    public void ContinueButtonHClick()
    {
        if (impactH1.activeInHierarchy || impactH2.activeInHierarchy)
        {
            //1: fra ConsequenceC til factC
            buttonManager.ContinueToFact(impactH1, impactH2, factH);
        }
        else if (factH.activeInHierarchy)
        {
            //2: knapper, der ikke er råd til ved decisionI skal gøres non-interactable
            buttonManager.DisableButton(iGamestate.costOfChoiceI1, iGamestate.buttonI1, iGamestate.costOfChoiceI2, iGamestate.buttonI2);

            //3: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay2(choiceH1Picked, costOfChoiceH1, costOfChoiceH2);
            endOfDayManager.UpdateAverageDailyIncome1(choiceH1Picked, permanentIncomeChangeH1, permanentIncomeChangeH2);

            //4: fra factB til endOfTheDayScreen og tilføjer daily income til ens moneybar 
            endOfDayManager.EnableEndOfDayScreen(factH);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceH1Picked, iGamestate.decisionI, iGamestate.decisionI, continueButtonH);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
            buttonManager.CheckLoosingCondition(iGamestate.buttonI1, iGamestate.buttonI2);

            //7: updates currentButtons and choices
            buttonManager.UpdateCurrentButtons(iGamestate.buttonI1, iGamestate.buttonI2, iGamestate.costOfChoiceI1, iGamestate.costOfChoiceI2);
        }
    }
}
