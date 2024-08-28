using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave
{
    public int Count{get;private set;}
    public Transform Target{get;private set;}
    public Enemy Prefab{get;private set;}

    public Wave(Transform target, Enemy prefab,int count)
   {
    Target=target;
    Prefab=prefab;
    Count=count;
   }
   public abstract List<Enemy> Spawn();
}
