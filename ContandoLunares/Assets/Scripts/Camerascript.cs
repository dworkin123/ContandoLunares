using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class Camerascript : MonoBehaviour
{
    private bool camAvailable;
    private WebCamTexture cameraTexture;
    private Texture defaultBackground;

    public RawImage background;
    public AspectRatioFitter fit;
    public bool frontFacing;
    public GameObject gamecontroller;
    public int ID;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (gamecontroller.GetComponent<gameController>().isCameraActive == true)
        {
            if (!camAvailable)
                return;
            background.texture = cameraTexture;
            float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
            fit.aspectRatio = ratio; // Set the aspect ratio

            float scaleY = cameraTexture.videoVerticallyMirrored ? -1f : 1f; // Find if the camera is mirrored or not
            background.rectTransform.localScale = new Vector3(1f, scaleY, 1f); // Swap the mirrored camera

            int orient = -cameraTexture.videoRotationAngle;
            background.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
            
           
            
        }
        
       
    }
    public void takePhoto()
    {
        if (gamecontroller.GetComponent<gameController>().isCameraActive == true)
        {
            StartCoroutine(TakePhoto());
            this.gameObject.SetActive(false);

        }
        else
        {
            this.gameObject.SetActive(false);
            
        }
    }
    public void enableCamera()
    {
        defaultBackground = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
            return;

        for (int i = 0; i < devices.Length; i++)
        {
            var curr = devices[i];

            if (curr.isFrontFacing == frontFacing)
            {
                cameraTexture = new WebCamTexture(curr.name, Screen.width, Screen.height);
                break;
            }
        }

        if (cameraTexture == null)
            return;

        cameraTexture.Play(); // Start the camera
        // Set the texture

        camAvailable = true;
    }
    IEnumerator TakePhoto()  // Start this Coroutine on some button click
    {

        // NOTE - you almost certainly have to do this here:
        print("prueba1");
       // yield return new WaitForEndOfFrame();

        // it's a rare case where the Unity doco is pretty clear,
        // http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
        // be sure to scroll down to the SECOND long example on that doco page 

        Texture2D photo = new Texture2D(cameraTexture.width, cameraTexture.height);
        photo.SetPixels(cameraTexture.GetPixels());
        photo.Apply();

        //Encode to a PNG
        byte[] bytes = photo.EncodeToPNG();

        ID = 0;
        print("prueba");
        while (File.Exists(Application.persistentDataPath + "/images/" + gamecontroller.GetComponent<gameController>().Name + "/" + gamecontroller.GetComponent<gameController>().bodypart + "/" + ID +".png")== true)
        {   

            print(ID);
            ID++;   
        }
        print("Patata");
            print(Application.persistentDataPath + "/images/" + gamecontroller.GetComponent<gameController>().Name + "/" + gamecontroller.GetComponent<gameController>().bodypart + "/"+ ID + ".png");
            File.Create(Application.persistentDataPath + "/images/" + gamecontroller.GetComponent<gameController>().Name + "/" + gamecontroller.GetComponent<gameController>().bodypart + "/" + ID + ".png").Close();
            File.WriteAllBytes(Application.persistentDataPath + "/images/" + gamecontroller.GetComponent<gameController>().Name + "/" + gamecontroller.GetComponent<gameController>().bodypart + "/" + ID + ".png", bytes);

        //Write out the PNG. Of course you have to substitute your_path for something sensible
        yield return null;
    }
}