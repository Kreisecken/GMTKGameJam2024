using Unity.VisualScripting;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    public SpawnerSpawnerScript spawnerSpawner;
    
    void OnMouseDown() {
        spawnerSpawner.StartRound();
    }
}
