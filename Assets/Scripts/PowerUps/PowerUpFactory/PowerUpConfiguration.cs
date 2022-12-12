using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Power up configuration")]
public class PowerUpConfiguration : ScriptableObject
{
    [SerializeField] private PowerUp[] _powerUps;
    private Dictionary<string, PowerUp> _idToPowerUp;

    private void Awake()
    {
        _idToPowerUp = new Dictionary<string, PowerUp>();
        foreach (var powerUp in _powerUps)
        {
            _idToPowerUp.Add(powerUp.Id, powerUp);
        }
    }

    public PowerUp GetPowerUpPrefabById(string id)
    {
        if (!_idToPowerUp.TryGetValue(id, out var powerUp))
            Debug.LogError($"PowerUp with id {id} does not exit");

        return powerUp;
    }

}
