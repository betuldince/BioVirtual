using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI stateIcon;
    public void ChangeIconText(string text)
    {
        stateIcon.text = text;
    }

}