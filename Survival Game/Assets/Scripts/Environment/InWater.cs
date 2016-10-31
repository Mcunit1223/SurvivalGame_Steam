using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class InWater : MonoBehaviour {
    public float antiGravForce;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.name == "MutatedSharkPrefab")
        {
            collider.GetComponentInChildren<CallAttack>().OnOceanEnter();
        }
        else if(collider.name == "Player")
        {
            playerInWater(collider);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.name == "MutatedSharkPrefab")
        {
            collider.GetComponentInChildren<CallAttack>().OnOceanExit();
        }
        else if (collider.name == "Player")
        {
            playerOutOfWater(collider);
        }
    }

    void playerInWater(Collider player)
    {
        Debug.Log("Player in Water");
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();
        playerController.m_isSwimming = true;
        playerController.m_GravityMultiplier = 1/antiGravForce;
        playerController.m_MoveDir.y = 0;
        playerController.m_MoveDir /= antiGravForce;
    }

    void playerOutOfWater(Collider player)
    {
        Debug.Log("Player out of Water");
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();
        playerController.m_isSwimming = false;
        playerController.m_GravityMultiplier = 2;
    }
}
