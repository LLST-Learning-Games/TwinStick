using System.Collections.Generic;
using Entities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<EntityComponent> _components;
    
    public bool IsDead { get; private set; } = false;

    public void SetIsDead()
    {
        IsDead = true;
    }
    
    private void Awake()
    {
        Cursor.visible = false;
        
        foreach (var component in _components)
        {
            component.Initialize(gameObject);
        }
    }

    void FixedUpdate()
    {
        foreach (var component in _components)
        {
            component.UpdateComponent();
        }
    }
    
}
