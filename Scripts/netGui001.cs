using FishNet;
using UnityEngine;
using System.Net.NetworkInformation;

public sealed class netGui001 : MonoBehaviour
{
    [SerializeField]
    private string IPAddress;

    //private SyncVar<string> syncedString = new SyncVar<string>();
    private void OnGUI()
    {
        if (GUILayout.Button("Host"))
        {
            InstanceFinder.ServerManager.StartConnection();

            InstanceFinder.ClientManager.StartConnection();
        }

        if (GUILayout.Button("Connect")) InstanceFinder.ClientManager.StartConnection();

        if(GUILayout.Button("Stop"))
        {
            if(InstanceFinder.IsServerStarted)
            {
                InstanceFinder.ServerManager.StopConnection(true);
            }
            else if (InstanceFinder.IsClientStarted)
            {
                InstanceFinder.ClientManager.StopConnection();
            }
        }

        if(GUILayout.Button("Ping"))
        {
            SimplePing();
        }

        if(GUILayout.Button("Quit"))
        {
            Application.Quit();
        }

    }


    public static void SimplePing()
    {
        System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
        PingReply reply = pingSender.Send("www.contoso.com");
        var thing = pingSender.Site;
        

        if (reply.Status == IPStatus.Success)
        {
            Debug.Log($"Address: {reply.Address.ToString()}");
            Debug.Log($"RoundTrip time: {reply.RoundtripTime}");
            Debug.Log($"Time to live: {reply.Options.Ttl}");
            Debug.Log($"Don't fragment: {reply.Options.DontFragment}");
            Debug.Log($"Buffer size: {reply.Buffer.Length}");
            Debug.Log(thing);
        }
        else
        {
            Debug.Log(reply.Status);
        }
    }



}
