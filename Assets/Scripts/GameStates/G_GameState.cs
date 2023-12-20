using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class G_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public H_GameState hGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionG;
    public GameObject impactG1;
    public GameObject impactG2;
    public GameObject factG;
    public GameObject continueButtonG;

    [Header("Bools")]
    public bool choiceG1Picked;
    public bool choiceG2Picked;

    [Header("Buttons")]
    public Button buttonG1;
    public Button buttonG2;

    [Header("Choice1")]
    public float costOfChoiceG1;
    public float healthOfChoiceG1;
    public float moodOfChoiceG1;
    public float temporaryIncomeChangeG1;
    public float permanentIncomeChangeG1;

    [Header("Choice 2")]
    public float costOfChoiceG2;
    public float healthOfChoiceG2;
    public float moodOfChoiceG2;
    public float temporaryIncomeChangeG2;
    public float permanentIncomeChangeG2;


    public void ButtonG1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionG, impactG1, continueButtonG);
        barManager.UpdateHealthBar(healthOfChoiceG1);
        barManager.UpdateMoodBar(moodOfChoiceG1);
        barManager.UpdateMoneyBar(costOfChoiceG1);
        choiceG1Picked = true;
    }

    public void ButtonG2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionG, impactG2, continueButtonG);
        barManager.UpdateHealthBar(healthOfChoiceG2);
        barManager.UpdateMoodBar(moodOfChoiceG2);
        barManager.UpdateMoneyBar(costOfChoiceG2);
        choiceG2Picked = true;
    }

    public void ContinueButtonGClick()
    {
        if (impactG1.activeInHierarchy || impactG2.activeInHierarchy)
        {
            //1: fra ConsequenceB til factB
            buttonManager.ContinueToFact(impactG1, impactG2, factG);
        }
        else if (factG.activeInHierarchy)
        {
            //2: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay2(choiceG1Picked, temporaryIncomeChangeG1, temporaryIncomeChangeG2);
            endOfDayManager.UpdateAverageDailyIncome2(choiceG1Picked, permanentIncomeChangeG1, permanentIncomeChangeG2);

            //3: enables end of day screen og opdaterer moneybar med daily income
            endOfDayManager.EnableEndOfDayScreen(factG);

            //4: knapper, der ikke er råd til ved decisionB skal gøres non-interactable for D eller D
            buttonManager.DisableButton(hGamestate.costOfChoiceH1, hGamestate.buttonH1, hGamestate.costOfChoiceH2, hGamestate.buttonH2);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceG1Picked, hGamestate.decisionH, hGamestate.decisionH, continueButtonG);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
            buttonManager.CheckLoosingCondition(hGamestate.buttonH1, hGamestate.buttonH2);

            //7: updates currentButtons and choices
            buttonManager.UpdateCurrentButtons(hGamestate.buttonH1, hGamestate.buttonH2, hGamestate.costOfChoiceH1, hGamestate.costOfChoiceH2);
        }
    }
}
