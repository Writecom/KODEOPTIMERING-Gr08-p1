using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController1 : MonoBehaviour
{

    public static SceneController1 instance;

    [SerializeField] Animator transitionAnim;

    public void NextScene()
    {
        StartCoroutine(LoadScene());
    }

    public void PlayWinningScene()
    {
        StartCoroutine (LoadWinningScene());
    }

    public void GetMainMenu()
    {
        StartCoroutine(LoadMainMenu());
    }

    public void PlayGameOverScene()
    {
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        transitionAnim.SetTrigger("Start");
    }
    IEnumerator LoadWinningScene()
    {
        transitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 2);

        transitionAnim.SetTrigger("Start");
    }
    IEnumerator LoadMainMenu()
    {
        transitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync("Main Menu");

        transitionAnim.SetTrigger("Start");
    }
    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSeconds(5);

        transitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync("GameOver");

        transitionAnim.SetTrigger("Start");
    }

    //NEDENST�ENDE KODE, SKAL BRUGES AF HVEM END DER FOR AT OPTIMERE DETTE SCRIPT TIL OPGAVE TIL IKDU
    //JEG HAR LAVET DET NU, FORDI JEG SKAL BRUGE SK�RMBILLEDE TIL RAPPORT
    //SP�RG MIG (FREDERIK), HVIS DER ER SP�RGSM�L TIL HVAD DER SKAL G�RES HELT KONKRET.
    /*
    IEnumerator LoadScene(int sceneIndex)
    {
        transitionAnim.SetTrigger("End");

        yield return new WaitForSeconds(1);

        SceneManager.LoadSceneAsync(sceneIndex);

        transitionAnim.SetTrigger("Start");
    }
    */
}
