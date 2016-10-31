using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
    private List<Item> database = new List<Item>();
    private JsonData itemData;

    void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();
    }
    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["ID"], itemData[i]["Title"].ToString(), itemData[i]["Slug"].ToString(), (int)itemData[i]["Damage"], (int) itemData[i]["Weight"], (int) itemData[i]["Amount"], (bool) itemData[i]["Usable"], (bool) itemData[i]["Tool"], (bool) itemData[i]["Stackable"], (float) (((int) itemData[i]["stats"]["Health"])) / 100, (float) (((int) itemData[i]["stats"]["Hunger"])) / 100, (float) (((int) itemData[i]["stats"]["Thirst"])) / 100, (float)(((int) itemData[i]["stats"]["Stamina"])) / 100, (int) itemData[i]["CraftRequirements"]["Wood"], (int)itemData[i]["CraftRequirements"]["Stone"], itemData[i]["Description"].ToString()));
        }
    }

    public Item FindItemByID(int id)
    {
        for(int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    public int FindIDByItem(string name)
    {
        for(int i = 0; i < database.Count; i++)
        {
            if(database[i].Slug == name)
            {
                return database[i].ID;
            }
        }
        return -1;
    }

    public int FindIDByTitle(string name)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].Title == name)
            {
                return database[i].ID;
            }
        }
        return -1;
    }
}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public int Damage { get; set; }
    public int Weight { get; set; }
    public int Amount { get; set; }
    public bool Usable { get; set; }
    public bool IsTool { get; set; }
    public bool Stackable { get; set; }
    public float Health { get; set; }
    public float Hunger { get; set; }
    public float Thirst { get; set; }
    public float Stamina { get; set; }
    public int WoodReq { get; set; }
    public int StoneReq { get; set; }
    public string Description { get; set; }
    public Sprite Sprite;
    public GameObject Object;

    public Item(int id, string title, string slug, int damage, int weight, int amount, bool usable, bool isTool, bool stackable, float health, float hunger, float thirst, float stamina, int wood, int stone, string description)
    {
        ID = id;
        Title = title;
        Slug = slug;
        Damage = damage;
        Weight = weight;
        Amount = amount;
        Usable = usable;
        IsTool = isTool;
        Stackable = stackable;
        Health = health;
        Hunger = hunger;
        Thirst = thirst;
        Stamina = stamina;
        WoodReq = wood;
        StoneReq = stone;
        Description = description;
        Sprite = Resources.Load<Sprite>("Sprites/" + slug);
        Object = Resources.Load<GameObject>("Objects/" + slug);
    }

    public Item()
    {
        ID = -1;
    }
}
