using UnityEngine;

public class Mario : MonoBehaviour
{
    public float Velocity = 5;
    public float Jump = 350;
    public LayerMask LayerMaskRobots;

    private bool _jumpping = false;
    private bool _drowning = false;
    private bool _right = true;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _jumpping = false;
        transform.parent = collision.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeepWater"))
        {
            _drowning = true;
            _animator.SetBool("drowning", true);

            Invoke(nameof(GameOver), 1.5f);
        }
    }

    void Update()
    {
        if (_drowning) return;

        var xAxis = Input.GetAxis("Horizontal");
        _rigidbody2D.velocity = new Vector2(xAxis * Velocity, _rigidbody2D.velocity.y);
        _animator.SetBool("stopped", _jumpping ? true : xAxis == 0);

        if (_right && xAxis < 0 || !_right && xAxis > 0)
        {
            transform.Rotate(new Vector2(0, 180));
            _right = !_right;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_jumpping)
        {
            _jumpping = true;
            transform.parent = null;
            _rigidbody2D.AddForce(new Vector2(0, Jump));
        }
    }

    void GameOver() => SceneController.GameOver();
}
