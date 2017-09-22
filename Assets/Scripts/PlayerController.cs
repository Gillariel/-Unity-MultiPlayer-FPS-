using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float mouseSensibility = 3f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

    }

    void Update()
    {
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        //Final movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        motor.Move(_velocity);

        //Rotation
        float yRota = Input.GetAxisRaw("Mouse X");

        Vector3 _rota = new Vector3(0f, yRota, 0f) * mouseSensibility;

        //Apply rotation
        motor.Rotate(_rota);

        //Camera Rotation
        float xRota = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRota = new Vector3(xRota, 0f, 0f) * mouseSensibility;

        //Apply rotation
        motor.RotateCamera(_cameraRota);
    }
}
