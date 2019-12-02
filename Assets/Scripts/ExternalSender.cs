using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(uOSC.uOscClient))]
public class ExternalSender : MonoBehaviour
{
    uOSC.uOscClient uClient = null;

    [SerializeField]
    private Transform hmd;
    [SerializeField]
    private Transform leftController;
    [SerializeField]
    private Transform rightController;

    // Start is called before the first frame update
    void Start()
    {
        uClient = GetComponent<uOSC.uOscClient>();
    }

    // Update is called once per frame
    void Update()
    {

        SendPos("/VMC/Ext/Hmd/Pos", "/VMC/Ext/Hmd/Pos/Local", hmd);
        SendPos("/VMC/Ext/Con/Pos", "/VMC/Ext/Con/Pos/Local", leftController);
        SendPos("/VMC/Ext/Con/Pos", "/VMC/Ext/Con/Pos/Local", rightController);

    }

    private void SendPos(string worldCommand, string localCommand, Transform transform)
    {
        uClient?.Send(worldCommand,
                transform.name,
                transform.position.x, transform.position.y, transform.position.z,
                transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        uClient?.Send(localCommand,
                transform.name,
                transform.localPosition.x, transform.localPosition.y, transform.localPosition.z,
                transform.localRotation.x, transform.localRotation.y, transform.localRotation.z, transform.localRotation.w);
    }

    public void ChangeOSCAddress(string address, int port)
    {
        if (uClient == null) uClient = GetComponent<uOSC.uOscClient>();
        uClient.enabled = false;
        var type = typeof(uOSC.uOscClient);
        var addressfield = type.GetField("address", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance);
        addressfield.SetValue(uClient, address);
        var portfield = type.GetField("port", BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Instance);
        portfield.SetValue(uClient, port);
        uClient.enabled = true;
    }
}
