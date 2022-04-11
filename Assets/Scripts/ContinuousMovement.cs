using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1.5f;
    public float runSpeed = 3f;
    public XRNode inputSource1;
    public XRNode inputSource2;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public float additionalHeight = 0.2f;
    private XRRig rig;
    private Vector2 inputAxis;
    private bool jumping;
    private bool sprinting;
    private CharacterController character;
    
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device1 = InputDevices.GetDeviceAtXRNode(inputSource1);
        InputDevice device2 = InputDevices.GetDeviceAtXRNode(inputSource2);
        device1.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        device2.TryGetFeatureValue(CommonUsages.primaryButton, out jumping);
        device1.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out sprinting);
    }

    //Called whenever Unity updates the physics of the game
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        
        //basic movement
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        if(!sprinting)
            character.Move(direction * Time.fixedDeltaTime * speed);
        else
            character.Move(direction * Time.fixedDeltaTime * runSpeed);

        //gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded) {
            if (!jumping)
                fallingSpeed = 0;
            else
                fallingSpeed = 5;
        }
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    //this dramatically improves locomotion quality
    //prevents traveling through mesh geometry and requires VR player to physically crouch into small places
    void CapsuleFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        //used to determine whether to apply gravity or not
        //determines this using a downwards spherecast
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength);
        return hasHit;
    }
}
