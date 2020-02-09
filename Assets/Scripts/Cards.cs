using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Cards")]
public class Cards : ScriptableObject
{

    public int cNumber;
    public string cName;
    public string upright;
    public string downright;
    public Sprite CardImg;

    public Cards card;

    private void OnEnable()
    {
        
    }
}
