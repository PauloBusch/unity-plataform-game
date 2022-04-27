using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Mario;
    public float OffsetY = 2;

    void Start()
    {
        
    }

    void Update()
    {
        var position = new Vector3(
            Mario.transform.position.x,
            Mario.transform.position.y + OffsetY,
            -10
        );
        transform.position = position;
    }
}
