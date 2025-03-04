using UnityEngine;

public class InputSystemController : IUpdates
{
    private GameObject _peakedObject = null;
    private DragSystem _dragSystem;
    private ScrollSystem _scrollSystem;
    private InputSystem _currentSystem;
    private Camera _camera;
    private bool _isdraging = false;

    public InputSystemController(Camera camera, float speedDrag, float speedScroll) {
        _camera = camera;
        _dragSystem = new DragSystem(camera, speedDrag);
        _scrollSystem = new ScrollSystem(camera, speedScroll);
        _currentSystem = _dragSystem;
    }

    public void OnUpdate() {

        CheckInput();

        if (!_isdraging || !_peakedObject) return;

        if (_peakedObject.CompareTag("Movable")) {
            _currentSystem = _dragSystem;
        } else {
            _currentSystem = _scrollSystem;
        }

        _currentSystem.OnMouseDown(_peakedObject);
        _currentSystem.OnMouseDrag();
    }

    private void CheckInput() {

        if (Input.GetMouseButtonDown(0)) {

            _isdraging = true;

            var touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = new RaycastHit2D[1];

            if (Physics2D.RaycastNonAlloc(touchPosition, Vector2.zero, hits) > 0) {
                var hit = hits[0].collider.gameObject;

                if (hit.CompareTag("Movable") || hit.CompareTag("Background")) {
                    _peakedObject = hit;
                } else
                    _peakedObject = null;
            }
        } else if (Input.GetMouseButtonUp(0)) {
            _currentSystem.OnMouseUp();
            _peakedObject = null;
            _isdraging = false;
        }
    }
}