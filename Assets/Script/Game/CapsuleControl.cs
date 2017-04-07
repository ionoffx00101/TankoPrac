using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour {

    private CapsuleCharater capsule;
    private Transform capsuleCam;                  // A reference to the main camera in the scenes transform
    private Vector3 capsuleCamForward;             // The current forward direction of the camera
    private Vector3 capsuleMove;
    private bool capsuleJump;

    /* JoyStick 추가해본것 - 시작 */
    public VirtualJoystick joyStick;
    /* JoyStick 추가해본것 - 끝 */

    // Use this for initialization
    void Start () {
        // get the transform of the main camera
        if ( Camera.main != null )
        {
            capsuleCam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls." , gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        capsule = GetComponent<CapsuleCharater>();
    }
	
	// Update is called once per frame
	void Update () {
        if ( !capsuleJump )
        {
            capsuleJump = Input.GetButtonDown("Jump");
        }
    }
    private void FixedUpdate ()
    {
        /* JoyStick때문에 빼본것 - 시작 */
        /*
        // read inputs
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        */
        /* JoyStick때문에 빼본것 - 끝 */

        /* JoyStick 추가해본것 - 시작 */
        float h = joyStick.Horizontal();
        float v = joyStick.Vertical();
        /* JoyStick 추가해본것 - 끝 */

        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if ( capsuleCam != null )
        {
            // calculate camera relative direction to move:
            capsuleCamForward = Vector3.Scale(capsuleCam.forward , new Vector3(1 , 0 , 1)).normalized;
            capsuleMove = v * capsuleCamForward + h * capsuleCam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            capsuleMove = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if ( Input.GetKey(KeyCode.LeftShift) ) capsuleMove *= 0.5f;
#endif

        // pass all parameters to the character control script
        capsule.Move(capsuleMove , crouch , capsuleJump);
        capsuleJump = false;
    }

}
