using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LightEstimation : MonoBehaviour
{
 

    public ARCameraManager arCameraManager;


    private Light currentLight;


    private void Awake()
    {
        currentLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            //brightnessValue.text = $"Brighntess: {args.lightEstimation.averageBrightness.Value}";
            currentLight.intensity = args.lightEstimation.averageBrightness.Value;
        }
         if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            //tempValue.text = $"Temp: {args.lightEstimation.averageColorTemperature.Value}";
            currentLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }
         
      //   if (args.lightEstimation.colorCorrection.HasValue)
      //  {
      //      colorCorrectionValue.text = $"Color: {args.lightEstimation.colorCorrection.Value}";
      //      currentLight.color = args.lightEstimation.colorCorrection.Value;
      //  }
    }



}
