using Entities;
using UnityEngine;

public class PlayerMovementComponent : EntityComponent
{
    [SerializeField] private float _moveSpeed = 1.5f;
    [SerializeField] private Animator _lowerBodyAnimator;
    
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

        if (_lowerBodyAnimator)
        {
            _lowerBodyAnimator.SetFloat("X", movement.x);
            _lowerBodyAnimator.SetFloat("Y", movement.y);
        }
        transform.position += movement * Time.deltaTime * _moveSpeed;
    }
}
