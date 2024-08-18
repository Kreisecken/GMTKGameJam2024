using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ProjectileBehaviourContainer))]
public class Projectile : MonoBehaviour
{
    public bool fired = false;

    public Rigidbody2D Rigidbody2D { get; private set; }

    public bool DoesInteractWithPlayer  { get; private set; } = false;
    public bool DoesInteractWithEnemies { get; private set; } = false;
    public bool DoesInteractWithTowers  { get; private set; } = false;

    private ProjectileBehaviourContainer _behaviourContainer = null;
    private ProjectileBehaviourContainer BehaviourContainer => _behaviourContainer ??= GetComponent<ProjectileBehaviourContainer>();

    public void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public Projectile FireProjectile()
    {
        fired = true;
        gameObject.SetActive(true);
        return this;
    }

    
    public Projectile AddBehaviour<T>(T behaviour = null, Action<T> setup = null) where T : ProjectileBehaviour, new()
    {
        behaviour ??= new();

        setup?.Invoke(behaviour);

        BehaviourContainer.AddProjectileBehaviour(behaviour);

        return this;
    }

    public Projectile AddBehaviour<T>(Action<T> setup = null) where T : ProjectileBehaviour, new()
        => AddBehaviour(new(), setup);

    public T GetBehaviour<T>() where T : ProjectileBehaviour
    {
        return BehaviourContainer.GetProjectileBehaviour<T>();  
    }

    public bool TryGetBehaviour<T>(out T behaviour) where T : ProjectileBehaviour
    {
        return BehaviourContainer.TryGetProjectileBehaviour(out behaviour);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public Projectile InteractWithPlayer () { DoesInteractWithPlayer  = true; return this; }
    public Projectile InteractWithEnemies() { DoesInteractWithEnemies = true; return this; }
    public Projectile InteractWithTowers () { DoesInteractWithTowers  = true; return this; }

    public Projectile SetInteractions(bool player, bool enemies, bool towers)
    {
        if (player ) InteractWithPlayer ();
        if (enemies) InteractWithEnemies();
        if (towers ) InteractWithTowers ();

        return this;
    }
}