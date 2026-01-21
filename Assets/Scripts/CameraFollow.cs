using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("El objeto que la cámara debe seguir.")]
    public Transform target;

    [Header("Límites de movimiento")]
    [Tooltip("Límite mínimo y máximo en el eje X.")]
    public Vector2 xLimits = new Vector2(-10, 10);

    [Tooltip("Límite mínimo y máximo en el eje Y.")]
    public Vector2 yLimits = new Vector2(-5, 5);

    [Header("Ajustes de cámara")]
    [Tooltip("Velocidad de seguimiento de la cámara.")]
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
        Mathf.Clamp(target.position.x, xLimits.x, xLimits.y),
        Mathf.Clamp(target.position.y, yLimits.x, yLimits.y),
        transform.position.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
