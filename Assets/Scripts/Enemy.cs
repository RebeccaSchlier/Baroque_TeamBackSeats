using UnityEngine;

// This script is for an enemy that will follow a player by searching for a 'Player' tag - Gatsby

// Script was taken from this video: https://www.youtube.com/watch?v=pQajI2xHe5U   credits go to Gatsby on YouTube.

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;            // Movement speed of the enemy
    private Transform target;               // Target to follow (player's transform when detected)
    private MeshRenderer _meshRenderer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        // If a target is set (player is within range), follow the target
        if (target != null)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        // Calculate direction towards the target
        Vector3 direction = (target.position - transform.position).normalized;

        // Move the enemy towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            target = other.transform; // Set the player's transform as the target
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the detection range, clear the target
        if (other.CompareTag("Player"))
        {
            target = null; // Stop following the player
        }
    }

    private void OnDrawGizmosSelected()
    {
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();
        if (capsule != null)
        {
            // Visualize the detection range in the editor using the collider's radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + capsule.center, capsule.height / 2f);
        }
        
    }
}
