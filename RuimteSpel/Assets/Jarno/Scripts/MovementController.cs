using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{
    public Slider speedSlider;

    public float maxForwardSpeed = 25f;
    public float maxStrafeSpeed = 7.5f;
    public float forwardEncrease = 2f;
    public float strafeEncrease = 1f;
    public float hoverSpeed = 5f;
    public float idlespeed = 2f;

    public float currentForwardSpeed = 0;
    public float currentStrafeSpeed = 0;

    public Vector2 minMaxForwardSpeed = new Vector2(-3,20);
    public Vector2 minMaxStraveSpeed = new Vector2(-2,2);

    private float activeForwardSpeed;
    private float activeStrafeSpeed;
    private float activeHoverSpeed;
    private float activeIdleSpeed;

    public float lookRateSpeed = 90f;

    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f;
    public float rollAcceleration = 3.5f;

    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
                                                                  
