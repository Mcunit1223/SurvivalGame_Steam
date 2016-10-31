using UnityEngine;
using System.Collections;

public class CallAttack : MonoBehaviour {

    MoveShark ms;
    Rigidbody rb;

    void Start()
    {
        ms = GetComponentInParent<MoveShark>();
        rb = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, -90, 0);
        transform.localPosition = new Vector3(0, 0, 0);
    }

    void callAttack()
    {
        ms.attemptAttack();
    }

    void changeMoveState()
    {
        ms.changeMoveState();
    }

    public void OnOceanEnter()
    {
        ms.onSwitchTerrain(false);
    }

    public void OnOceanExit()
    {
        ms.onSwitchTerrain(true);
    }
}
