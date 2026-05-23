using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Kameranın baktığı yöne paralel olarak dönmesini sağlar (Ters dönmesini engeller)
            transform.LookAt(transform.position + mainCamera.transform.forward);
        }
    }
}