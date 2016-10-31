using UnityEngine;
using System.Collections;

public class MoveShark : MonoBehaviour
{
    public float swimSpeed;
    public float walkSpeed;
    Animator anim;
    bool isMoving;
    bool trackingPlayer;
    public Transform[] destinations;
    Transform destination;
    public GameObject player;
    public float trackDistance;
    public float hitDistance;
    public float damage = 0.3f;
    public float rotationSpeed = 3;

    float attackTimer;
    public float attackWaitTime = 1;
    bool isAttacking = true;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("onLand", true);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("distanceFromPlayer", distance);
        if (distance <= trackDistance)
        {
            trackPlayer();
            trackDistance *= 1.3f;
        }
        else
        {
            if (trackingPlayer)
            {
                trackDistance /= 1.3f;
                trackingPlayer = false;
            }
            RandomMovement();
        }

    }

    void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0);
    }

    public void attemptAttack()
    {
        // Get enemy and player positions
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;

        // Get Vector between them and enemy forward vector
        Vector3 vectorBetween = playerPos - enemyPos;
        Vector3 forwardVector = -transform.forward;
        float angle = Vector3.Angle(vectorBetween, forwardVector);

        // Get distance between enemy and player
        float distance = Vector3.Distance(playerPos, enemyPos);

        /* if the angle is less than 180 degrees (Pi) and the player
         * is within the attack distance, do damage 
         */
        if (angle > 100 && distance <= hitDistance)
        {
            player.GetComponent<StatManagement>().health -= damage;
        }
    }

    public void changeMoveState()
    {
        isAttacking = !isAttacking;
    }

    void trackPlayer()
    {
        //rotate to look at the player
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(player.transform.position - transform.position), rotationSpeed * Time.deltaTime);
        if (isAttacking)
        {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
    }

    void RandomMovement()
    {
        // Pick a destination if destination picked is false
        if (!isMoving && !trackingPlayer)
        {
            destination = destinations[Random.Range(0, destinations.Length)];
            isMoving = true;
        }

        // Move Towards that destination
        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(destination.transform.position - transform.position), rotationSpeed * Time.deltaTime);
        if (isAttacking)
        {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }

        // if arrived at destination, set isMoving to false
        if (Vector3.Distance(transform.position, destination.position) <= 3)
        {
            isMoving = false;
        }
    }

    public void onSwitchTerrain(bool onLand)
    {
        anim.SetBool("onLand", onLand);
        if (onLand)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
