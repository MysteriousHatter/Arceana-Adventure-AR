using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Deck deck;
    CardModel cardModel;
    GameObject[] cards = null;
    public GameObject cardPref;
    //WaitingCards wcards = null;
    [SerializeField]
    Transform firstCard = null;
    float spaceBetween = 0.5f;

    public void Start()
    {
        StartCoroutine(SpawnCard1());
        //cardModel.setPickingPhase(false);
    }

    public IEnumerator SpawnCard()
    {
        cards = new GameObject[3];
        int cardCount = 0;
        for (int i = 0; i < 3; i++)
        {
            float x = spaceBetween * cardCount;

            cards[i] = Instantiate(cardPref) as GameObject;

            Vector3 temp = firstCard.position + new Vector3(-x, 0f, -x);
            cards[i].transform.position = temp;

            cardModel = cards[i].GetComponent<CardModel>();
            int currentIndex = CardModel.waitingCards[i].getIndex();
            cardModel.setIndex(currentIndex);

            cardCount++;
            yield return new WaitForSeconds(.5f);
        }


    }

    public IEnumerator SpawnCard1()
    {
        cards = new GameObject[3];
        int cardCount = 0;
        foreach (int i in WaitingCards.indexList)
        {
            float x = spaceBetween * cardCount;
            Vector3 temp = firstCard.position + new Vector3(x, 0f, 0f);

            cards[cardCount] = Instantiate(cardPref) as GameObject;
            cardModel = cards[cardCount].GetComponent<CardModel>();
            cardModel.setIndex(i);
            cardModel.transform.position = temp;

            cardModel.setPickingPhase(false);
            cardCount++;
            yield return new WaitForSeconds(.5f);
        }
        
    }
}
