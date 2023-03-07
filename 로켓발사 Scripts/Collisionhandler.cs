using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collisionhandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other) {

        if (isTransitioning || collisionDisabled) {
            return;
        }
            
            switch (other.gameObject.tag) {

                case "friendly":
                    Debug.Log("friendly");
                    break;

                case "Finish":
                    StartSuccessSequence();
                    break;

                case "Fuel":
                    Debug.Log("Fuel");
                    break;

                default:
                    StartCrashSequence();
                    break;
            
        }
   }


    void StartSuccessSequence() {
        
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
    }
    
   void StartCrashSequence() {
    
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
   }

   void ReloadLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
   }

   void NextLevel() {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextScneneIndex = currentSceneIndex + 1;
    if (nextScneneIndex == SceneManager.sceneCountInBuildSettings) {
        nextScneneIndex = 0;
    }
    SceneManager.LoadScene(nextScneneIndex);
   }
}
