using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform; // Referencja do kamery
    public float parallaxEffectMultiplier; // Wspó³czynnik efektu paralaksy

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Oblicz ró¿nicê pozycji kamery od ostatniej klatki na osi X
        float deltaX = cameraTransform.position.x - lastCameraPosition.x;

        // Przesuñ t³o w zale¿noœci od ruchu kamery na osi X
        transform.position += new Vector3(deltaX * parallaxEffectMultiplier, 0, 0);

        // ŒledŸ pozycjê kamery na osi Y
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y, transform.position.z);

        // Zaktualizuj pozycjê kamery
        lastCameraPosition = cameraTransform.position;
    }
}
