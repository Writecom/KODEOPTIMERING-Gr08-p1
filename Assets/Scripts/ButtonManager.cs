using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public BarManager barManager;
    public EndOfDayManager endOfDayManager;
    public GameObject askANeighborButton;
    public I_GameState iGamestate;
    public GameObject villageBackground;
    public J_GameState jGamestate;



    private float delayedTimeInSeconds = 5f;

    private Button currentButton1;
    private Button currentButton2;
    private float currentCostOfChoice1;
    private float currentCostOfChoice2;
    private float moneyAddedFromAANB = 5f;

    [Header("SceneController")]
    public SceneController1 sceneController;

    /// <summary>
    /// Disables (makes buttons non-interactable) if the costOfChoice is higher than the ammount of money the player has.
    /// </summary>
    /// 

    public void DisableButton(float costOfChoice1, Button button1, float costOfChoice2, Button button2)
    {
        costOfChoice1 *= -1;
        costOfChoice2 *= -1;
        if (barManager.moneyValueTotal < costOfChoice1)
        {
            button1.interactable = false;
        }
        if (barManager.moneyValueTotal < costOfChoice2)
        {
            button2.interactable = false;
        }
    }

    //BENYTTES
    /// <summary>
    /// Sets ups a loosing condition for the player. If non of the buttons are interactable and the player do not have the askANeighborButton
    /// the GameOverScreen is loaded using the PlayGameOverScreen
    /// </summary>
    public void CheckLoosingCondition(Button button1, Button button2)
    {
        if (!button1.interactable && !button2.interactable && !askANeighborButton.activeInHierarchy)
        { 
            sceneController.PlayGameOverScene();

        } 
    }

    /// <summary>
    /// Disables the decision and enable the consequence and continueButton
    /// </summary>
    public void OnChoiceButtonClick(GameObject decision, GameObject consequence, GameObject continueButton)
    {
        decision.SetActive(false);
        villageBackground.SetActive(false);
        consequence.SetActive(true);
        continueButton.SetActive(true);
    }

    /// <summary>
    /// Disables consequences and enables fact
    /// </summary>
    public void ContinueToFact(GameObject consequence1, GameObject consequence2, GameObject fact)
    {
        consequence1.SetActive(false);
        consequence2.SetActive(false);
        fact.SetActive(true);
    }

    /// <summary>
    /// Disables fact and continuebutton and enables the next decision
    /// </summary>

    public void EnableNextDecision(GameObject fact, GameObject nextDecision, GameObject continueButton)
    {
        fact.SetActive(false);
        continueButton.SetActive(false);
        villageBackground.SetActive(true);
        nextDecision.SetActive(true);
    }

    /// <summary>
    /// Disables fact and continuebutton and enables either nextDecision 1 or 2 based on the consequencePicked
    /// </summary>

    public void EnableNextDecision1or2(GameObject fact, bool consequence1Picked, GameObject nextDecision1, GameObject nextDecision2, GameObject continueButton)
    {
        fact.SetActive(false);
        continueButton.SetActive(false);
        villageBackground.SetActive(true);

        if (consequence1Picked)
        {
            nextDecision1.SetActive(true);
        }
        else
        {
            nextDecision2.SetActive(true);
        }
    }

    /// <summary>
    /// Enables AskANeighborButton if it is not active in hiererachy
    /// </summary>
    public void EnableAskANeighborButton()
    {
        if (askANeighborButton.activeInHierarchy == false)
        {
            askANeighborButton.SetActive(true);
        }
    }

    /// <summary>
    /// Updates current buttons variables
    /// </summary>
 
    public void UpdateCurrentButtons(Button button1, Button button2, float costOfChoice1, float costOfChoice2)
    {
        currentButton1 = button1;
        currentButton2 = button2;
        currentCostOfChoice1 = costOfChoice1;
        currentCostOfChoice2 = costOfChoice2;
    }


    //BENYTTES
    /// <summary>
    /// Disables the AANB and updates the Moneybar wth the money.
    /// If the player is on a decision, the DisableButton method will be run again using the current costs and buttons and the loosing conditions will be checked
    /// </summary>
    
    public void DisableAskANeighborButton()
    {       
        barManager.UpdateMoneyBar(moneyAddedFromAANB);
        askANeighborButton.SetActive(false);

        if (jGamestate.decisionJ.activeInHierarchy)
        {
            DisableButton(currentCostOfChoice1, currentButton1, currentCostOfChoice2, currentButton2);
            CheckLoosingCondition(currentButton1, currentButton2);
        }
        if (iGamestate.decisionI.activeInHierarchy)
        {
            DisableButton(currentCostOfChoice1, currentButton1, currentCostOfChoice2, currentButton2);
            CheckLoosingCondition(currentButton1, currentButton2);
        }
    }



    





        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
}
