using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    public bool fired = false;

    protected virtual void FireProjectile()
    {
        fired = true;
    }
}