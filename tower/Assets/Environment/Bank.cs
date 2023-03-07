using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] int lifes = 15;
    [SerializeField] int startingBalance = 120;
    [SerializeField] int kills = 0;
    [SerializeField] int lefts = 15;

    [SerializeField] int currentBalance;
    public int CurrentBalance{ get { return currentBalance;} }

    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] TextMeshProUGUI displayLifes;
    [SerializeField] TextMeshProUGUI displayKills;
    [SerializeField] TextMeshProUGUI displayLefts;
    

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            NextLevel();
        }
    }

    public void Deposit (int amount)
    {
        currentBalance += Mathf.Abs(amount);
        kills += 1;
        lefts -= 1;
        UpdateDisplay();
        if (lefts == 0) 
        {
            StartSuccessSequence();
        }
    }

    public void Withdraw (int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void LooseLife (int amount)
    {
        lifes -= Mathf.Abs(amount);
        UpdateDisplay();

        if (lifes <= 0)
        {
            //게임오버
            ReloadLevel();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold:" + currentBalance;
        displayLifes.text = "Life:" + lifes;
        displayKills.text = "Kills:" + kills;
        displayLefts.text = "Lefts:" + lefts;
    }

    void StartSuccessSequence() 
    {
        // audioSource.Stop();
        // audioSource.PlayOneShot(success);
        // successParticles.Play();
        Invoke("NextLevel", levelLoadDelay);
    }

   void ReloadLevel() 
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
   }

   void NextLevel() 
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextScneneIndex = currentSceneIndex + 1;
    if (nextScneneIndex == SceneManager.sceneCountInBuildSettings) {
        nextScneneIndex = 0;
    }
    SceneManager.LoadScene(nextScneneIndex);
   }
}
