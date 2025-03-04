using UnityEngine;
using System;
using System.Collections;

public class Bootstrap : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Camera _camera;
    public Camera Camera { get { return _camera; } }

    [Header("Settings")]
    [SerializeField] private float _dragSpeed;
    [SerializeField] private float _scrollSpeed;

    [Header("Items")]
    [SerializeField] private ItemSO[] _items;

    private IUpdates _currentSystem;
    private ItemsController _itemsController;

    private void Awake() {
        if (!_camera) throw new Exception("Error(Bootstrap): компонент камера не назначен!");
        else _currentSystem = new InputSystemController(_camera, _dragSpeed, _scrollSpeed);

        if (_items.Length == 0) throw new Exception("Error(Bootstrap): нет добавленных элементов!");
        else _itemsController = new ItemsController(this, _items);
    }

    public void StartCorutines(IEnumerator enumerator) {
        StartCoroutine(enumerator);
    }

    private void Update() {
        _currentSystem.OnUpdate();
        _itemsController.OnUpdate();
    }

    private void FixedUpdate() {
        
    }

    private void OnDisable() {
        _itemsController.OnDisable();
    }
}