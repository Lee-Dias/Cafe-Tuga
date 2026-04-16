using Unity.Cinemachine;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeCameraToBigValue(CinemachineCamera cinemachineCamera)
    {
        cinemachineCamera.Priority = 20;
    }

    // Update is called once per frame
    public void ChangeCameraToSmallValue(CinemachineCamera cinemachineCamera)
    {
        cinemachineCamera.Priority = 10;
    }
}
