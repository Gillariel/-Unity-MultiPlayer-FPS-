using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

	void Start () {
        if(cam == null)
        {
            Debug.LogError("[PlayerShoot.cs] No referenced Camera.");
            this.enabled = false;
        }		
	}

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }    
    }

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if(_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
            
    }

    //only on server
    [Command]
    void CmdPlayerShot(string _playerID, int damage)
    {
        Player player = GameManager.GetPlayer(_playerID);
        player.RpcTakeDamage(damage);
    }
}
