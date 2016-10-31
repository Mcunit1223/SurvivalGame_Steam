using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class PickUpItem : MonoBehaviour {
    Inventory inventory;
    GameObject inventoryObject;
    RaycastHit hitInfo;
    Grabbable item;
    bool canPickUp = false;
    public float rayDistance;
    public AudioClip pickUpClip;

    void Start()
    {
        inventoryObject = GameObject.Find("Inventory");
        inventory = inventoryObject.GetComponent<Inventory>();
    }

    void Update()
    {
        if (canPickUp)
        {
            int id = inventoryObject.GetComponent<ItemDatabase>().FindIDByItem(getCorrectName(hitInfo.collider.name));
            Item item = inventoryObject.GetComponent<ItemDatabase>().FindItemByID(id);
            inventory.addItem(id, item.Amount);
            Destroy(hitInfo.collider.gameObject);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = pickUpClip;
            audioSource.Play();
            canPickUp = false;
        }
    }

    void OnGUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, rayDistance) && isGrabbable(hitInfo.collider.gameObject))
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 50), "Press E to pick up " + hitInfo.collider.name);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hitInfo.collider.transform.parent && getCorrectName(hitInfo.collider.transform.parent.name) == "BerryBush")
                {
                    pickBerries();
                }
                canPickUp = true;
            }
        }
        else
        {
            canPickUp = false;
        }
    }

    bool isGrabbable(GameObject obj)
    {
        return inventoryObject.GetComponent<ItemDatabase>().FindIDByItem(getCorrectName(obj.name)) != -1;
    }

    public string getCorrectName(string name)
    {

        string s = name;
        s = s.Replace("(", "");
        s = s.Replace(")", "");
        s = s.Replace(" ", "");
        s = s.Replace("Clone", "");
        s = Regex.Replace(s, @"\d+", "");
        return s;
    }

    void pickBerries()
    {
        hitInfo.collider.transform.parent.GetComponent<BerryBush>().berriesGone = true;
    }
}
