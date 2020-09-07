/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NativeCamera; 

public class prueba : MonoBehaviour
{


void Update()
{
	if( Input.GetMouseButtonDown( 0 ) )
	{
		// Don't attempt to use the camera if it is already open
		if( NativeCamera.IsCameraBusy() )
			return;
			
		
			// Take a picture with the camera
			// If the captured image's width and/or height is greater than 512px, down-scale it
			TakePicture( 512 );
		
		
	}


}

private void TakePicture( int maxSize )
{
	NativeCamera.Permission permission = NativeCamera.TakePicture( ( path ) =>
	{
		Debug.Log( "Image path: " + path );
		if( path != null )
		{
			// Create a Texture2D from the captured image
			Texture2D texture = NativeCamera.LoadImageAtPath( path, maxSize );
			if( texture == null )
			{
				Debug.Log( "Couldn't load texture from " + path );
				return;
			}

			// Assign texture to a temporary quad and destroy it after 5 seconds
			GameObject quad = GameObject.CreatePrimitive( PrimitiveType.Quad );
			quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
			quad.transform.forward = Camera.main.transform.forward;
			quad.transform.localScale = new Vector3( 1f, texture.height / (float) texture.width, 1f );
			
			Material material = quad.GetComponent<Renderer>().material;
			if( !material.shader.isSupported ) // happens when Standard shader is not included in the build
				material.shader = Shader.Find( "Legacy Shaders/Diffuse" );

			material.mainTexture = texture;
				
			Destroy( quad, 5f );

			// If a procedural texture is not destroyed manually, 
			// it will only be freed after a scene change
			Destroy( texture, 5f );
		}
	}, maxSize );

	Debug.Log( "Permission result: " + permission );
}

    void SaveImage()
    {
        //Create a Texture2D with the size of the rendered image on the screen.
        Texture2D texture = NativeCamera.GetImageProperties(string imagePath);

        //Save the image to the Texture2D
        texture.SetPixels(cameraTexture.GetPixels());
        texture.Apply();

        //Encode it as a PNG.
        byte[] bytes = texture.EncodeToPNG();

        //Save it in a file.

        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/images/testimg.png", bytes);
        Object.Destroy(texture);
    }

}
*/