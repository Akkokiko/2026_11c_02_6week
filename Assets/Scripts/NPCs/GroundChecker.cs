
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundChecker : MonoBehaviour
{
    [Header("Ground Check")]
    public LayerMask floorLayer;
    public float groundCheckDistance = 0.3f;
    public float groundCheckForwardOffset = 0.05f;

    private Collider2D bodyCollider;

    private void Awake()
    {
        bodyCollider = GetComponent<Collider2D>();
    }

    public bool HasGroundAhead(float direction)
    {
        if (bodyCollider == null)
            return false;

        Bounds bounds = bodyCollider.bounds;

        float x = bounds.center.x +
                  direction * (
                      bounds.extents.x +
                      groundCheckForwardOffset
                  );

        float y = bounds.min.y + 0.05f;

        Vector2 origin = new Vector2(x, y);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            groundCheckDistance,
            floorLayer
        );

        Debug.DrawRay(
            origin,
            Vector2.down * groundCheckDistance,
            hit.collider != null ? Color.green : Color.red
        );

        return hit.collider != null;
    }
}
