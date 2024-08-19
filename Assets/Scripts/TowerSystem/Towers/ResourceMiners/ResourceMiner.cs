using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceMiner : PlaceableTower
{
    [Header("Simple Miner Tower Properties")]
    public float mineRate = 1f;

    private float internalMineRate;

    public new void Update()
    {
        if (!placed) return;

        internalMineRate -= Time.deltaTime;

        if (internalMineRate <= 0)
        {
            Debug.Log("Mining resources...");

            internalMineRate = mineRate;
        }
    }
}