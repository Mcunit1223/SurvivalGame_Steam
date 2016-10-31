using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CraftingMenu : MonoBehaviour {
    // Inventory
    Inventory inventory;
    ItemDatabase itemDatabase;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        itemDatabase = GetComponent<ItemDatabase>();
    }

    void Update()
    {
        
    }

    public void onCraft(Button button)
    {
        Item item = itemDatabase.FindItemByID(itemDatabase.FindIDByTitle(button.name));
        if(inventory.GetCount("Wood") >= item.WoodReq && inventory.GetCount("Stone") >= item.StoneReq)
        {
            inventory.removeItem("Wood", item.WoodReq);
            inventory.removeItem("Stone", item.StoneReq);
            inventory.addItem(item.ID, 1);
        }
    }
}
