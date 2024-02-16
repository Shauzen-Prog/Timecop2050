using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Create New Enemy", order = 1)]
public class EnemyFlyweightSO : ScriptableObject
{
    public float maxHealth;
    
    public float maxSpeed;
    public float maxForce;
    public float damage;
    public int probabilityToSpawnHealOnDie = 1;
    public HealItem heal;
    public LayerMask enemyLayerMask;
    public int chance;
    public AudioClip movementSound;
    public AudioClip dieSound;
}
