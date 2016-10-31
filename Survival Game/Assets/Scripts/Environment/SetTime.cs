using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTime : MonoBehaviour {
    int hour;
    int minutes;
    float speed;
    int day;
    DayNight dn;
    Text time;
    bool timeSet;

    void Start()
    {
        time = GameObject.Find("Time").GetComponent<Text>();
        dn = GetComponent<DayNight>();
        speed = dn.DayLengthInMinutes;
    }

    void FixedUpdate()
    {
        float hour;
        hour = 7 + (dn.transform.rotation.eulerAngles.z / 15);
        if(hour >= 24)
        {
            hour -= 24;
        }
        minutes = (int)(60 * (hour - ((int)(hour))));
        this.hour = (int)hour;
        time.text = "Day: " + day + "\n" + this.hour + ":" + minutes;
        if(!timeSet && dn.transform.rotation.eulerAngles.z >= 255)
        {
            day++;
            timeSet = true;
        }
        if (dn.transform.rotation.eulerAngles.z <= 255)
        {
            timeSet = false;
        }
    }
}
