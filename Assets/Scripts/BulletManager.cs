using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    private static BulletManager _instance;

    public static BulletManager Instance => _instance ?? (_instance = new BulletManager());
    Dictionary<BulletType, List<Bullet>> bulletDict;
    Stack<Bullet> bulletsToRemoveStack;
    Stack<Bullet> bulletsToAddStack;

    private BulletManager()
    {
        bulletDict = new Dictionary<BulletType, List<Bullet>>();
        bulletsToRemoveStack = new Stack<Bullet>();
        bulletsToAddStack = new Stack<Bullet>();
    }

    public void Initialize()
    {
    }

    public void Refresh()
    {
        while (bulletsToRemoveStack.Count > 0)
        {
            Bullet toRemove = bulletsToRemoveStack.Pop();
            BulletType bulletType = toRemove.bulletType;
            if (!bulletDict.ContainsKey(bulletType) || !bulletDict[bulletType].Contains(toRemove))
            {
                Debug.LogError("Stack tried to remove element of type: " + bulletType.ToString() +
                               " but was not found in dictionary?");
            }
            else
            {
                bulletDict[bulletType].Remove(toRemove);
                ObjectPool.Instance.AddToPool(toRemove.bulletType.ToString(), toRemove);
                if (bulletDict[bulletType].Count == 0)
                    bulletDict.Remove(bulletType);
            }
        }


        //Add Bullets to the dictionary from the "toAdd stack"
        while (bulletsToAddStack.Count > 0)
        {
            Bullet toAdd = bulletsToAddStack.Pop();
            BulletType bulletType = toAdd.bulletType;

            if (!bulletDict.ContainsKey(bulletType)) // || !bulletDict[kv.Key].Contains(kv.Value))
            {
                bulletDict.Add(bulletType, new List<Bullet>() {toAdd});
            }
            else if (!bulletDict[bulletType].Contains(toAdd))
            {
                bulletDict[bulletType].Add(toAdd);
            }
            else
            {
                //Spotting an error where the same monster is being initialized twice is almost impossible sometimes
                Debug.LogError("The bullet you are trying to add is already in the bullet dict");
            }
        }


        foreach (KeyValuePair<BulletType, List<Bullet>> kv in bulletDict)
        foreach (Bullet b in kv.Value)
            b.Refresh();
    }

    public void ShootBullet(BulletType type, Vector3 originPoint)
    {
        Bullet bullet = BulletFactory.Instance.CreateBullet(type, originPoint);
        AddBullet(bullet);
    }
    
    public void AddBullet(Bullet toAdd)
    {
        bulletsToAddStack.Push(toAdd);
    }

    public void RemoveBullet(Bullet toRemove)
    {
        bulletsToRemoveStack.Push(toRemove);
    }
}