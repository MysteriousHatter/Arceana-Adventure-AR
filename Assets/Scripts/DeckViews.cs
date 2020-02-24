using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckViews : MonoBehaviour
{
    public float cardOffset;
    public GameObject cardPref;

    [SerializeField]
    float spaceBetweenCard = 0.1f;


    Deck deck;
    CardModel cardModel;
    GameObject[] cards;

    Vector3 start;
    bool isSpread = false; 

    void Start()
    {
        

        deck = gameObject.GetComponent<Deck>();
        CreateDeck();
    }

    void CreateDeck()
    {
        int num = deck.getCardNum();
        cards = new GameObject[num];
        int cardCount = 0;

        foreach (int i in deck.GetDeck())
        {
            float x = cardOffset * cardCount;
            
            cards[i] = Instantiate(cardPref) as GameObject;
            start = cards[i].transform.position;
            Vector3 temp = start + new Vector3(-x, 0f, -x);
            cards[i].transform.position = temp;

            cardModel = cards[i].GetComponent<CardModel>();
            cardModel.setIndex(i);

            cardCount++;
        }
    }

    // Extract cards from the deck
    void SpreadCards()
    {
        
        int cardCount = 0;
        foreach (int i in deck.GetDeck())
        {
            float x = spaceBetweenCard * cardCount;
            Vector3 end = start + new Vector3(x, 0f);
            Vector3 startPos = cards[i].transform.position;
            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, end, 1f));

            cardCount++;
        }
    }

    // Compress cards into the deck
    void De_Spread()
    {
        cardModel.setEmptyQueue();  // Clear current queue
        float spaceBetweenCard = 0.001f;
        int cardCount = 0;
        foreach (int i in deck.GetDeck())
        {
            float x = spaceBetweenCard * cardCount;
            Vector3 startPos = cards[i].transform.position;
            Vector3 endPos = start + new Vector3(-x, 0f, -x);
            cardModel = cards[i].GetComponent<CardModel>();
            StartCoroutine(cardModel.Move(startPos, endPos, 2f));
            cardModel.isPick = false; //reset the picking card condition

            cardCount++;

        }

    }
    private void OnMouseUpAsButton()
    {
        if (!isSpread)
        {
            SpreadCards();
            isSpread = true;
        }
        else
        {
            De_Spread();
            isSpread = false;
        }

    }

    //public bool GetDeckState()
    //{
    //    return isDeSpread;
    //}
}
