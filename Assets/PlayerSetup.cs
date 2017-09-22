using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;

	// Use this for initialization
	void Start () {
        // Object is not the host
        if (!isLocalPlayer)
            foreach (Behaviour b in componentsToDisable)
                b.enabled = false;
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }
}
