using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStateManager : MonoBehaviour
{
    public GameObject losingTower;
    public UIDocument gameOverUI;
    public GameObject startButton;
    public TMP_Text waveLabel;
    
    private bool waveRunning = false;
    
    public static bool canStartRound = false;
    private int currentWave = 1;
    
    void FixedUpdate()
    {
        if(losingTower == null)
        {
            GameOver();
            return;
        }
        
        if(!waveRunning && Enemy.Enemies.Count == 0)
        {
            if(!canStartRound)
            {
                canStartRound = true;
                startButton.SetActive(true);
            }
        }
        else
        {
            if(canStartRound)
            {
                canStartRound = false;
                startButton.SetActive(false);
            }
        }
    }
    
    private void GameOver()
    {
        gameOverUI.enabled = true;
    }
    
    public void SetWaveRunning(bool running)
    {
        waveRunning = running;
    }
    
    public void SetCurrentWave(int wave)
    {
        currentWave = wave;
        waveLabel.text = $"wave {wave}";
    }
}
