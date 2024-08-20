using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceMiner : PlaceableTower
{
    [Header("Simple Miner Tower Properties")]

    public float mineRate = 1f;
    public int mineAmount = 1;

    public enum Type
    {
        BlueCrystal,
        GreenCrystal,
        Money
    }

    public Type type;

    public int minAmount = 1;
    public int maxAmount = 5;

    private float internalMineRate;

    public void Update()
    {
        if (!placed) return;

        if (!GameStateManager.canStartRound) internalMineRate -= Time.deltaTime; // only continue time when round is active

        if (internalMineRate <= 0)
        {
            Debug.Log("Mining resources...");

            float extra = (transform.localScale.x + transform.localScale.y) * 0.25f;

            switch (type)
            {
                case Type.BlueCrystal:
                    PlayerInventory.Instance.AddBlueCrystal(Random.Range(minAmount, maxAmount * (int)extra));
                    break;
                case Type.GreenCrystal:
                    PlayerInventory.Instance.AddGreenCrystal(Random.Range(minAmount, maxAmount * (int)extra));
                    break;
                case Type.Money:
                    PlayerInventory.Instance.AddMoney(Random.Range(minAmount, maxAmount * (int)extra));
                    break;
            }
            
            internalMineRate = mineRate;
        }
    }
}