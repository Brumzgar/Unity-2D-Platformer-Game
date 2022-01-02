using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeableSlidersViaInput : MonoBehaviour
{

    [SerializeField]
    public GameObject sliderButtonToHighlight;
    [SerializeField]
    public Slider musicSliderElementToAdjust;



    // Update is called once per frame
    void Update()
    {

        //AudioListener.volume = musicSliderElementToAdjust.value/10;

        if (EventSystem.current.currentSelectedGameObject == sliderButtonToHighlight)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                --musicSliderElementToAdjust.value;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                ++musicSliderElementToAdjust.value;
            }
        }
    }
}
