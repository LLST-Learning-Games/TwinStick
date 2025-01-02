using Entities;
using UnityEngine;

public class PlayerMovementComponent : EntityComponent
{
    [SerializeField] private float _moveSpeed = 1.5f;
    
    private PlayerController _playerController;

    public override void Initialize(GameObject parent)
    {
        _playerController = parent?.GetComponent<PlayerController>();
    }

    public override void UpdateComponent()
    {
        if (_playerController.IsDead)
        {
            return;
        }
        
        var movement = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }
        transform.position += movement * Time.deltaTime * _moveSpeed;
    }
}
