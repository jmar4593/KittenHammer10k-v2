using Cinemachine;
using FishNet;
using FishNet.Object;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerSpawn : NetworkBehaviour
{
    public static PlayerSpawn instance = null;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject); 
    }

    [SerializeField]
    private List<GameObject> playerAvatars;

    public override void OnStartClient()
    {
        base.OnStartClient();


        if (!IsOwner) return;

        SetController(OwnerId);

    }

    public override void OnStopClient()
    {
        base.OnStopClient();
    }

    // Remember to use SErverRPC or object will instantiate inactive.
    [ServerRpc]
    void SetController(int ownerIdentification)
    {

        switch (ownerIdentification)
        {
            case 0:
                GameObject newAvatar1 = Instantiate(playerAvatars[ownerIdentification], transform);
                InstanceFinder.ServerManager.Spawn(newAvatar1, Owner);
                break;
            case 1:
                GameObject newAvatar2 = Instantiate(playerAvatars[ownerIdentification], transform);
                InstanceFinder.ServerManager.Spawn(newAvatar2, Owner);
                break;
            case 2:
                GameObject newAvatar3 = Instantiate(playerAvatars[ownerIdentification], transform);
                InstanceFinder.ServerManager.Spawn(newAvatar3, Owner);
                break;
            case 3:
                GameObject newAvatar4 = Instantiate(playerAvatars[ownerIdentification], transform);
                InstanceFinder.ServerManager.Spawn(newAvatar4, Owner);
                break;
        }

    }
}
