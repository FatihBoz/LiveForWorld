using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWave : Wave
{

    private float distance;
   public CircleWave(Transform target, WaveObj prefab,int count,float distance) : base(target,prefab,count)
   {
    this.distance=distance;
   }

public override List<WaveObj> Spawn()
    {
        List<WaveObj> spawnedEntities=new();
        for (int i = 0; i < Count; i++)
        {
            WaveObj spawnedWaveObj=Object.Instantiate(Prefab);
            spawnedWaveObj.target=Target;
            float x = Target.position.x+Mathf.Cos(i*2*Mathf.PI/Count)*distance;
            float z = Target.position.y+Mathf.Sin(i*2*Mathf.PI/Count)*distance;
            spawnedWaveObj.transform.position=new Vector3(x,1f,z);
            spawnedEntities.Add(spawnedWaveObj);
        }
        return spawnedEntities;

    }
}
