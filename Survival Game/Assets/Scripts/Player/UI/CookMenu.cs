using UnityEngine;
using System.Collections;

public class CookMenu : MonoBehaviour {
    Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {

    }

    void addFuel()
    {

    }

    void addMeat()
    {
        
    }
}
