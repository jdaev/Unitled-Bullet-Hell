using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    public float bulletSpeed = 10f;
    public float range = 10f;
    private float _rangeSquared;
    public BulletType bulletType;
    public Vector3 originPoint;
    private TrailRenderer tlr;
    public virtual void Initialize()
    {
        transform.position = originPoint;
        tlr = gameObject.GetComponent<TrailRenderer>();
    }

    public void Refresh()
    {
        transform.position += Vector3.forward * (bulletSpeed * Time.deltaTime);
        OnHitTarget();
        OnReachingRangeEnd();
    }

    void OnHitTarget()
    {
    }

    void OnReachingRangeEnd()
    {
        if (Vector3.SqrMagnitude(originPoint - transform.position) > Mathf.Pow(range, 2))
        {
            BulletManager.Instance.RemoveBullet(this);
        }
    }

    public void OnFired()
    {
    }

    public void Pooled()
    {
    }

    public void DePooled()
    {
        transform.position = Vector3.zero;
        tlr.Clear();
    }

    public GameObject GetGameObject => gameObject;
}