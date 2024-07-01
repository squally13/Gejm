using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform; // Referencja do kamery
    public float parallaxEffectMultiplier; // Wsp�czynnik efektu paralaksy

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Oblicz r�nic� pozycji kamery od ostatniej klatki na osi X
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;

        // Przesu� t�o w zale�no�ci od ruchu kamery na osi X
        transform.position += new Vector3(deltaX * parallaxEffectMultiplier, 0, 0);

        // �led� pozycj� kamery na osi Y
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z);

        // Zaktualizuj pozycj� kamery
        lastCameraPosition = cameraTransform.position;
    }
}
