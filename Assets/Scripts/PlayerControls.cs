using UnityEngine;

// using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour 
{
    // [SerializeField] InputAction movement;
    [Header("General Settings")] 
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float moveSpeed = 30f;
    [Tooltip("Horizontal Screen Range of the player ship")]
    [SerializeField] float xRange = 20f;
    [Tooltip("Vertical Screen Range of the player ship")]
    [SerializeField] float YRange = 20f;

    [Header("Screen Position Based Tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    
    [Header("Player input Based Tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;
    
    [Header("All Lasers on Player Ship")]
    [Tooltip("Add all lasers of the player ship")]
    [SerializeField] GameObject[] lasers;
    
    float horizontalMove, verticalMove;

    void Start()
    {

    }

 // void OnEnable()
 // {
 //    movement.Enable(); 
 // }

 // void OnDisable()
 // {
 //     movement.Disable();
 // }

    
    void Update()
    {
        ProcessPosition();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + verticalMove * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalMove * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessPosition()
    {
        // float horizontalRotate = movement.ReadValue<Vector2>().x;
        // Debug.Log(horizontalRotate);

        // float verticalRotate = movement.ReadValue<Vector2>().y;
        // Debug.Log(verticalRotate);

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        float xOffset = horizontalMove * Time.deltaTime * moveSpeed;
        float newXPosition = transform.localPosition.x + xOffset;
        float clampedXPositionRange = Mathf.Clamp(newXPosition, -xRange, xRange);

        float yOffset = verticalMove * Time.deltaTime * moveSpeed;
        float newYPosition = transform.localPosition.y + yOffset;
        float clampedYPositionRange = Mathf.Clamp(newYPosition, -YRange, YRange);

        transform.localPosition = new Vector3(clampedXPositionRange, clampedYPositionRange, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else 
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
  
}   
