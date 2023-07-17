using Pv.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Pv.Unity;

public class SpeechRecognition : MonoBehaviour
{
    public string intent;
    public string keywordPath;
    public string contextPath;

    public PicovoiceManager _picovoiceManager;

    void Start()
    {
        string accessKey = "J5lviC8D5XQk9gGxRXw7I/4sqByXFqxHqurqWh3CxrQ9PL8pWkevJw=="; // your Picovoice AccessKey
        keywordPath = Path.Combine(Application.streamingAssetsPath,
                                          "ok google_windows.ppn");
        contextPath = Path.Combine(Application.streamingAssetsPath,
                                          "video_player_windows.rhn");
        try
        {
            Debug.Log("TRY");
            _picovoiceManager = PicovoiceManager.Create(
                accessKey,
                keywordPath,
                OnWakeWordDetected,
                contextPath,
                OnInferenceResult);
            Debug.Log(_picovoiceManager);
        }
        catch (Exception ex)
        {
            Debug.LogError("PicovoiceManager was unable to initialize: " + ex.ToString());
        }

        _picovoiceManager.Start();
    }

    private void OnWakeWordDetected()
    {
        // wake word detected!
        Debug.Log("HAHADETECTED");
    }

    private void OnInferenceResult(Inference inference)
    {
        if (inference.IsUnderstood)
        {
            intent = inference.Intent;
            Dictionary<string, string> slots = inference.Slots;
            // interpret intent and slots
        }
    }


}
