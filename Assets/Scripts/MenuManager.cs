using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.IDidThing();
    }


    public void Play()
    {
        Debug.Log("Play");
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        #if UNITY_EDITOR
        // Stop playing the scene within the Editor
        EditorApplication.isPlaying = false;
        #else
        // This block is only compiled in a standalone build
        Application.Quit();
        #endif
    }
}
