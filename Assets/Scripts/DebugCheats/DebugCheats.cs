using System;
using UnityEngine;

public class DebugCheats : MonoBehaviour
{
#if UNITY_EDITOR
    
    [SerializeField] private GameObject CheatsExplain;
    private PlayerModel _player;
    
    private bool cheatsActive;
    Action _ArtificialUpdate;
    
    public void Start()
    {
        _player = PlayerModel.instance;
        _ArtificialUpdate = () => { };

        cheatsActive = false;
    }
    
    public void Update()
    {
        _ArtificialUpdate();

        if (!Input.GetKeyDown(KeyCode.F1)) return;
        
        cheatsActive = !cheatsActive;
            
        if (cheatsActive)
        {
            CheatsExplain.SetActive(cheatsActive);
            _ArtificialUpdate = ActiveOrDeactivateCheats;
        }
        else
        {
            CheatsExplain.SetActive(cheatsActive);
            _ArtificialUpdate = () => { };
        }
        
    }

    private void ActiveOrDeactivateCheats()
    {
        if (Input.GetKeyDown(KeyCode.D) && cheatsActive)
        {
            _player.TakeDamage(10f);
        }
        
        if (Input.GetKeyDown(KeyCode.K) && cheatsActive)
        {
            _player.Die();
        }
    }
    
#endif
}
