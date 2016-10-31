using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject hotBar;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    private GameObject crosshair;

    public int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public int weight;
    public int maxWeight = 1000;
    public Text weightText;
    public FirstPersonController playerController;
    bool isOverWeight;
    public bool paused;

    public GameObject equippedItem;
    int equippedSlot = -1;

    GameObject starterAxe;
    GameObject starterPick;
    GameObject woodSpear;
    GameObject arms;

    void Start()
    {
        starterAxe = GameObject.Find("Starter_Axe");
        starterPick = GameObject.Find("Starter_Pickaxe");
        woodSpear = GameObject.Find("Wood_Spear");
        arms = GameObject.Find("Arms");

        starterAxe.SetActive(false);
        starterPick.SetActive(false);
        woodSpear.SetActive(false);
        arms.SetActive(false);

        crosshair = GameObject.Find("Crosshair");
        weightText = GameObject.Find("Weight Text").GetComponent<Text>();
        playerController = GameObject.Find("Player").GetComponent<FirstPersonController>();
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        hotBar = GameObject.Find("Hotbar");
        database = GetComponent<ItemDatabase>();
        unpause();
        for (int i = 0; i < slotAmount; i++)
        {
            if (i < 8)
            {
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlot));
                slots[i].transform.SetParent(hotBar.transform);
                slots[i].GetComponent<Slot>().id = i;
            }
            else
            {
                items.Add(new Item());
                slots.Add(Instantiate(inventorySlot));
                slots[i].transform.SetParent(slotPanel.transform);
                slots[i].GetComponent<Slot>().id = i;
            }
        }
        addItem(0, 1);
        addItem(1, 1);
        addItem(2, 1);
        addItem(3, 10);
        addItem(5, 150);
    }

    void Update()
    {
        if(equippedSlot == -1)
        {
            changeEquippedSlot(1);
        }
        weightText.text = "Weight: " + weight + " lbs";
        if(!isOverWeight && weight > maxWeight)
        {
            playerController.m_WalkSpeed /= 3;
            playerController.m_RunSpeed /= 3;
            isOverWeight = true;
        }
        else if(isOverWeight && weight <= maxWeight)
        {
            playerController.m_WalkSpeed /= 3;
            playerController.m_RunSpeed /= 3;
            isOverWeight = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (paused)
            {
                unpause();
            }
            else
            {
                pause();
            }
        }

        float step;
        if(!paused && (step = Input.GetAxis("Mouse ScrollWheel")) != 0)
        {
            if(step < 0)
            {
                changeEquippedSlot(-1);
            }
            else
            {
                changeEquippedSlot(1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            useItem();
        }
    }

    public void addItem(int id, int amount)
    {
        Item itemToAdd = database.FindItemByID(id);
        if (itemToAdd.Stackable && exists(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount += amount;
                    weight += itemToAdd.Weight;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    weight += itemToAdd.Weight;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().amount += amount;
                    itemObj.GetComponentInChildren<Text>().text = itemObj.GetComponent<ItemData>().amount.ToString();
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.transform.localPosition = Vector2.zero;
                    itemObj.name = itemToAdd.Title;
                    return;
                }
            }
        }
    }

    public void removeItem(ItemData itemData, int amount, bool destroyObject)
    {
        if((itemData.amount - amount) <= 0)
        {
            if (destroyObject)
            {
                Destroy(itemData.gameObject);
            }
            items[itemData.slot] = new Item();
        }
        else
        {
            itemData.amount -= amount;
            itemData.gameObject.GetComponentInChildren<Text>().text = itemData.amount.ToString();
        }
    }

    public bool removeItem(string itemName, int amount)
    {
        Item item = database.FindItemByID(database.FindIDByTitle(itemName));
        if(GetCount(itemName) < amount)
        {
            return false;
        }
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].Title == itemName)
            {
                ItemData itemData = slots[i].GetComponentInChildren<ItemData>();
                int amountInSlot = itemData.amount;
                if(amountInSlot > amount)
                {
                    itemData.amount -= amount;
                    itemData.gameObject.GetComponentInChildren<Text>().text = (itemData.amount).ToString();
                    return true;
                }
                else if(amountInSlot == amount)
                {
                    items[i] = new Item();
                    Destroy(itemData.gameObject);
                    return true;
                }
                else
                {
                    items[i] = new Item();
                    Destroy(itemData.gameObject);
                    amount -= itemData.amount;
                }
            }
        }
        return false;
    }

    public int GetCount(string title)
    {
        int count = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].Title == title)
            {
                count += slots[i].GetComponentInChildren<ItemData>().amount;
            }
        }
        return count;
    }

    bool exists(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        inventoryPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
    }

    public void unpause()
    {
        paused = false;
        Time.timeScale = 1;
        inventoryPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.SetActive(true);
    }

    void changeEquippedSlot(int step)
    {
        fixequippedSlot();
        if (equippedItem)
            equippedItem.SetActive(false);
        slots[equippedSlot].GetComponent<Image>().color = new Color(255, 255, 255, 0.39f);
        equippedSlot -= step;
        fixequippedSlot();
        slots[equippedSlot].GetComponent<Image>().color = new Color(0, 255, 0);
        if (items[equippedSlot].IsTool)
        {
            equipTool();
            arms.SetActive(false);
        }
        else
        {
            arms.SetActive(true);
        }

    }

    void fixequippedSlot()
    {
        if (equippedSlot < 0)
        {
            equippedSlot = 0;
        }
        if (equippedSlot > 7)
        {
            equippedSlot = 7;
        }
    }

    void equipTool()
    {
        switch (items[equippedSlot].Slug)
        {
            case "Starter_Axe":
                equippedItem = starterAxe;
                starterAxe.SetActive(true);
                break;
            case "Starter_Pickaxe":
                equippedItem = starterPick;
                starterPick.SetActive(true);
                break;
            case "Wood_Spear":
                equippedItem = woodSpear;
                woodSpear.SetActive(true);
                break;
        }
    }

    void useItem()
    {
        if (items[equippedSlot].Usable)
        {
            StatManagement sm = GameObject.Find("Player").GetComponent<StatManagement>();
            Item item = items[equippedSlot];
            sm.health += item.Health;
            sm.hunger += item.Hunger;
            sm.thirst += item.Thirst;
            sm.stamina += item.Stamina;
            ItemData itemData = slots[equippedSlot].GetComponentInChildren<ItemData>();
            itemData.amount--;
            itemData.gameObject.GetComponentInChildren<Text>().text = itemData.amount.ToString();
            if (itemData.amount <= 0)
            {
                items[equippedSlot] = new Item();
                Destroy(itemData.gameObject);
            }
        }
    }
}
