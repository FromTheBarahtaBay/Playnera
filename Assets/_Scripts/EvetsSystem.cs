using System;

public class EvetsSystem
{
    public static event Action<ItemComponentsConteiner> IsItemDropped;
    public static void OnIsItemDropped(ItemComponentsConteiner item) { IsItemDropped?.Invoke(item); }
}