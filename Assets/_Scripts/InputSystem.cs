using UnityEngine;

public abstract class InputSystem 
{
    protected bool IsDragging = false;

    protected Camera Camera;

    protected GameObject PeakedObject;

    protected Vector3 TouchPosition;

    public InputSystem(Camera camera) {
        Camera = camera;
    }

    public virtual bool OnMouseDown(GameObject peakedObject) {

        bool value = Input.GetMouseButtonDown(0) && !IsDragging;

        if (value) {

            IsDragging = true;

            PeakedObject = peakedObject;
        }

        return value;
    }

    public abstract void OnMouseDrag();

    public abstract void OnMouseUp();
    
}