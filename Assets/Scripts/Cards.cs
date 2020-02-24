using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Cards")]
public class Cards : ScriptableObject
{

    public int cNumber;
    public string cName;
    [TextArea(14, 10)] public string meaning;
    [TextArea(14,10)] public string upright;
    [TextArea(14, 10)] public string downright;
    public Sprite CardImg;

    public Cards card;

    private void OnEnable()
    {
        
    }
}
