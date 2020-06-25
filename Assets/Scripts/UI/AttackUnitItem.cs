using LocalizationSystem;
using TMPro;

public class AttackUnitItem : ItemStore
{
    public AttackUnit unitProperties;
    private ItemCreation itemCreation = new ItemCreation();
    public ButtonStoreItem buttonStoreItem;
    public TextMeshProUGUI nameUnitText;
    public TextMeshProUGUI descriptionUnit;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;


    public override void Init()
    {
        itemImage.sprite = unitProperties.unitIcon;
        itemCreation.timeToCreate = unitProperties.timeToCreate;
        itemCreation.prefab = unitProperties.unitPrefab;
        itemCreation.rockCost = unitProperties.stoneCost;
        itemCreation.woodCost = unitProperties.woodCost;
        itemCreation.robotType = unitProperties.RobotType;
        woodText.text = unitProperties.woodCost.ToString();
        stoneText.text = unitProperties.stoneCost.ToString();
    }

    public override void BuyItem()
    {
        if (GameController.Instance.stackCreation.Count < 10)
        {
            GameController.Instance.resources[ResourceType.Stone] -= unitProperties.stoneCost;
            GameController.Instance.resources[ResourceType.Wood] -= unitProperties.woodCost;

            GameController.Instance.stackCreation.Add(itemCreation);
            GameController.Instance.InstantiateItemLoading(unitProperties.unitIcon);
        }
    }

    private void Update()
    {
        UpdateTextUi();
        var resources = GameController.Instance.resources;
        if (resources[ResourceType.Stone] >= unitProperties.stoneCost &&
            resources[ResourceType.Wood] >= unitProperties.woodCost)
            buttonStoreItem.buttonItem.interactable = true;
        else
            buttonStoreItem.buttonItem.interactable = false;
    }

    private void UpdateTextUi()
    {
        switch (LocalizationManager.instance.GetLanguageKey())
        {
            case LanguageKey.English:
                descriptionUnit.text = unitProperties.itemDescriptionEn;
                nameUnitText.text = unitProperties.nameItemShopEn;
                break;
            case LanguageKey.Portuguese:
                descriptionUnit.text = unitProperties.itemDescriptionPt;
                nameUnitText.text = unitProperties.nameItemShopPt;
                break;
        }
    }
}