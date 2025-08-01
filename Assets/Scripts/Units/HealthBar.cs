using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    private Transform target;
    private Vector3 offset = new Vector3(0, 1f, 0);

    public void Initialize(Transform followTarget)
    {
        target = followTarget;
    }

    public void UpdateHealth(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.forward = Camera.main.transform.forward; // Mantener visible
        }
    }
}
