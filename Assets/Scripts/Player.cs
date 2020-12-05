using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void Initialize()
    {
    }

    
    public void Refresh()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BulletManager.Instance.ShootBullet(BulletType.Energy,Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            BulletManager.Instance.ShootBullet(BulletType.Explosive,Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            BulletManager.Instance.ShootBullet(BulletType.Kinetic,Vector3.forward);
        }
    }
}
