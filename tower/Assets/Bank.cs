using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int lifes = 10;
    [SerializeField] int startingBalance = 150;

    [SerializeField] int currentBalance;
    public int CurrentBalance{ get { return currentBalance;} }

    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] TextMeshProUGUI displayLifes;

    void Awake()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit (int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
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
            ReloadScene();
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold:" + currentBalance;
        displayLifes.text = "Life:" + lifes;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
