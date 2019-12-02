using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonEvents : MonoBehaviour
{

    [SerializeField]
    private ExternalSender externalSender;

    private int currentSelectIPBox = 0;
    private Text currentText = null;

    private string[] ipAddress = new string[] { "192", "168", "1", "2" };
    private string port = "39540";

    public void IPButton_Click(Text text)
    {
        currentSelectIPBox = int.Parse(text.name);
        currentText = text;
        ipAddress[currentSelectIPBox - 1] = "";
    }

    public void PortButton_Click(Text text)
    {
        currentSelectIPBox = 5;
        currentText = text;
        port = "";
    }

    public void NumericButton_Click(Text text)
    {
        if (currentSelectIPBox == 0) return;
        if (currentSelectIPBox == 5)
        {
            if (port.Length >= 5) return;
            port += text.text;
        }
        else
        {
            if (ipAddress[currentSelectIPBox - 1].Length >= 3) return;
            ipAddress[currentSelectIPBox - 1] += text.text;
        }
    }

    public void SetButton_Click()
    {
        if (ipAddress.Any(d => d.Length == 0)) return;
        if (string.IsNullOrEmpty(port)) return;
        externalSender.ChangeOSCAddress(string.Join(".", ipAddress), int.Parse(port));
    }


    private void Update()
    {
        if (currentSelectIPBox != 0)
        {
            if (currentSelectIPBox == 5)
            {
                currentText.text = port;
            }
            else
            {
                currentText.text = ipAddress[currentSelectIPBox - 1];
            }
        }
    }
}
