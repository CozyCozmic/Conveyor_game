using UnityEngine;

public class IndustrialMenuJuice : MonoBehaviour
{
    public RectTransform panel;
    public float Speed = 12f;
    
    [Header("Juice Settings")]
    public bool shakeOnOpen = true;
    public float shakeIntensity = 25f;

    private Vector2 targetPosition = Vector2.zero;
    private Vector2 startPosition = new Vector2(0, 1400); // Start high off-screen
    private bool isSlamming = false;
    private float currentShake = 0f;
    private Vector3 originalPanelLocalPos;

    void OnEnable()
    {
        if (panel == null) panel = GetComponent<RectTransform>();
        panel.anchoredPosition = startPosition;
        originalPanelLocalPos = panel.transform.localPosition;
        isSlamming = true;
    }

    void Update()
    {
        // 1. Hydraulic Slam Down (Runs on Unscaled Time so it works while paused)
        Vector2 currentPos = panel.anchoredPosition;
        panel.anchoredPosition = Vector2.Lerp(currentPos, targetPosition, Time.unscaledDeltaTime * Speed);

        // 2. Trigger the Impact Shake when it hits the center
        if (isSlamming && Vector2.Distance(panel.anchoredPosition, targetPosition) < 5f)
        {
            panel.anchoredPosition = targetPosition;
            isSlamming = false;
            if (shakeOnOpen) currentShake = shakeIntensity;
        }

        // 3. Apply Screen Shake Decay
        if (currentShake > 0.1f)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * currentShake;
            shakeOffset.z = 0; // Keep it 2D
            panel.transform.localPosition = originalPanelLocalPos + shakeOffset;
            currentShake = Mathf.Lerp(currentShake, 0f, Time.unscaledDeltaTime * 10f);
        }
        else if (!isSlamming)
        {
            panel.transform.localPosition = originalPanelLocalPos;
        }
    }
}