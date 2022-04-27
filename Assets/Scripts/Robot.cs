using System.Linq;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public float Velocity = 3;
    public float Distance = 0.6f;
    public Collider2D HeadCollider;
    public LayerMask LayerMaskSolids;
    public LayerMask LayerMaskPlayer;

    private bool _broken;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeepWater"))
        {
            Broken();
        }
    }

    void Update()
    {
        if (_broken) return;

        var hits = Physics2D.RaycastAll(transform.position, transform.right, Distance, LayerMaskSolids);
        var hitSolid = hits.FirstOrDefault(h => h.collider?.name != name);
        if (hitSolid.collider != null)
        {
            Velocity *= -1;
            transform.Rotate(new Vector2(0, 180));
        }

        var hitPlayerInFront = Physics2D.Raycast(transform.position, transform.right, Distance, LayerMaskPlayer);
        if (hitPlayerInFront.collider != null)
        {
            SceneController.GameOver();
        }        

        var hitPlayerInTop = Physics2D.Raycast(transform.position, transform.up, Distance, LayerMaskPlayer);
        if (hitPlayerInTop.collider != null)
        {
            Broken();
        }

        _rigidbody2D.velocity = new Vector2(Velocity, _rigidbody2D.velocity.y);
    }

    void Broken()
    {
        _broken = true;
        _animator.SetBool("broken", true);
        HeadCollider.enabled = false;
    }
}
