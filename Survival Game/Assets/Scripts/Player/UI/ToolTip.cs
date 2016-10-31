using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;
    public int amount;

    void Start()
    {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
        tooltip.transform.position = Input.mousePosition;
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }
	
    public void ConstructDataString()
    {
        data = "<b><color=#00FFFF>" + item.Title + "</color></b>";
        data = data + "\n\n" + item.Description + "\n";
        data = data + "\nID: " + item.ID;
        data = data + "\nDamage: " + item.Damage;
        data = data + "\nWeight: " + item.Weight;
        if (item.Stackable)
        {
            data = data + "\nStack Weight: " + item.Weight * amount;
        }
        if (item.Usable)
        {
            data = data + "\nStats: ";
            data = data + "\n\tHealth: " + item.Health * 100 + "%";
            data = data + "\n\tHunger: " + item.Hunger * 100 + "%";
            data = data + "\n\tThirst: " + item.Thirst * 100 + "%";
            data = data + "\n\tStamina: " + item.Stamina * 100 + "%";
        }
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
