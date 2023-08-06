using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    //helikopterin gidip gelece�i sa� ve sol poziyonlar belirlecek.
    //shooting point bullet belirlenecek
    //bullet speed belirlenecek.

    [SerializeField] Transform right;
    [SerializeField] Transform left;

    

    [Header("Shooting")]
    [SerializeField] Transform shootingPoint;
    [SerializeField] int bulletSpeed;
    public GameObject bulletPrefab;
    [SerializeField] public float fireRate = 0.5f; // Ate�leme aral��� (saniye)
    private float nextFire = 0.0f; // Sonraki ate�leme zaman�

    [Header("Helicopter")]
    [SerializeField] Transform helicopter;
    [SerializeField] int helicopterSpeed;

    private bool movingLeft;

    private void Update()
    {
        if (movingLeft)
        {
            if (helicopter.position.x <= right.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }

        else
        {
            if (helicopter.position.x >= left.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        ShootToPlayer();
    }
    
    private void ShootToPlayer()
    {
        //burada karaktere do�ru ate� etmesi ayarlanacak.
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
    private void DirectionChange()
    {
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        helicopter.position = new Vector3(helicopter.position.x + Time.deltaTime * _direction * helicopterSpeed,
           helicopter.position.y, helicopter.position.z);
    }

}
