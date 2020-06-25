using UnityEngine;

public class RockSize : MonoBehaviour
{
    public GameObject fullSize;
    public GameObject fullSizeMesh;
    public GameObject minedRock;
    [Range(0.0f, 1.0f)] public float size;
    Resources resource;
    int totalResources = 0;

    private void Start()
    {
        resource = GetComponent<Resources>();
        totalResources = resource.amountResources;
        minedRock.SetActive(false);
    }

    private void Update()
    {
        size = (float) resource.amountResources / (float) totalResources;
        ShrinkInSize();
    }

    private void ShrinkInSize()
    {
        if (size > 0.6f)
            fullSize.transform.localScale = new Vector3(100.0f, 100.0f, 100.0f);
        else if (size <= 0.6f && size >= 0.35f)
            fullSize.transform.localScale = new Vector3(75.0f, 75.0f, 75.0f);
        else
        {
            fullSizeMesh.SetActive(false);
            minedRock.SetActive(true);
        }
    }
}