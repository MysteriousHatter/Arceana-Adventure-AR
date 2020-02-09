using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI Upright;
    public TextMeshProUGUI Reversed;
    public Image Card_Image;

    
    public Cards[] card_list;

    public void OpenCards(int card)
    {
        title.text = card_list[card].cName;
        Upright.text = card_list[card].upright;
        Reversed.text = card_list[card].downright;
        Card_Image.sprite = card_list[card].CardImg;
    }
}
