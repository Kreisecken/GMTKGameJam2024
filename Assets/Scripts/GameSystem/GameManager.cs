using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public SceneAsset MainMenuScene;
    public SceneAsset GameScene;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("Multiple Game instances detected. Destroying the new one.");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene.name);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GameScene.name);
    }
}