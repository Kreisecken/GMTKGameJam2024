using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceMiner : PlaceableTower
{
    [Header("Simple Miner Tower Properties")]
    public float mineRate = 1f;
    public int mineAmount = 1;

    private float internalMineRate;

    public void Update()
    {
        if (!placed) return;

        if (Enemy.Enemies.Count != 0) internalMineRate -= Time.deltaTime; // TODO: optimize enemy-count-check (?) (by adding a statically accessible boolean variable that stores if a round is currently active (enemies are alive))

        if (internalMineRate <= 0)
        {
            Debug.Log("Mining resources...");
            PlayerInventory.Instance.AddMoney(mineAmount); // TODO: drop collectable money instead (if the player was not removed)
            
            internalMineRate = mineRate;
        }
    }
}