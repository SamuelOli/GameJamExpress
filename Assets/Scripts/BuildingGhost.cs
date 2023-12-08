using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private Color canBuildColor;
    [SerializeField] private Color cannotBuildColor;
    private Transform visual;
    private SpriteRenderer spriteRenderer;
    private PlacedObjectSO placedObjectSO;

    private void Start() 
    {
        RefreshVisual();

        spriteRenderer = visual.Find("Sprite").GetComponent<SpriteRenderer>();

        GridBuildingSystem.Instance.OnSelectedChanged += Instance_OnSelectedChanged;
        GridBuildingSystem.Instance.OnCanBuild += ChangeStatusColors;
    }

    private void Instance_OnSelectedChanged(object sender, System.EventArgs e) 
    {
        RefreshVisual();
    }

    private void ChangeStatusColors(object sender, GridBuildingSystem.OnCanBuildEventArgs e)
    {
        if (e.canBuild)
            spriteRenderer.color = canBuildColor;
        else spriteRenderer.color = cannotBuildColor;
    }

    private void LateUpdate() 
    {
        Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() 
    {
        if (visual != null) 
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        placedObjectSO = GridBuildingSystem.Instance.GetPlacedObjectSO();

        if (placedObjectSO != null) 
        {
            visual = Instantiate(placedObjectSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
        }
    }
}
