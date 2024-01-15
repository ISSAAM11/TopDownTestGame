using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{

    public float bulletSpeed = 1f;
    public GameObject player;
    void Start()
    {
        Destroy(gameObject, 5);
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy") { 
            other.gameObject.GetComponent<Enemy>().enemyHP -= player.GetComponent<PlayerStats>().PlayerCurrerenDamage;
        }
        Destroy(gameObject);
    }
}
