using UnityEngine;
using UnityEngine.UIElements;

public class GameStateManager : MonoBehaviour
{
    public GameObject losingTower;
    public UIDocument gameOverUI;
    
    void FixedUpdate()
    {
        if(losingTower == null) {
            GameOver();
        }
    }
    
    private void GameOver()
    {
        gameOverUI.enabled = true;
    }
}
