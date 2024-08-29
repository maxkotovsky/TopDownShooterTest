using UnityEngine;
using Gamecore;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _acceleration = 5;
    [SerializeField]
    private float _moveTime = 0.1f;

    private InputReader _input;
    private Vector3 _currentPos;
    private Vector3 _velocity;
    private Camera _mainCamera;
    private Vector2 _screenBounds;

    void Start()
    {
        _input = GetComponent<InputReader>();
        _mainCamera = Camera.main;
        _screenBounds = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    private void Update()
    {
        Vector3 inputMovement = new Vector3(_input.Move.x, _input.Move.y, 0f) * (_acceleration * Time.deltaTime);
        _currentPos += inputMovement;
        transform.position = Vector3.SmoothDamp(transform.position, _currentPos, ref _velocity, _moveTime);

        float maxY = _screenBounds.y * -0.75f;
        float minY = _screenBounds.y * 0.9f;
        float maxX = _screenBounds.x * 0.8f;
        float minX = _screenBounds.x * 0.8f;
        _currentPos.y = Mathf.Clamp(_currentPos.y, -minY, maxY);
        _currentPos.x = Mathf.Clamp(_currentPos.x, -minX, maxX);

    }
}
