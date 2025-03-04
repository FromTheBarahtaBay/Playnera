using UnityEngine;

public class CreateItem
{
    public CreateItem(ItemSO itemData, out ItemComponentsConteiner itemConteiner) {
        itemConteiner = Create(itemData);
    }

    private ItemComponentsConteiner Create(ItemSO itemData) {
        var newItem = new GameObject($"{itemData.Name}") {
            tag = "Movable"
        };
        var spriteRenderer = newItem.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.Sprite;
        spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
        var boxCollider2D = newItem.AddComponent<BoxCollider2D>();
        //boxCollider2D.isTrigger = true;
        var rigitbody2D = newItem.AddComponent<Rigidbody2D>();
        rigitbody2D.freezeRotation = true;
        rigitbody2D.drag = 2f;
        var itemConteiner = newItem.AddComponent<ItemComponentsConteiner>();

        itemConteiner.SpriteRenderer = spriteRenderer;
        itemConteiner.BoxCollider2D = boxCollider2D;
        itemConteiner.Rigidbody2D = rigitbody2D;

        return itemConteiner;
    }
}