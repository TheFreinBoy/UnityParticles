using UnityEngine;

public class ToggleSwordEffect : MonoBehaviour
{
    public GameObject VFXObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {        
                bool isActive = VFXObject.activeSelf;
                VFXObject.SetActive(!isActive);
                       
        }
    }
}