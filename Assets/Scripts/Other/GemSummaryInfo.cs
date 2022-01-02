using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemSummaryInfo : MonoBehaviour
{
    public int gemSummary;
    public GameObject gemSummaryText;

    void Start()
    {
        gemSummary = PlayerPrefs.GetInt("gemPickedUpInfo");
        gemSummaryText.GetComponent<Text>().text = "Gems Collected: " + gemSummary.ToString() + " / 20";
    }

    void Update()
    {
        
    }
}
