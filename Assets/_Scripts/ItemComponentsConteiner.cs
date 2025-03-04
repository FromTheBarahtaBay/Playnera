using UnityEngine;

public class ItemComponentsConteiner : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; set; }
    public BoxCollider2D BoxCollider2D { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }

    public void OnDrag() {
        Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        Rigidbody2D.velocity = Vector2.zero;
    }

    public void OffDrag() {
        EvetsSystem.OnIsItemDropped(this);
        Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}