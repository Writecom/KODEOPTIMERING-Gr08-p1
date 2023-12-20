using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{
    [SerializeField]
    private BarManager barManager;

    [SerializeField]
    private ButtonManager buttonManager;
 
    public TMP_Text incomeForTheDayText;
    public TMP_Text averageDailyIncomeText;

    private float averageDailyIncome = 5;
    private float incomeForTheDay;

    public GameObject endOfDayScreen;
    //public GameObject continueButtonEndScreen;

    ///<summary>
    ///Enables End Of Day Screen. If "fact" is active in hierarchy, it disables "fact" and enables end of day screen.
    ///It uses the method "UpdateMoneyBar" to add the daily income to the moneybar
    /// </summary>
    public void EnableEndOfDayScreen(GameObject fact)
    {
        //NotUsed redundant if statement
        if (fact.activeInHierarchy)
        {
            fact.SetActive(false);
            endOfDayScreen.SetActive(true);
            barManager.UpdateMoneyBar(incomeForTheDay);
        } 
    }

    /// <summary>
    /// Calculates and updates income for the day based on what choices was picked. If choice1 was picked, the temporaryIncomeChange1 is added
    /// to the averageDailyIncome and overrides the incomeForTheDay variable. Else it adds temporaryIncomeChange2. Then the IncomeForTheDayText is
    /// updates using the .text and setting the text on the players screen to incomeForTheDay using .ToString
    /// </summary>
    public void UpdateIncomeForTheDay1(bool choicePickedX, float temporaryIncomeChangeX1, float temporaryIncomeChangeX2)
    {
        if (choicePickedX)
        {
            incomeForTheDay = averageDailyIncome + temporaryIncomeChangeX1;
        }
        else
        {
            incomeForTheDay = averageDailyIncome + temporaryIncomeChangeX2;
        }
        incomeForTheDayText.text = incomeForTheDay.ToString();

    }

    /// <summary>
    /// Method is used after the first one, if the daily income must to calculated based on 2 or more decisions. Same method as before, but instead of adding
    /// temporaryIncomeChange to averageDaily, it adds it to the incomeForTheDay (calculated in the first method).
    /// </summary>
  
    public void UpdateIncomeForTheDay2(bool choicePickedZ, float temporaryIncomeChangeZ1, float temporaryIncomeChangeZ2)
    {
        if (choicePickedZ)
        {
            incomeForTheDay += temporaryIncomeChangeZ1;
        }
        else 
        {
            incomeForTheDay += temporaryIncomeChangeZ2;
        }
        incomeForTheDayText.text = incomeForTheDay.ToString();
    }

    
    /// <summary>
    /// Calculates and updates the average daily income based on what choices was picked, if there was any permanentIncomeChanges. Same as UpdateIncomeForTheDay 
    /// </summary>
    public void UpdateAverageDailyIncome1(bool choicePickedX, float permanentIncomeChangeX1, float permanentIncomeChangeX2)
    {
        if (choicePickedX)
        {
            averageDailyIncome += permanentIncomeChangeX1;
        }
        else
        {
            averageDailyIncome += permanentIncomeChangeX2;
        }
        averageDailyIncomeText.text = averageDailyIncome.ToString();
        
    }

    //NOT USED because same as ovenover
    //metoden opdaterer igen den nye daily income, hvis der har været 2 valg med en permanent change
    /// <summary>
    /// Method is used after the first one, if the average daily income must to calculated based on 2 or more decisions. Same as method 1
    /// </summary>

    public void UpdateAverageDailyIncome2(bool choicePickedZ, float permanentIncomeChangeZ1, float permanentIncomeChangeZ2)
    {
        if (choicePickedZ)
        {
            averageDailyIncome += permanentIncomeChangeZ1;
        }
        else
        {
            averageDailyIncome += permanentIncomeChangeZ2;
        }
        averageDailyIncomeText.text = averageDailyIncome.ToString();
    }

    //BENYTTES
    /// <summary>
    /// Disables end of the day screen and continueButton. Enables the next decision based on what consequence you picked.
    /// </summary>

    public void DisableEndOfDayScreen(bool choicePicked1, GameObject nextDecision1, GameObject nextDecision2, GameObject continueButton)
    {
        endOfDayScreen.SetActive(false);
        continueButton.SetActive(false);
        buttonManager.villageBackground.SetActive(true);

        if (choicePicked1)
        {
        nextDecision1.SetActive(true);
        }
        else
        {
        nextDecision2.SetActive(true);
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
