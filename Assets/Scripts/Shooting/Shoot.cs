using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    //oyuncunun sadece eli hareket edecek b�t�n hareketler oldu�u gibi olacak fakat eli farkl� olacak.
    //oyunc arkas� d�n�kken geriye do�ru ko�amayacak.
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 1f)]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private Animator anim;
    [SerializeField] private bool shootBool;

    [SerializeField] Text gunText;

    [Header("Reload")]
    private int currentAmmo = 7; // Ba�lang��ta 7 mermi
    public bool isReloading = false;
    public float reloadTime = 2f; // Reload s�resi

    [Header("Shooting Rotation")]
    [SerializeField] private float speed = 5f;

    private bool canPlay;
    PlayerController controller;

    //new
    /*public Vector3 target;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject crosshair;
    [SerializeField] GameObject gun;*/

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {

        /* //new
          Cursor.visible = false;
          target = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
          crosshair.transform.position = new Vector2(target.x,target.y);
          Vector3 difference = target - gun.transform.position;
          float rotationZ = Mathf.Atan2(difference.y,difference.x) * Mathf.Rad2Deg;

         //burada duvarda de�ilse vs kontroller yap�lacak.
         if (target.x > transform.position.x)
         {
             // Hedef noktas� karakterin sa��nda oldu�unda
             transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Y ekseni 0 derece rotasyon
             gun.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);         
         }
         else
         {
             // Hedef noktas� karakterin solunda oldu�unda
             transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Y ekseni 180 derece rotasyon
             gun.transform.localScale = new Vector3(2.3f, -2.3f, 2.3f);
         }
         gun.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            */


        if (Input.GetMouseButtonDown(0) && controller.canPlay && controller.isGrounded && !controller.isCrounch && !controller.dieBool)
        {
            if (currentAmmo > 0 && !isReloading)
            {
                ShootMet();
                AudioManager.Instance.Play("Gun1");
                Shake.Instance.ShakeCamera(5f, .1f);
                currentAmmo--;
                gunText.text = currentAmmo.ToString();
            }
            else if (currentAmmo == 0 && !isReloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = 7; // Mermi say�s�n� yeniden doldur
        gunText.text = currentAmmo.ToString();
        isReloading = false;
    }

    private void ShootMet()
    {
        Instantiate(bulletPrefab,firingPoint.position, firingPoint.rotation);
    }
    
    /*
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }

    public void DestroyFunc()
    {
        Destroy(gameObject);
    }*/
}
