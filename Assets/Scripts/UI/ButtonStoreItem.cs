using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStoreItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemStore itemStore;
    public Button buttonItem;
    [SerializeField] private GameObject infoPopup;


    private void Start()
    {
        itemStore = GetComponent<ItemStore>();
        if (buttonItem == null)
            buttonItem = GetComponent<Button>();
        itemStore.Init();
        buttonItem.onClick.AddListener(itemStore.BuyItem);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPopup.SetActive(false);
    }
}