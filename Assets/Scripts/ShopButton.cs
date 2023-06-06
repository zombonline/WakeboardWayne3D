using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI titleText, descText, costText;
    [SerializeField] ShopItem shopItem;
    [SerializeField] Button purchaseButton;

    [SerializeField] UnityEvent onUnableToAfford, onPurchaseComplete;

    private void OnEnable()
    {
        iconImage.sprite = shopItem.icon;
        titleText.text = shopItem.itemName;
        if(descText!= null )
        {
            descText.text = shopItem.itemDesc;
        }
        if(!shopItem.storePlacePurchase)
        {
            costText.text = shopItem.cost.ToString("000");
        }
        FindObjectOfType<PlayerPrefsController>().AssignModels();


        if(PlayerPrefs.GetInt(shopItem.SHOP_ITEM_UNLOCKED_KEY, 0) == 1)
        {
            if(purchaseButton == null)
            {
                return;
            }    
            purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
    }

    public void Purchase()
    {
        if (PlayerPrefs.GetInt(shopItem.SHOP_ITEM_UNLOCKED_KEY, 0) == 0 && PlayerPrefs.GetInt(PlayerPrefsController.COINS_KEY) < shopItem.cost)
        {
            onUnableToAfford.Invoke();
            return;
        }
        else
        {
            //send shopitem SO to playerprefs to be assigned 
            FindObjectOfType<PlayerPrefsController>().SetCustomisation(shopItem);
            //subtract the cost of the shop item from the saved coins value in playerprefs
            FindObjectOfType<PlayerPrefsController>().AssignModels();
            if(PlayerPrefs.GetInt(shopItem.SHOP_ITEM_UNLOCKED_KEY, 0) == 0)
            {
                PlayerPrefs.SetInt(shopItem.SHOP_ITEM_UNLOCKED_KEY, 1);
                FindObjectOfType<PlayerPrefsController>().SetCoins(PlayerPrefs.GetInt(PlayerPrefsController.COINS_KEY) - shopItem.cost);
            }
            onPurchaseComplete.Invoke();
        }
    }

}
