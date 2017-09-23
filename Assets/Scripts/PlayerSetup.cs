using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

	// Use this for initialization
	void Start () {
        // Object is not the host
        if (!isLocalPlayer) { 
            DisabledComponents();
            AssignRemoteLayer();
        } else {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }
        GetComponent<Player>().Setup();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager.RegisterPlayer(GetComponent<NetworkBehaviour>().netId.ToString(), GetComponent<Player>());
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisabledComponents()
    {
        foreach (Behaviour b in componentsToDisable)
            b.enabled = false;
    }

	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);

        GameManager.UnregisterPlayer(transform.name);
    }
}
