
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource myAudioSource;
    
    // Method to keep music active through scenes
    
    void Awake()
    {
        DontDestroyOnLoad(myAudioSource);
    }
    

    // Turns music off if in scene index 4 or 5
    
    
    void StopMusic()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            myAudioSource.Stop();
        }
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            myAudioSource.Stop();
        }
    }

    
    
    // ReSharper disable Unity.PerformanceAnalysis
    
    /*
    void RestartMainMusic()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 1);
        {
            myAudioSource.Play();
        }
        
    }*/
    
    
    private void Update()
    {
        StopMusic();
        //RestartMainMusic();
    }
    
}
