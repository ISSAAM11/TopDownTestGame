using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject swordCollider;
    private Animator animator;

    Vector3 mouvement;
    public Rigidbody rb;
    private float playerInitialSpeed = 5;
    public float playerCurrentSpeed;
    public Text playerCurrentSpeedText;

    void Start()
    {
        playerCurrentSpeed = playerInitialSpeed;
        animator = GetComponent<Animator>();
        playerCurrentSpeedText.text = "Speed: " + (playerCurrentSpeed * 100 / 5).ToString() + "%";

    }

    public void updateStats(List<ItemsClass> myItems)
    {
        float totalItemsSpeed = myItems.Sum(item => item.speed)/100;
        playerCurrentSpeed = playerInitialSpeed + totalItemsSpeed;
        playerCurrentSpeedText.text = "Speed: " + (playerCurrentSpeed * 100 / 5).ToString() + "%";

    }

    void Update()
    {
        Attack();        
    }

    private void FixedUpdate()
    {
        if (!playerOnHit)
        {
            Move();
            Aim();
        }
    }
    void Move()
    {
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.z = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + mouvement.normalized * playerCurrentSpeed * Time.fixedDeltaTime);
        UpdateAnimator();
    }

    void Aim()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Set a distance from the camera to create a point in the world space
        mousePosition.z = 10f;

        // Convert the mouse position to world space
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction to the target position
        Vector3 aimDirection = (targetPosition - transform.position).normalized;

        // Rotate the player to face the mouse direction (only the y-axis)
        transform.forward = new Vector3(aimDirection.x, 0f, aimDirection.z);
    }

    public GameObject Bulette;
    public Transform firePosition;

     
    void DisableSwordCollider()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("IsWalking", true);
        attacking = false;

        playerWeapons.swordHandler.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
      
    private float nextFireAssaultRifle;

    bool ShotNow;

    bool attacking;


    public PlayerWeapons playerWeapons;
    void Attack()
    {
        if (playerWeapons.currerentWeapon == "Gun" && Input.GetMouseButtonDown(0) && playerWeapons.currerentWeapons[1].bulletCount > 0)
        {
            playerWeapons.currerentWeapons[1].bulletCount--;
            GameObject bulette = Instantiate(Bulette, firePosition.transform.position, firePosition.transform.rotation);
            bulette.GetComponent<bulletScript>().player = this.gameObject;
            playerWeapons.GunCount();
        }
        else if(playerWeapons.currerentWeapon == "Assult Rifle" && Input.GetMouseButton(0) && playerWeapons.currerentWeapons[2].bulletCount > 0) 
        {
             if (Time.time >= nextFireAssaultRifle)
             {
                playerWeapons.currerentWeapons[2].bulletCount--;
                GameObject bulette = Instantiate(Bulette, firePosition.transform.position, firePosition.transform.rotation);
                bulette.GetComponent<bulletScript>().player = this.gameObject;
                nextFireAssaultRifle = Time.time + 1f / playerWeapons.currerentWeapons[2].fireRate;
                playerWeapons.AssultRifleCont();
             }
        }
        else if (playerWeapons.currerentWeapon == "Sword" && Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true );
            animator.SetBool("IsWalking", false);

            playerWeapons.swordHandler.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
            attacking = true;
            Invoke("DisableSwordCollider", 0.2f);
        }
    }

    void UpdateAnimator()
    {
        if (!attacking) { 
            float moveMagnitude = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).sqrMagnitude;
            animator.SetBool("IsWalking", moveMagnitude > 0f);
        }
    }


    public bool playerOnHit = false;
    public int pushForce = 3000;

    public PlayerStats playerStats;






}
