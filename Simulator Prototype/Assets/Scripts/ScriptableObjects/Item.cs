
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemType = "Hat";
    public int buy = 5;
    public int sell = 2;
    public Sprite image;
    public GameObject prefab;
}
