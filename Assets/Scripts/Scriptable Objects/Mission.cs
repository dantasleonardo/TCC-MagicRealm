using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Mission/Create A Mission")]
public class Mission : ScriptableObject
{
    public string title;
    public string textUI_En;
    public string textUI_Pt;
    public MissionType missionType;

    //Collect Type
    public ResourceType resourceType;
    public int amountOfResources;

    //Destroy Type
    public DestroyType destroyType;

    //Crystals
    public int amountToDestroyed;
}