using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Analytics.CustomEvent("Startup", new Dictionary<string, object>
                {
                    {"Platform",Application.platform},
                    {"LocalTime", System.DateTime.Now}
                });
    }

}
