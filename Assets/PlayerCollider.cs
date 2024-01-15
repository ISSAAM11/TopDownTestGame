using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerControl playerControl;


    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {

            Rigidbody playerRigidbody = gameObject.GetComponentInParent<Rigidbody>();

            if (playerRigidbody != null)
            {
                Debug.Log("collision enemy");

                playerStats.playerHP -= other.gameObject.GetComponent<Enemy>().Damage;
                playerControl.playerOnHit = true;
                Vector3 pushDirection = (transform.position - other.transform.position).normalized;
                playerRigidbody.AddForce(pushDirection * playerControl.pushForce, ForceMode.Impulse);
                StartCoroutine(reactivatePlayer());
            }
        }

    }

    IEnumerator reactivatePlayer()
    {
        yield return new WaitForSeconds(0.2f);
        playerControl.playerOnHit = false;
    }
}
