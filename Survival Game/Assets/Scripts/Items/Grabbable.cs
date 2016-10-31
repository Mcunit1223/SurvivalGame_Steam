using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Grabbable : MonoBehaviour {
    public int amount;
    public Item item;
    Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        string slug = Regex.Replace(name, @"[\d-]", string.Empty);

    }
}
