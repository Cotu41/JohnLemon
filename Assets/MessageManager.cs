using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    TextMeshProUGUI message_text;
    Image background;

    // Start is called before the first frame update
    void Start()
    {
        GroundMessage.OnMessageShown += GroundMessage_OnMessageShown;
        GroundMessage.OnMessageHidden += GroundMessage_OnMessageHidden;

        background = GetComponentInChildren<Image>();
        message_text = GetComponentInChildren<TextMeshProUGUI>();
        background.enabled = false;
        message_text.enabled = false;
    }

    private void GroundMessage_OnMessageHidden(string message)
    {
        if(message_text.text.Equals(message))
        {
            background.enabled = false;
            message_text.enabled = false;
        }
    }

    private void GroundMessage_OnMessageShown(string message)
    {
        background.enabled = true;
        message_text.text = message;
        message_text.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
