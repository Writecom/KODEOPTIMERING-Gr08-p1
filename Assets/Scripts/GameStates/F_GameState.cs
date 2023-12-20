using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public E_GameState eGamestate;
    public H_GameState hGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionF;
    public GameObject impactF1;
    public GameObject impactF2;
    public GameObject factF;
    public GameObject continueButtonF;

    [Header("Bools")]
    public bool choiceF1Picked;
    public bool choiceF2Picked;

    [Header("Buttons")]
    public Button buttonF1;
    public Button buttonF2;

    [Header("Choice1")]
    public float costOfChoiceF1;
    public float healthOfChoiceF1;
    public float moodOfChoiceF1;
    public float temporaryIncomeChangeF1;
    public float permanentIncomeChangeF1;

    [Header("Choice 2")]
    public float costOfChoiceF2;
    public float healthOfChoiceF2;
    public float moodOfChoiceF2;
    public float temporaryIncomeChangeF2;
    public float permanentIncomeChangeF2;

    public void ButtonF1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionF, impactF1, continueButtonF);
        barManager.UpdateHealthBar(healthOfChoiceF1);
        barManager.UpdateMoodBar(moodOfChoiceF1);
        barManager.UpdateMoneyBar(costOfChoiceF1);
        choiceF1Picked = true;
    }

    public void ButtonF2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionF, impactF2, continueButtonF);
        barManager.UpdateHealthBar(healthOfChoiceF2);
        barManager.UpdateMoodBar(moodOfChoiceF2);
        barManager.UpdateMoneyBar(costOfChoiceF2);
        choiceF2Picked = true;
    }

    public void ContinueButtonFClick()
    {
        if (impactF1.activeInHierarchy || impactF2.activeInHierarchy)
        {
            //1: fra ConsequenceB til factB
            buttonManager.ContinueToFact(impactF1, impactF2, factF);
        }
        else if (factF.activeInHierarchy)
        {
            //2: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay2(choiceF1Picked, temporaryIncomeChangeF1, temporaryIncomeChangeF2);
            endOfDayManager.UpdateAverageDailyIncome2(choiceF1Picked, permanentIncomeChangeF1, permanentIncomeChangeF2);

            //3: enables end of day screen og opdaterer moneybar med daily income
            endOfDayManager.EnableEndOfDayScreen(factF);

            //4: knapper, der ikke er råd til ved decisionB skal gøres non-interactable for D eller D
            buttonManager.DisableButton(hGamestate.costOfChoiceH1, hGamestate.buttonH1, hGamestate.costOfChoiceH2, hGamestate.buttonH2);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceF1Picked, hGamestate.decisionH, hGamestate.decisionH, continueButtonF);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
                buttonManager.CheckLoosingCondition(hGamestate.buttonH1, hGamestate.buttonH2);

            //7: updates currentButtons and choices
            buttonManager.UpdateCurrentButtons(hGamestate.buttonH1, hGamestate.buttonH2, hGamestate.costOfChoiceH1, hGamestate.costOfChoiceH2);
        }
    }
}
