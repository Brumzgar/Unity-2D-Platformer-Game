using System.Collections;
using UnityEngine;

public class PlayerCutsceneSounds : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PlayerCutsceneSoundsCoroutine());
    }

    IEnumerator PlayerCutsceneSoundsCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<AudioManager>().Play("PlayerLandingCutscene");
        yield return new WaitForSeconds(0.7f);
        FindObjectOfType<AudioManager>().Loop("PlayerRun");
        FindObjectOfType<AudioManager>().Play("PlayerRun");
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Stop("PlayerRun");
        yield return new WaitForSeconds(1.35f);
        FindObjectOfType<AudioManager>().Play("PlayerHit");
        yield return new WaitForSeconds(1);
        FindObjectOfType<AudioManager>().Play("BossLevelStart");
    }
}
