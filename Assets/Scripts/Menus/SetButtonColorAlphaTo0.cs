using UnityEngine;
using UnityEngine.UI;

public class SetButtonColorAlphaTo0 : MonoBehaviour
{
    public Image otherButton;

    public void SetButtonColorAlphaTo0Method ()
    {
        Color col = GetComponent<Image>().color;
        col.a = 0;
        GetComponent<Image>().color = col;
        otherButton.color = col;
    }
}
