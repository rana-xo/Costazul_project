using UnityEngine;


public class PlayerMeshPosition : MonoBehaviour
{
    private Vector2 _input;
    private Vector3 _moveDirection;

    private void Update()
    {
        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        _moveDirection = new Vector3(_input.x, 0f, _input.y);

        if (_moveDirection != Vector3.zero)
        {
            transform.forward = _moveDirection;
        }
    }
}
