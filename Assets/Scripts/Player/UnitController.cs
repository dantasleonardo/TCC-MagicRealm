using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    public List<UnitScript> units = new List<UnitScript>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddUnitInList(UnitScript unitScript)
    {
        this.units.Add(unitScript);
    }

    public void RemoveUnitOfList(UnitScript unitScript)
    {
        this.units.Remove(unitScript);
    }
}