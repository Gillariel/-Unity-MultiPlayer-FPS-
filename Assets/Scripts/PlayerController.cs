using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float mouseSensibility = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;


    [Header("Joint Options")]
    [SerializeField]
    private float jointSpring= 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PlayerMotor motor;
    private ConfigurableJoint joint;
    private Animator animator;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        animator = GetComponent<Animator>();
        SetJointSettings(jointSpring);
    }

    void Update()
    {
        float _xMove = Input.GetAxis("Horizontal");
        float _zMove = Input.GetAxis("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        //Final movement vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical) * speed;

        //Animate moevement
        animator.SetFloat("ForwardVelocity", _zMove);

        motor.Move(_velocity);

        //Rotation
        float yRota = Input.GetAxisRaw("Mouse X");

        Vector3 _rota = new Vector3(0f, yRota, 0f) * mouseSensibility;

        //Apply rotation
        motor.Rotate(_rota);

        //Camera Rotation
        float xRota = Input.GetAxisRaw("Mouse Y");

        float _cameraRota = xRota * mouseSensibility;

        //Apply rotation
        motor.RotateCamera(_cameraRota);

        //Calculate thruster Force
        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        //Apply the thruster force
        motor.ApplyThruster(_thrusterForce);
    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
