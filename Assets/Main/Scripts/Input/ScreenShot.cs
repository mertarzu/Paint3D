using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShot : MonoBehaviour
{
    [SerializeField] GamePlayScreen _gamePlayScreen;

    public void Initialize()
    {
        _gamePlayScreen.OnScreenShotPressed += TakeScreenShot;

    }
    public void TakeScreenShot()
    {
        _gamePlayScreen.gameObject.SetActive(false);

        StartCoroutine("screenshot");
    }

    IEnumerator screenshot()
    {
        yield return new WaitForEndOfFrame();
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();
        string name = "Screenshot_Paint3DApp" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".jpeg";
        byte[] bytes = texture.EncodeToJPG();
        //File.WriteAllBytes(Application.persistentDataPath + name ,bytes);
        NativeGallery.SaveImageToGallery(texture, "Paint3D Pictures", name);
        Destroy(texture);
        _gamePlayScreen.gameObject.SetActive(true);

    }
}
