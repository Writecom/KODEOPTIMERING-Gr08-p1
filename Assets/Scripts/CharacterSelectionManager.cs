using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    // rules variable NotUsed
    [SerializeField]
    private GameObject characterHawa, characterCarl, rules, characterSelection;
    [SerializeField]
    private Button nextButton, previousButton, selectCharacterButton;
    [SerializeField]
    private TMP_Text selectCharacterButtonText;


    public void LoadNextCharacter()
    {
        if (characterHawa.activeInHierarchy == true)
        {
            characterHawa.SetActive(false);
            characterCarl.SetActive(true);
            selectCharacterButton.interactable = false;
            nextButton.interactable = false;
            previousButton.interactable = true;
            selectCharacterButtonText.text = "NOT AVAILABLE";

        }
    }
    public void LoadPreviousCharacter()
    {
        if (characterCarl.activeInHierarchy == true)
        {
            characterCarl.SetActive(false);
            characterHawa.SetActive(true);
            nextButton.interactable = true;
            previousButton.interactable = false;
            selectCharacterButton.interactable = true;
            selectCharacterButtonText.text = "SELECT CHARACTER";
        }
    }

    // NotUsed
    public void LoadRules()
    {
        characterSelection.SetActive(false);
        rules.SetActive(true);
    }

    // NotUsed
    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
