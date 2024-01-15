using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speedEnemy;
    public int Damage ;
    public float enemyHP;
    public float masEnemyHP;

    public Transform playerTransform;
    private Rigidbody rb;
    public WeaponsDB weaponsDB;
    public float pushBackForce = 5000f;

    public int score ;

    public Image healthBar;
    public GameObject player;
    void Start()
    {
        masEnemyHP = enemyHP;
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar)
        {
            healthBar.fillAmount = enemyHP / masEnemyHP;
        }
        Vector3 targetPosition = player.transform.position;
        Vector3 rotationmDirection = (targetPosition - transform.position).normalized;
        transform.forward = new Vector3(rotationmDirection.x, 0f, rotationmDirection.z);

        if(enemyHP <= 0)
        {
            spownMoney();
            Destroy(gameObject);
        }
    }

    public GameObject goldMoney;
    public GameObject silverMoney;
    public GameObject heart;


    public int minChanseTogetreward;
    public int maxChanseTogetreward;

    public int minChanse;
    public int maxChanse;

    public int minGunChanse;
    public int maxGunChanse;

    void spownMoney()
    {
        player.GetComponent<PlayerStats>().PlayerScore += score;

        if (Random.Range(minChanseTogetreward, maxChanseTogetreward) == 1) 
        {

            int randomNumber = Random.Range(minChanse, maxChanse);
            if (randomNumber == 1)
                Instantiate(goldMoney,   new Vector3(transform.position.x, transform.position.y +0.2f, transform.position.z), Quaternion.identity);
            else if (randomNumber == 2) {
                GameObject weaponObj = weaponsDB.getWeapon(Random.Range(minGunChanse, maxGunChanse)).weaponItemPrefab;
                Instantiate(weaponObj, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);
            }else if (randomNumber == 3)
            {
                Instantiate(heart, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity);         
            }
            else
                Instantiate(silverMoney, new Vector3(transform.position.x, transform.position.y +0.2f, transform.position.z), Quaternion.identity);
        }

    }

    private void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        rb.velocity = direction * speedEnemy;
    }
}
