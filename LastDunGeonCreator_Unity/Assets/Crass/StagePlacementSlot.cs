
using UnityEngine;

public class StagePlacementSlot : MonoBehaviour
{
    public StagePlacementSlot up;
    public StagePlacementSlot down;
    public StagePlacementSlot left;
    public StagePlacementSlot right;

    public GameObject selectArrow;
    public GameObject placedObject;

    [HideInInspector]
    public int placedObjectIndex = -1;

    [Header("配置位置調整")]
    [SerializeField] private float slotHalfHeight = 0.5f;
    [SerializeField] private float bottomOffset = 0f;

    [Header("配置枠")]
    [SerializeField] private SpriteRenderer slotFrameRenderer;

    public void SetSelect(bool value)
    {
        if (selectArrow != null)
            selectArrow.SetActive(value);
    }

    public void PlaceObject(GameObject prefab)
    {
        if (prefab == null)
            return;

        RemoveObject();

        placedObject = Instantiate(prefab, transform);
        placedObject.transform.localPosition = Vector3.zero;
        placedObject.transform.localRotation = Quaternion.identity;

        SpriteRenderer[] renderers =
            placedObject.GetComponentsInChildren<SpriteRenderer>();

        if (renderers.Length == 0)
            return;

        Bounds bounds = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
            bounds.Encapsulate(renderers[i].bounds);

        float slotBottomY = transform.position.y - 0.5f;
        float offsetY = slotBottomY - bounds.min.y;

        placedObject.transform.position += Vector3.up * offsetY;
    }

    public void RemoveObject()
    {
        if (placedObject != null)
        {
            Destroy(placedObject);
            placedObject = null;
        }

        placedObjectIndex = -1;
    }

    public bool IsPlaced()
    {
        return placedObject != null;
    }

    public void HideFrame()
    {
        SetSelect(false);

        if (slotFrameRenderer != null)
            slotFrameRenderer.enabled = false;
    }
}
