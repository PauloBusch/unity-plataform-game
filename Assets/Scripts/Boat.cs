using UnityEngine;

public class Boat : MonoBehaviour
{
    public float Velocity = -0.5f;
    public float Range = 6.4f;

    private Vector2 _initialPosition;
    private float _iterations;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void Update()
    {
        _iterations += Velocity * Time.deltaTime;

        var positionX = Mathf.Cos(_iterations) * Range;

        transform.position = _initialPosition + new Vector2(positionX, 0);

        if (_iterations >= 2 * Mathf.PI)
            _iterations = 2 * Mathf.PI - _iterations;
    }
}
