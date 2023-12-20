using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class I_GameState : MonoBehaviour
{
    [Header("Managers")]
    public ButtonManager buttonManager;
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public J_GameState jGamestate;
    public DayToDay DayToDayController;

    [Header("Game Objects")]
    public GameObject decisionI;
    public GameObject impactI1;
    public GameObject impactI2;
    public GameObject factI;
    public GameObject continueButtonI;

    [Header("Bools")]
    public bool choiceI1Picked;
    public bool choiceI2Picked;

    [Header("Buttons")]
    public Button buttonI1;
    public Button buttonI2;

    [Header("Choice1 Variables")]
    public float costOfChoiceI1;
    public float healthOfChoiceI1;
    public float moodOfChoiceI1;
    public float temporaryIncomeChangeI1;
    public float permanentIncomeChangeI1;

    [Header("Choice 2 Variables")]
    public float costOfChoiceI2;
    public float healthOfChoiceI2;
    public float moodOfChoiceI2;
    public float temporaryIncomeChangeI2;
    public float permanentIncomeChangeI2;

    public void ButtonI1Click()
    {
        buttonManager.OnChoiceButtonClick(decisionI, impactI1, continueButtonI);
        barManager.UpdateHealthBar(healthOfChoiceI1);
        barManager.UpdateMoodBar(moodOfChoiceI1);
        barManager.UpdateMoneyBar(costOfChoiceI1);
        choiceI1Picked = true;
    }

    public void ButtonI2Click()
    {
        buttonManager.OnChoiceButtonClick(decisionI, impactI2, continueButtonI);
        barManager.UpdateHealthBar(healthOfChoiceI2);
        barManager.UpdateMoodBar(moodOfChoiceI2);
        barManager.UpdateMoneyBar(costOfChoiceI2);
        choiceI2Picked = true;
    }

    public void ContinueButtonIClick()
    {
        if (impactI1.activeInHierarchy || impactI2.activeInHierarchy)
        {
            //1: fra ConsequenceC til factC
            buttonManager.ContinueToFact(impactI1, impactI2, factI);
        }
        else if (factI.activeInHierarchy)
        {
            //2: knapper, der ikke er råd til ved decisionb skal gøres non-interactable
            buttonManager.DisableButton(jGamestate.costOfChoiceJ1, jGamestate.buttonJ1, jGamestate.costOfChoiceJ2, jGamestate.buttonJ2);

            //3: updater Income for the day text & permanent income
            endOfDayManager.UpdateIncomeForTheDay1(choiceI1Picked, temporaryIncomeChangeI1, temporaryIncomeChangeI2);
            endOfDayManager.UpdateAverageDailyIncome2(choiceI1Picked, permanentIncomeChangeI1, permanentIncomeChangeI2);

            //4: fra factB til endOfTheDayScreen og tilføjer daily income til ens moneybar 
            endOfDayManager.EnableEndOfDayScreen(factI);
        }

        else if (endOfDayManager.endOfDayScreen.activeInHierarchy)
        {
            DayToDayController.NextDay();

            //5: fra endOfTheDayScreen til decision c eller d
            endOfDayManager.DisableEndOfDayScreen(choiceI1Picked, jGamestate.decisionJ, jGamestate.decisionJ, continueButtonI);

            //6: tjek om loosing conditions bliver mødt (om ingen knapper er interactable og askANeighborg er inaktiv)
            buttonManager.CheckLoosingCondition(jGamestate.buttonJ1, jGamestate.buttonJ2);
        }
    }
}
    /*
    public void DisableAANBI()
    {
        // disabler ask a neighbor button, tiløfjer x antal penge og tjekker loosinjg conditions, hvis man er inde i decisionI
        buttonManager.DisableAskANeighborButton(decisionI);
    }
    */

