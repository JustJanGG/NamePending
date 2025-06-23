using TMPro;
using UnityEngine;

public class StartWave : MonoBehaviour
{
    public TMP_Text waveText;
    void Update()
    {
        if(GameManager.instance.enemyList.Count == 0)
        {
            waveText.enabled = true;
        }
        else
        {
            waveText.enabled = false;
        }
    }
}
