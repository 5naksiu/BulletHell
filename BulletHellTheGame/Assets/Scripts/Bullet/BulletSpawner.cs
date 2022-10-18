using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int NumberOfBullets;
    public bool isRandom;

    public float cooldown;
    float timer;
    public float bulletSpeed;
    public Vector2 bulletVelocity;

    float[] rotations;
    void Start()
    {
        timer = cooldown;
        rotations = new float[NumberOfBullets];

        if (!isRandom)
        {
            DistributedRotations();
        }
    }


    void Update()
    {
        if(timer <= 0)
        {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }
    //gives a random rotation from min to max for each bullet.
    public float[] RandomRotations()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;
    }
    //gives a random rotations evenly distributed between min and max rotation.
    public float[] DistributedRotations()
    {
        for (int i = 0; i < NumberOfBullets; i++)
        {
            var fraction = (float)i / (float)NumberOfBullets;
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference + minRotation;
        }
        foreach (var r in rotations) print(r);
        return rotations;
    }
    public GameObject[] SpawnBullets()
    {
        if (isRandom)
        {
            //this is in void update cuz we want random rotations for each new bullet
            RandomRotations();
        }
            //spawnbullet
        GameObject[] spawnedBullets = new GameObject[NumberOfBullets];
        for (int i = 0; i < NumberOfBullets; i++)
        {
            spawnedBullets[i] = Instantiate(bulletResource, transform);
            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
        }
        return spawnedBullets;
    }

}
