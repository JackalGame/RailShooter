using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ship Speed")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 9f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 6f;
    [Header("Screen Boundries")]
    [Tooltip("In m")][SerializeField] float xBoundry = 3.6f;
    [Tooltip("In m")] [SerializeField] float yBoundry = 2.3f;
    

    [Header("Ship Angle")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -16f;
    [SerializeField] float positionYawFactor = 3f;
    [SerializeField] float controlRollFactor = -30f;

    [Header("Misc")]
    [SerializeField] GameObject[] guns;
 
    float xThrow, yThrow;
    bool controlsAreEnabled = true;

    void Update()
    {
        if (controlsAreEnabled)
        {
            ProcessMovement();
            ProcessRotation();
            ProcessFiring();
        }
        else
        {
            SetGunState(false);
        }
    }

    private void ProcessMovement() 
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xBoundry, xBoundry);
        float clampedYPos = Mathf.Clamp(rawYPos, -yBoundry, yBoundry);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            SetGunState(true);
        }
        else
        {
            SetGunState(false);
        }
    }

    private void SetGunState(bool isActive) 
    {
        foreach(GameObject gun in guns)
        {
            var particleEmission = gun.GetComponent<ParticleSystem>().emission;
            particleEmission.enabled = isActive; 
        }
    }

    private void OnPlayerDeath() // called by string reference via SendMessage().
    {
        controlsAreEnabled = false;
    }
}
