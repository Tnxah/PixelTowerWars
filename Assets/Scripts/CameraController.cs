using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;

    private Touch touch;

    private float standaloneSensitivity = 10f;
    private float androidSensitivity = 0.5f;

    private float leftCamLimitPosition;
    private float rightCamLimitPosition;

    private float height;
    private float width;

    private void Start()
    {
        cam = Camera.main;

        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        var halfMapSize = GameManager.instance.mapSize / 2;
        leftCamLimitPosition = -halfMapSize + width / 2;
        rightCamLimitPosition = halfMapSize - width / 2;
    }
    void FixedUpdate()
    {
#if UNITY_STANDALONE_WIN
        StandaloneCameraControl();
#endif

#if UNITY_ANDROID
        AndroidCameraControl();
#endif
    }

    private void StandaloneCameraControl()
    {
        MoveCamera(Input.GetAxis("Horizontal") * Time.fixedDeltaTime * standaloneSensitivity + cam.transform.position.x);
    }

    private void AndroidCameraControl()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase.Equals(TouchPhase.Moved))
            {
                MoveCamera(-touch.deltaPosition.x * Time.fixedDeltaTime * androidSensitivity + cam.transform.position.x);
            }
        }
    }

    private void MoveCamera(float delta)
    {
        var xPosition = Mathf.Clamp(delta, leftCamLimitPosition, rightCamLimitPosition);
        cam.transform.position = new Vector3(xPosition, cam.transform.position.y, cam.transform.position.z);
    }
}
