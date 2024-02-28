using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movements
{
    Up,
    Down,
    Left,
    Right
}

public class Ship : MonoBehaviour, IHittable
{
    public List<Movements> movements = new List<Movements>();

    List<ICommand> _commands = new List<ICommand>();

    public EnemyFlyweightSO EnemyFlyweightSo;
    
    [SerializeField] private float changeState;
    
    private void Start()
    {
        SoundManager.instance.Play(TypesSFX.Second, EnemyFlyweightSo.movementSound);
        SoundManager.instance.ChangeVolumeToSpecificSound(TypesSFX.Second, 0.5f);
        foreach (var item in movements)
        {
            switch (item)
            {
                case Movements.Up: _commands.Add(new IUp(transform, EnemyFlyweightSo.maxSpeed));
                    break;
                case Movements.Down: _commands.Add(new IDown(transform, EnemyFlyweightSo.maxSpeed));
                    break;
                case Movements.Left: _commands.Add(new ILeft(transform, EnemyFlyweightSo.maxSpeed));
                    break;
                case Movements.Right: _commands.Add(new IRight(transform, EnemyFlyweightSo.maxSpeed));
                    break;
            }
        }
        
        StartCoroutine(ArtificialUpdate());

    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        transform.position += transform.forward * (EnemyFlyweightSo.maxSpeed * Time.deltaTime);

        if (transform.position.y <= -60)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerModel>();
        
        if(player != null)
        {
            player.TakeDamage(EnemyFlyweightSo.damage);
            Die();
        }

        if (other.gameObject.layer == 13)
        {
            PlayerModel.instance.TakeDamage(EnemyFlyweightSo.damageIfHitCollisionArea);
        }
        
    }
    
    IEnumerator ArtificialUpdate()
    {
        var count = 0;

        while (_commands.Count > 0)
        {
            _commands[count].Do();
            yield return new WaitForSeconds(changeState);
            count++;

            if (count >= _commands.Count)
                count = 0;
        }
    }

    public void TakeDamage(float dmg)
    {
        Die();
    }

    void Die()
    {
        Instantiate(EnemyFlyweightSo.explotionGO, transform.position, transform.rotation);
        SoundManager.instance.Play(TypesSFX.Second, EnemyFlyweightSo.dieSound);
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg, Entity entity)
    {
        throw new NotImplementedException();
    }
    
}
