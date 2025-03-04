using UnityEngine;

public class ScrollSystem : InputSystem
{
    private float _dragSpeed;
    private Vector2 _bounds;

    public ScrollSystem(Camera camera, float speed = 1) : base (camera) {
        _dragSpeed = speed;
    }

    public override bool OnMouseDown(GameObject gameObject) {

        if (Input.GetMouseButtonDown(0) && !IsDragging) {
            IsDragging = true;
            TouchPosition = Input.mousePosition;
            gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer roomImage);
            if (roomImage) {
                _bounds = SetBounds(roomImage);
            }
        }

        return true;
    }

    public override void OnMouseDrag() {
        if (!IsDragging) return;

        Vector3 delta = Input.mousePosition - TouchPosition;
        float moveX = _dragSpeed * Time.deltaTime * -delta.x;

        Vector3 newPosition = Camera.transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x + moveX, _bounds.x, _bounds.y);

        Camera.transform.position = newPosition;
        TouchPosition = Input.mousePosition;
    }

    public override void OnMouseUp() {
        IsDragging = false;
    }

    private Vector2 SetBounds (SpriteRenderer spriteRenderer) {
        float cameraHalfWidth = Camera.orthographicSize * Camera.aspect;
        float minX = spriteRenderer.bounds.min.x + cameraHalfWidth;
        float maxX = spriteRenderer.bounds.max.x - cameraHalfWidth;
        return new Vector2(minX, maxX);
    }
}