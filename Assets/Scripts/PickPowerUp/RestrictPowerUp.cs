using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestrictPowerUp : MonoBehaviour
{
    public int allowPowerUp;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(JsonManager.instance.data.activeAllMenuPowerUps)
        {
            button.interactable = true;
        }
        else
        {
            if (JsonManager.instance.data.randomPowerUpActive == allowPowerUp)
            {
                button.interactable = true;
            }
            else
            {
                Debug.Log(JsonManager.instance.data.randomPowerUpActive);
                button.interactable = false;
            }
        }
    }

}
