using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{

    public float seconds = 60f;
    TextMeshProUGUI text;
    GameManager gm;
    public GameObject endPanel;


    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine("Countdown");
        endPanel.SetActive(false);
    }

    string getMinutesSeconds()
    {
        int min = Mathf.FloorToInt(seconds / 60F);
        int sec = Mathf.FloorToInt(seconds - min * 60);
        return string.Format("{0:00}:{1:00}", min, sec);
    }

    private IEnumerator Countdown()
    {
        while (seconds > 0)
        {
            text.text = getMinutesSeconds();
            yield return new WaitForSeconds(1.0f);
            seconds--;
        }

        gm.Pause(false);
        endPanel.SetActive(true);

    }

}
