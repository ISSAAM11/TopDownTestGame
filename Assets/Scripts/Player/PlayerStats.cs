using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int currerentMaxHP;     // with items ability
    private int initialMaxHP = 100; // without items ability
    public int playerHP;           // actual HP

    public float PlayerCurrerenDamage ;
    private float PlayerInitialDamage = 50;

    public float CoinsAmount = 0;
    public Text HPText;
    public Text PlayerDamageText;
    public Text CoinsAmountText;

    public CameraPosition cameraPosition;
    public PlayerWeapons playerWeapons;
    public MyItems myItems;

    public GameObject PressButtonShop;
    bool ShopCanBeActive = false;
    public GameObject shopPanel;


    public int PlayerScore;
    public Text PlayerScoreTxt;
    private void Start()
    {
        PlayerCurrerenDamage = PlayerInitialDamage;
        playerHP = initialMaxHP;
        currerentMaxHP = initialMaxHP;
    }
    void Update()
    {
        PlayerScoreTxt.text = "Score: " + PlayerScore.ToString();
        HPText.text = "HP: " + playerHP.ToString() +  " / " + currerentMaxHP.ToString();

        PlayerDamageText.text = "Damage: " + PlayerCurrerenDamage;
        CoinsAmountText.text = "Coins: " + CoinsAmount;
        updateStats();
        
        if (Input.GetKeyDown(KeyCode.P) && ShopCanBeActive && !shopPanel.activeSelf)
        {
            Time.timeScale = 0f;

            shopPanel.SetActive(true);
            PressButtonShop.SetActive(false);
            cameraPosition.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.P) && shopPanel.activeSelf)
        {
            Time.timeScale = 1f;
            shopPanel.SetActive(false);
            PressButtonShop.SetActive(true);
            cameraPosition.enabled = true;
        }
    }

    public void updateStats()
    {
        int totalItemHP = myItems.myItems.Sum(item => item.Health);
        int totalItemDamage = myItems.myItems.Sum(item => item.Damage);

        PlayerCurrerenDamage = PlayerInitialDamage + totalItemDamage + playerWeapons.currerentWeapons[playerWeapons.GetCurrerentWeapon()].damage;
        currerentMaxHP = initialMaxHP + totalItemHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CoinsAmount += other.GetComponent<Coins>().CoinsValue;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("heart"))
        {
            playerHP += 50;
            if (playerHP > currerentMaxHP)
            {
                playerHP = currerentMaxHP;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Shop"))
        {
            PressButtonShop.SetActive(true);
            ShopCanBeActive = true;
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Shop"))
        {
            PressButtonShop.SetActive(false);
            ShopCanBeActive = false;
            shopPanel.SetActive(false);
        }
    }
}
