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

    [SerializeField] private float speed;
    [SerializeField] private float changeState;

    public AudioClip moveSound;
    public AudioClip dieSound;

    private void Start()
    {
        SoundManager.instance.Play(TypesSFX.Second, moveSound);
        foreach (var item in movements)
        {
            switch (item)
            {
                case Movements.Up: _commands.Add(new IUp(transform, speed));
                    break;
                case Movements.Down: _commands.Add(new IDown(transform, speed));
                    break;
                case Movements.Left: _commands.Add(new ILeft(transform, speed));
                    break;
                case Movements.Right: _commands.Add(new IRight(transform, speed));
                    break;
            }
        }

        //StartCoroutine(ArtificialUpdate());

    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerModel>();

        if (other.gameObject.layer == 13)
        {
            Die();
        }

        if(player != null)
        {
            StartCoroutine(ArtificialUpdate());
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
        SoundManager.instance.Play(TypesSFX.Second, dieSound);
        Destroy(gameObject);
    }
}
