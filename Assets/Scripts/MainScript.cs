using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Player player;
    void Start()
    {
        
        player.Initialize();
        BulletFactory.Instance.Initialize();
        BulletManager.Instance.Initialize();
        
    }

    void Update()
    {
        player.Refresh();
        BulletManager.Instance.Refresh();
    }
}
