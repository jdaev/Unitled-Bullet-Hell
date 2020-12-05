using System.Collections.Generic;
using UnityEngine;

public class BulletFactory
{
    private static BulletFactory _instance;

    public static BulletFactory Instance => _instance ?? (_instance = new BulletFactory());


    private Dictionary<string, GameObject> bulletPrefabDict;

    private string bulletPrefabPath = "Prefabs/Bullets/";


    public void Initialize()
    {
        bulletPrefabDict = new Dictionary<string, GameObject>();
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(bulletPrefabPath);
        foreach (GameObject prefab in allPrefabs)
        {
            Bullet bullet = prefab.GetComponent<Bullet>();
            bulletPrefabDict.Add(bullet.bulletType.ToString(), prefab);
        }
    }

    public Bullet CreateBullet(BulletType bulletType, Vector3 originPoint)
    {
        Bullet res;
        GameObject resObj;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(bulletType.ToString());
        if (poolable != null)
        {
            resObj = poolable.GetGameObject;
            res = resObj.GetComponent<Bullet>();
        }
        else
        {
            res = _CreateBullet(bulletType);
            resObj = res.gameObject;
            resObj.transform.position = originPoint;
        };
        return res;
    }

    private Bullet _CreateBullet(BulletType bulletType)
    {
        if (!bulletPrefabDict.ContainsKey(bulletType.ToString()))
        {
            Debug.Log("Bullet Not Found");
            return null;
        }

        GameObject newBulletObj = GameObject.Instantiate(bulletPrefabDict[bulletType.ToString()]);
        Bullet newBullet = newBulletObj.GetComponent<Bullet>();
        newBullet.Initialize();
        return newBullet;
    }
}