using UnityEngine;

public class DragSystem : InputSystem
{
    private float _speed;

    private Vector2 _offset = Vector2.zero;

    private ItemComponentsConteiner _itemController;

    public DragSystem(Camera camera, float speed = 50) : base(camera) {
        _speed = speed;
    }

    public override bool OnMouseDown(GameObject peakedObject) {

        if (base.OnMouseDown(peakedObject)) {
            TouchPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            _offset = TouchPosition - peakedObject.transform.position;

            peakedObject.TryGetComponent<ItemComponentsConteiner>(out ItemComponentsConteiner itemController);
            if (itemController) {
                _itemController = itemController;
                _itemController.OnDrag();
            }
        }

        return true;
    }

    public override void OnMouseDrag() {
        if (!IsDragging || !PeakedObject) return;

        Vector2 currentPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        PeakedObject.transform.position = Vector2.Lerp(PeakedObject.transform.position, currentPosition - _offset, _speed * Time.deltaTime);
    }

    public override void OnMouseUp() {
        PeakedObject = null;
        IsDragging = false;
        if (_itemController) {
            _itemController.OffDrag();
            _itemController = null;
        } 
    }
}