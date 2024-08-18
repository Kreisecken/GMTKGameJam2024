using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Projectile))]
public class ProjectileBehaviourContainer : MonoBehaviour
{
    public List<ProjectileBehaviour> projectileBehaviours = new();
 
    private Projectile projectile;

    public void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    public void AddProjectileBehaviour(ProjectileBehaviour projectileBehaviour)
    {
        projectileBehaviours.Add(projectileBehaviour);
    }

    public T GetProjectileBehaviour<T>() where T : ProjectileBehaviour
    {
        return projectileBehaviours.Find(behaviour => behaviour is T) as T;
    }
    
    public bool TryGetProjectileBehaviour<T>(out T projectileBehaviour) where T : ProjectileBehaviour
    {
        projectileBehaviour = GetProjectileBehaviour<T>();
        return projectileBehaviour != null;
    }

    public void RemoveProjectileBehaviour(ProjectileBehaviour projectileBehaviour)
    {
        projectileBehaviours.Remove(projectileBehaviour);
    }

    public void Start()
    {
        if (!projectile.fired) return;

        foreach (ProjectileBehaviour projectileBehaviour in projectileBehaviours)
            projectileBehaviour.Start(projectile);
    }

    public void Update()
    {
        if (!projectile.fired) return;

        foreach (ProjectileBehaviour projectileBehaviour in projectileBehaviours)
            projectileBehaviour.Update(projectile);
    }

    public void FixedUpdate()
    {
        if (!projectile.fired) return;

        foreach (ProjectileBehaviour projectileBehaviour in projectileBehaviours)
            projectileBehaviour.FixedUpdate(projectile);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        foreach (ProjectileBehaviour projectileBehaviour in projectileBehaviours) 
        {
            if (collider.gameObject.TryGetComponent(out Player player)) projectileBehaviour.OnHit(projectile, player);
            if (collider.gameObject.TryGetComponent(out Enemy enemy  )) projectileBehaviour.OnHit(projectile, enemy );
            if (collider.gameObject.TryGetComponent(out Tower tower  )) projectileBehaviour.OnHit(projectile, tower );
        }
    }
}