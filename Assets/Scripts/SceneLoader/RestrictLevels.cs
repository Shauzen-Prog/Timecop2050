using UnityEngine;
using UnityEngine.UI;

public class RestrictLevels : MonoBehaviour
{
    public int neededCompletLevel;
    public Button levelButton;

    // Start is called before the first frame update
    void Start()
    {
        levelButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        levelButton.interactable = JsonManager.instance.data.completeLevel[neededCompletLevel];
    }
}
