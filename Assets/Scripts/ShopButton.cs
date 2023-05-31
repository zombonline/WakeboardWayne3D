using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI titleText, descText, costText;
    [SerializeField] ShopItem shopItem;
    [SerializeField] Button purchseButton;


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


        if(shopItem.unlocked)
        {
            if(purchseButton == null)
            {
                return;
            }    
            purchseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
    }

    public void Purchase()
    {
        if (shopItem.unlocked == false && false) // change this to false and check for  not enough coins
        {
            return;
        }
        else
        {
            GameObject modelPreview;
            //check what kind of object item is, assign it a transform to display model on, set the playerpref itemID to correct ID
            if (shopItem.itemType == ItemType.Costume) { PlayerPrefs.SetInt(PlayerPrefsController.COSTUME_ID_KEY, shopItem.ItemID); }
            else if (shopItem.itemType == ItemType.Board) { PlayerPrefs.SetInt(PlayerPrefsController.BOARD_ID_KEY, shopItem.ItemID); }
            else if (shopItem.itemType == ItemType.Pet) { PlayerPrefs.SetInt(PlayerPrefsController.PET_ID_KEY, shopItem.ItemID); }
            FindObjectOfType<PlayerPrefsController>().AssignModels();
            shopItem.unlocked = true;
        }
    }

}
