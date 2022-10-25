using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletSpawnData[] spawnDatas;
    public int index = 0;
    public bool isSequenceRandom;

    BulletSpawnData GetSpawnData()
    {
        return spawnDatas[index];
    }
    float timer;

    float[] rotations;
    void Start()
    {
        timer = GetSpawnData().cooldown;
        rotations = new float[GetSpawnData().NumberOfBullets];

        if (!GetSpawnData().isRandom)
        {
            DistributedRotations();
        }
    }


    void Update()
    {
        if(timer <= 0)
        {
            SpawnBullets();
            timer = GetSpawnData().cooldown;
            if (isSequenceRandom)
            {
                index = Random.Range(0, spawnDatas.Length);
            }
            else
            {
                index += 1;
                if (index >= spawnDatas.Length) index = 0;
            }
        }
        timer -= Time.deltaTime;
    }
    //gives a random rotation from min to max for each bullet.
    public float[] RandomRotations()
    {
        for (int i = 0; i < GetSpawnData().NumberOfBullets; i++)
        {
            rotations[i] = Random.Range(GetSpawnData().minRotation, GetSpawnData().maxRotation);
        }
        return rotations;
    }
    //gives a random rotations evenly distributed between min and max rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < GetSpawnData().NumberOfBullets; i++)
        {
            var fraction = (float)i / (float)GetSpawnData().NumberOfBullets;
            var difference = GetSpawnData().maxRotation - GetSpawnData().minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + GetSpawnData().minRotation;
        }
        foreach (var r in rotations) print(r);
        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        if (GetSpawnData().isRandom)
        {
            //this is in void update cuz we want random rotations for each new bullet
            RandomRotations();
        }
            //spawn bullets
        GameObject[] spawnedBullets = new GameObject[GetSpawnData().NumberOfBullets];
        for (int i = 1; i < GetSpawnData().NumberOfBullets; i++)
        {
            spawnedBullets[i] = BulletManager.GetBulletFromPool();
            if(spawnedBullets[i] == null)
            {
                spawnedBullets[i] = Instantiate(GetSpawnData().bulletResource, transform);
            }else
            {
                spawnedBullets[i].transform.SetParent(transform);
                spawnedBullets[i].transform.localPosition = Vector2.zero;
            }


            var b = spawnedBullets[i].GetComponent<Bullet>();
            if (i < GetSpawnData().NumberOfBullets)
            {
                b.rotation = rotations[i];
            }
            b.speed = GetSpawnData().bulletSpeed;
            b.velocity = GetSpawnData().bulletVelocity;
            if (GetSpawnData().isNotParent) spawnedBullets[i].transform.SetParent(null);

        }
        return spawnedBullets;
    }

}
