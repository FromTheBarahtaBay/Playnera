using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ItemsController
{
    private Bootstrap _bootstrap;
    private List<ItemComponentsConteiner> _items = new();
    private List<ItemComponentsConteiner> _itemsToSet = new();
    private bool CheckListOfItemsToSet = true;

    public ItemsController(Bootstrap bootstrap, ItemSO[] items) {
        _bootstrap = bootstrap;
        CreateItems(items);
        EvetsSystem.IsItemDropped += AddItemForSetPlace;
    }

    private void CreateItems(ItemSO[] items) {
        foreach (var item in items) {
            new CreateItem(item, out ItemComponentsConteiner element);
            if (element == null) {
                Debug.LogError("Failed to create element.");
            }
            _items.Add(element);
            _itemsToSet.Add(element);
            AddItemForSetPlace(element);
            SetPosition(element);
        }
    }

    private void AddItemForSetPlace(ItemComponentsConteiner item) {
        //_bootstrap.StartCorutines(SetPlace(item));
        SetPlace(item);
    }

    private void SetPosition(ItemComponentsConteiner item) {
        Vector3 minScreenBounds = _bootstrap.Camera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = _bootstrap.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float randomX = Random.Range(minScreenBounds.x, maxScreenBounds.x);
        float randomY = Random.Range(minScreenBounds.y, maxScreenBounds.y);

        item.transform.position = new Vector3(randomX, randomY, 0);
    }

    private void SetPlace(ItemComponentsConteiner item) {
        _itemsToSet.Add(item);
        CheckListOfItemsToSet = true;
    }

    public void OnUpdate() {
        if (!CheckListOfItemsToSet) return;

        for (int i = 0; i < _itemsToSet.Count; i++) {
            var e = _itemsToSet[i];
            Vector3 worldPoint = e.transform.position;
            Collider2D[] hits = Physics2D.OverlapPointAll(worldPoint);

            if (hits.Length > 0) {
                for (int j = 0; j < hits.Length; j++) {
                    if (hits[j].CompareTag("Plane")) {
                        e.OnDrag();
                        _itemsToSet.RemoveAt(i);
                        i--;
                        if (_itemsToSet.Count <= 0)
                            CheckListOfItemsToSet = false;
                        break;
                    }
                }
            }
        }
    }

    public void OnDisable() {
        EvetsSystem.IsItemDropped -= AddItemForSetPlace;
    }
}