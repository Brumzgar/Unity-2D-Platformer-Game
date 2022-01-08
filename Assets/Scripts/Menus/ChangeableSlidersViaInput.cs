using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeableSlidersViaInput : MonoBehaviour
{
    [SerializeField]
    public GameObject sliderButtonToHighlight;
    [SerializeField]
    public Slider sliderToAdjust;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == sliderButtonToHighlight)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {               
                sliderToAdjust.value = sliderToAdjust.value - 0.1f;
                FindObjectOfType<AudioManager>().Play("MenuDecrease");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                sliderToAdjust.value = sliderToAdjust.value + 0.1f;
                FindObjectOfType<AudioManager>().Play("MenuIncrease");
            }
        }
    }
}
