using UnityEngine;

[CreateAssetMenu(fileName = "NewItemSO", menuName = "ScriptableObjects/CreateNewItem")]
public class ItemSO : ScriptableObject
{
    [Header("Name")]
    public string Name;

    [Header("Image")]
    public Sprite Sprite;
}