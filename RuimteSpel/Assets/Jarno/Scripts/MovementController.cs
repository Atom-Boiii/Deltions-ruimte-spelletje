using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public Slider speedSlider;

    [Header("Speed Settings")]
    public float maxForwardSpeed = 25f;
    public float maxStrafeSpeed = 7.5f;
    public float forwardEncrease = 2f;
    public float strafeEncrease = 1f;
    public float hoverSpeed = 5f;
    public float idlespeed = 2f;

    [Header("Direction Indicator")]
    public Transform directionIndicator;

    [Header("CurrentSpeed")]
    public float currentForwardSpeed = 0;
    public float currentStrafeSpeed = 0;

    [Header("MinMaxSpeed")]
    public Vector2 minMaxForwardSpeed = new Vector2(-3,20);
    public Vector2 minMaxStraveSpeed = new Vector2(-2,2);

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float activeIdleSpeed;

    [Header("Rotate to cursor speed")]
    public float lookRateSpeed = 90f;

    private Vector2 lookInput, screenCenter, mouseDistance;

    [Header("Roll Speed")]
    private float rollInput;
    public float rollSpeed = 90f;
    public float rollAcceleration = 3.5f;

    [Header("Screen Shake")]
    public Transform cameraObject;
    public Camera cameraa;
    public Vector3 offset;
    private Vector3 localposition;
    private float screenShakeDuration;
    private float screenShakeIntensity;

    [Header("FreeLook")]
    private bool freeLookActive;
    public float freeLookSens = 1;
    private Vector3 originalCameraRot;

    

    void Start()
    {
        minMaxForwardSpeed.y = PlayerPrefs.GetFloat("MaxSpeed");
        if (minMaxForwardSpeed.y == 0)
            minMaxForwardSpeed.y = 100;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;

        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        localposition = cameraObject.transform.localPosition;
        originalCameraRot = cameraObject.localEulerAngles;
    }



    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        Vector2 mousePos = Input.mousePosition;

        directionIndicator.position = mousePos;

        if(speedSlider != null)
        {
            speedSlider.value = currentForwardSpeed;
        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        if (!freeLookActive)
            transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);

        //foreward
        currentForwardSpeed += Input.GetAxisRaw("Vertical") * forwardEncrease * Time.deltaTime;
        if (currentForwardSpeed < minMaxForwardSpeed.x)
            currentForwardSpeed = minMaxForwardSpeed.x;
        if (currentForwardSpeed > minMaxForwardSpeed.y)
            currentForwardSpeed = minMaxForwardSpeed.y;
        //strave
        currentStrafeSpeed += Input.GetAxisRaw("Horizontal") * strafeEncrease * Time.deltaTime;
        if (currentStrafeSpeed < minMaxStraveSpeed.x)
            currentStrafeSpeed = minMaxStraveSpeed.x;
        if (currentStrafeSpeed > minMaxStraveSpeed.y)
            currentStrafeSpeed = minMaxStraveSpeed.y;


        //ScreenShake
        if (screenShakeDuration > 0)
        {
            cameraObject.transform.localPosition = new Vector3(localposition.x + Random.insideUnitSphere.x * screenShakeIntensity, localposition.y + Random.insideUnitSphere.y * screenShakeIntensity, localposition.z);
            screenShakeDuration -= 1 * Time.deltaTime;
        }
        else
        {
            cameraObject.transform.localPosition = localposition;
        }

        //check screenshake
        if (currentForwardSpeed <= minMaxForwardSpeed.x || currentForwardSpeed == minMaxForwardSpeed.y)
        { }else
        { 
            if (Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Vertical") == 1)
            {
                Effect_ScreenShake(.1f, 0.1f);
            }
        }
     

        /*
        currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, Input.GetAxisRaw("Vertical") * maxForwardSpeed, forwardAcceleration * Time.deltaTime);
        else
            currentForwardSpeed = Mathf.Lerp(currentForwardSpeed, idlespeed, forwardAcceleration * Time.deltaTime);

        currentStraceSpeed = Mathf.Lerp(currentStraceSpeed, Input.GetAxisRaw("Horizontal") * maxStrafeSpeed, strafeAcceleration * Time.deltaTime);
        //activeIdleSpeed = Mathf.Lerp(activeIdleSpeed, idlespeed, idleAcceleration * Time.deltaTime);

        
            
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        */

        transform.position += transform.forward * currentForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * currentStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);

        //freelook
        if (Input.GetKey(KeyCode.LeftAlt))
            freeLookActive = true;
        else
            freeLookActive = false;

        if (freeLookActive)
        {
            float mousex = Input.GetAxis("Mouse X") * freeLookSens;
            float mousey = Input.GetAxis("Mouse Y") * freeLookSens;
            cameraObject.transform.eulerAngles += new Vector3(-mousey, mousex, 0);
        }
        else
            cameraObject.transform.localEulerAngles = originalCameraRot;

        //backlook
        if(Input.GetKey(KeyCode.Space))
        {
            cameraObject.transform.localEulerAngles = new Vector3(cameraObject.transform.localEulerAngles.x,180, cameraObject.transform.localEulerAngles.z);
        }


        //FOV / Camera swing
        cameraa.fieldOfView = 70 + currentForwardSpeed / 2;
        cameraa.transform.localPosition = new Vector3(localposition.x + mouseDistance.x/1.5f, localposition.y, localposition.z) + offset;
    }

    public void Effect_ScreenShake(float duration, float intesity)
    {
        screenShakeDuration = duration;
        screenShakeIntensity = intesity;
    }
}
                                                                  
