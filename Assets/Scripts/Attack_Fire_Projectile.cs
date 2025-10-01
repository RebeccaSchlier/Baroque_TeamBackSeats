using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script in reference to this video: https://www.youtube.com/watch?v=EwiUomzehKU

public class Attack_Fire_Projectile : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10;

    public InputActionReference attack;

    private bool isFiring;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isFiring == false) // Left Mouse Button
        {
            StartCoroutine(ExecuteRangedAttack());
            isFiring = true;

        }
    }

    private IEnumerator ExecuteRangedAttack()
    {
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectile.GetComponent<Rigidbody>().linearVelocity = projectileSpawnPoint.forward * projectileSpeed;
        yield return new WaitForSeconds(1);
        isFiring = false;
    }
}
