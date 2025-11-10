using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public int timeTillNextDay = 5000; //IN SECONDS
    [SerializeField] TextMeshProUGUI timerText;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(UpdateTimer());
        StartCoroutine(DecrementTimer()); //Might implement multiplayer so I separate
    }

    IEnumerator DecrementTimer()
    {
        while (true)
        {
            timeTillNextDay -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator UpdateTimer()
    {
        while (true)
        {
            int hrs = (timeTillNextDay / 60 / 60);
            int mins = (timeTillNextDay / 60) - (hrs * 60);
            int sec = timeTillNextDay - ((hrs * 60 * 60) + (mins * 60));

            string hrsText = "00";
            string minsText = "00";
            string secText = "00";

            if (hrs > 0 && hrs < 10)
                hrsText = "0" + hrs;
            else if (hrs >= 10)
                hrsText = hrs.ToString();

            if (mins > 0 && mins < 10)
                minsText = "0" + mins;
            else if (mins >= 10)
                minsText = mins.ToString();

            if (sec > 0 && sec < 10)
                secText = "0" + sec;
            else if (sec >= 10)
                secText = sec.ToString();

            timerText.text = "Next day in: " +  hrsText + ":" + minsText + ":" + secText;



            yield return new WaitForSeconds(1);
        }
    }
}
