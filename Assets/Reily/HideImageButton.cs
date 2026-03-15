using UnityEngine;
using UnityEngine.UI;
public class HideImageButton : MonoBehaviour
{
    public GameObject imageObject;
    public GameObject buttonObject;

    public void HideImage()
    {
        imageObject.SetActive(false);
        buttonObject.SetActive(false);
    }
}
