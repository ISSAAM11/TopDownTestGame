using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{

    public GameObject pressToGatherWeapon;
    public WeaponsClass weaponFounded = null;
    public WeaponsDB weaponsDB;

    GameObject thisWeaponsToDestroy;
    

    public WeaponsClass[] currerentWeapons = new WeaponsClass[3];


    private void Start()
    {
        currerentWeapons[0] = weaponsDB.getWeapon(1);
    }

    public GameObject swordHandler;
    public GameObject gunHandler;
    public GameObject assultRifleHandler;
    public string currerentWeapon;

    public Image imageSword;
    public Image imageGun;
    public Image imageAssultRifle;
    public Text CountGunText;
    public Text CountAssultRifleText;


    public int GetCurrerentWeapon()
    {
        if (currerentWeapon == "Sword")
        {
            return 0;
        }
        else if (currerentWeapon == "Gun")
        {
            return 1;
        }
        else if (currerentWeapon == "Assult Rifle")
        {
            return 2;
        }
        return 3;
    }

    public void SetoutLineActive(GameObject thisGameObject)
    {
        imageSword.gameObject.GetComponent<Outline>().enabled = false;
        imageGun.gameObject.GetComponent<Outline>().enabled = false;
        imageAssultRifle.gameObject.GetComponent<Outline>().enabled = false;
        thisGameObject.GetComponent<Outline>().enabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSwordActive();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currerentWeapons[1].id != 0)
        {
            currerentWeapon = "Gun";
            swordHandler.SetActive(false);
            gunHandler.SetActive(true);
            assultRifleHandler.SetActive(false);
            SetoutLineActive(imageGun.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currerentWeapons[2].id != 0)
        {
            currerentWeapon = "Assult Rifle";
            swordHandler.SetActive(false);
            gunHandler.SetActive(false);
            assultRifleHandler.SetActive(true);
            SetoutLineActive(imageAssultRifle.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.E) && weaponFounded != null)
        {
            if (weaponFounded.weaponType == "Sword")
            {
                Instantiate(currerentWeapons[0].weaponItemPrefab, thisWeaponsToDestroy.transform.position, thisWeaponsToDestroy.transform.rotation);
                Destroy(swordHandler.transform.GetChild(0).gameObject);

                currerentWeapons[0] = weaponFounded;
                GameObject weapon = Instantiate( weaponFounded.bulletPrefab.gameObject, swordHandler.transform );
                imageSword.sprite = currerentWeapons[0].sprite;
            }
            if (weaponFounded.weaponType == "Gun") {

                if (gunHandler.transform.childCount > 0)
                {
                    GameObject itemWeapon = Instantiate(currerentWeapons[1].weaponItemPrefab, thisWeaponsToDestroy.transform.position, thisWeaponsToDestroy.transform.rotation);
                    itemWeapon.transform.GetChild(0).GetComponent<IdWeapon>().BulletCount = currerentWeapons[1].bulletCount;
                    Destroy(gunHandler.transform.GetChild(0).gameObject);
                }

                currerentWeapons[1] = weaponFounded;
                GameObject weapon =  Instantiate(weaponFounded.bulletPrefab.gameObject, gunHandler.transform);
                imageGun.sprite = currerentWeapons[1].sprite;
                CountGunText.text = currerentWeapons[1].bulletCount.ToString();
            }
            if (weaponFounded.weaponType == "Assult Rifle") {

                if (assultRifleHandler.transform.childCount > 0)
                {
                    GameObject itemWeapon = Instantiate(currerentWeapons[2].weaponItemPrefab, thisWeaponsToDestroy.transform.position, thisWeaponsToDestroy.transform.rotation);
                    itemWeapon.transform.GetChild(0).GetComponent<IdWeapon>().BulletCount = currerentWeapons[2].bulletCount;
                    Destroy(assultRifleHandler.transform.GetChild(0).gameObject);
                }

                currerentWeapons[2] = weaponFounded;
                GameObject weapon =  Instantiate(weaponFounded.bulletPrefab.gameObject, assultRifleHandler.transform);
                imageAssultRifle.sprite = currerentWeapons[2].sprite;
                CountAssultRifleText.text = currerentWeapons[2].bulletCount.ToString();
            }
            Destroy(thisWeaponsToDestroy);

            weaponFounded = null;
            thisWeaponsToDestroy = null;
            pressToGatherWeapon.SetActive(false);
        }
        
       
    }

    public void GunCount()
    {
        CountGunText.text = currerentWeapons[1].bulletCount.ToString();

        if (currerentWeapons[1].bulletCount < 1 && gunHandler.transform.GetChild(0).gameObject) 
        {
            Debug.LogWarning(currerentWeapons[1].bulletCount);
            currerentWeapons[1].id = 0;
            //currerentWeapons[1]= null;
            Destroy(gunHandler.transform.GetChild(0).gameObject);
            imageGun.sprite = null;
            SetSwordActive();
        }

    }
    public void AssultRifleCont()
    {
        CountAssultRifleText.text = currerentWeapons[2].bulletCount.ToString();
        if (currerentWeapons[2].bulletCount < 1 && assultRifleHandler.transform.GetChild(0).gameObject)
        {
            currerentWeapons[2].id = 0;
            //currerentWeapons[2] = null;
            Destroy(assultRifleHandler.transform.GetChild(0).gameObject);
            imageAssultRifle.sprite = null;
            SetSwordActive();
        }

    }
    /*
    public void UpdateCountBullets()
    {
        CountGunText.text = currerentWeapons[1].bulletCount.ToString();
        CountAssultRifleText.text = currerentWeapons[2].bulletCount.ToString();
    }
    */
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WeaponItem"))
        {
            weaponFounded = null;
            thisWeaponsToDestroy = null;
            pressToGatherWeapon.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponItem"))
        {
            weaponFounded = new WeaponsClass(weaponsDB.getWeapon(other.GetComponent<IdWeapon>().id));
            thisWeaponsToDestroy = other.gameObject;
            weaponFounded.bulletCount = thisWeaponsToDestroy.GetComponent<IdWeapon>().BulletCount;
            pressToGatherWeapon.SetActive(true);
        }
    }


    void SetSwordActive()
    {
        currerentWeapon = "Sword";
        swordHandler.SetActive(true);
        gunHandler.SetActive(false);
        assultRifleHandler.SetActive(false);
        SetoutLineActive(imageSword.gameObject);
    }
}
