using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public GameObject player;

    int pushForce = 15;
    private void Start()
    {
        GameObject foundObject = GameObject.Find("Player");
        player = foundObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") ) {
            GameObject Player = GameObject.Find("Player");
            other.gameObject.GetComponent<Enemy>().enemyHP -= player.GetComponent<PlayerStats>().PlayerCurrerenDamage;
            Debug.Log("sword");

            Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();

            if (enemyRigidbody != null)
            {
                Vector3 pushDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Enemy>().enabled = false;
                other.GetComponent<BoxCollider>().enabled = false;
                enemyRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);

                StartCoroutine(reactivateEnemy(other));

            }
        }
    }

    IEnumerator reactivateEnemy(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        other.GetComponent<Enemy>().enabled = true;
        other.GetComponent<BoxCollider>().enabled = true;
    }

}
