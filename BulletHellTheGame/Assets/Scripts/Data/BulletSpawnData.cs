using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BulletSpawnData", order = 1)]
public class BulletSpawnData : ScriptableObject
{
    public GameObject bulletResource;
    public float minRotation;
    public float maxRotation;
    public int NumberOfBullets;
    public bool isRandom;
    public bool isNotParent;
    public float cooldown;
    public float bulletSpeed;
    public Vector2 bulletVelocity;
}
