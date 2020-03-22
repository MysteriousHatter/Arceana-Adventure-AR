using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    
    static public List<CardModel> waitingCards = null;

    public Material[] materialDirectory;
    public bool isPick = false;
    public bool isDecoration = false;
    public bool isFlipped = false;

    MeshRenderer myMesh;
    int cardIndex;
    bool inPickingPhase = true;
    public int isReversed = 0; //0 is upright, 1 is reversed
    int cardOrder = 0; //keeps track of order of cards

    public Cards[] card_list;
    //private void Awake()
    //{
    //    if (waitingCards != null)
    //    {
    //        foreach (CardModel c in waitingCards)
    //        {
    //            DontDestroyOnLoad(c.gameObject);
    //        }
    //        //if (waitingCards.Contains(this))
    //        //{
    //        //    DontDestroyOnLoad(this.gameObject);
    //        //}
    //    }

    //}

    void Start()
    {
        myMesh = gameObject.GetComponent<MeshRenderer>();
        ShowFace();
        waitingCards = new List<CardModel>();
    }

    void Update()
    {
        if (isDecoration)
        {
            this.transform.Rotate(0, 90 * Time.deltaTime, 0);
        }     
    }

    public void ShowFace()
    {
        myMesh.material = materialDirectory[cardIndex];
    }

    public void setIndex(int index)
    {
        cardIndex = index;
    }

    public int getIndex()
    {
        return cardIndex;
    }

    private IEnumerator Rotate(Vector3 start, Vector3 end)
    {
        // Time the rotation takes (0.5s)
        float lerpTime = 0.5f;
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime +=  Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.eulerAngles = Vector3.Lerp(start, end, t);
            yield return null;
        }
        
    }

    public void Flip() //flips card and displays associated text
    {
        //this part randomizes whether each card is in the reversed or not, then rotates it so
        int num = Random.Range(0, 2);
        isReversed = num;
        if (isReversed == 1)
        {
            //Tuan uncomment this
            //this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0, 0, 180);
        }

        Vector3 start = this.transform.eulerAngles;
        Vector3 end = this.transform.eulerAngles + new Vector3(0f, -180f, 0f);
        StartCoroutine(Rotate(start, end));

        cardOrder++;
        Display();        
    }

    public void Display()
    {
        if (cardOrder == 1)
        {
            if (isReversed == 1)
                print(card_list[cardIndex].RePast);
            else
                print(card_list[cardIndex].Past);
        }
        else if (cardOrder == 2)
        {
            if (isReversed == 1)
                print(card_list[cardIndex].RePresent);
            else
                print(card_list[cardIndex].Present);
        }
        else
        {
            if (isReversed == 1)
                print(card_list[cardIndex].ReFuture);
            else
                print(card_list[cardIndex].Future);
        }
    }

    public IEnumerator Move(Vector3 startPos, Vector3 endPos, 
                    Quaternion startRotation, Quaternion endRotation, float speed)
    {
        float lerpTime = 0.5f;
        lerpTime = lerpTime / speed; //if speed = 1, it takes 0.5s to finsih the animation
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.position = Vector3.Lerp(startPos, endPos, t);
            this.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
    }

    public void Pick()
    {
        
        float pickSpace = 0.05f;
        if (!isPick)
        {
            this.transform.position += new Vector3(0, pickSpace);
            isPick = true;

            if (waitingCards.Count < 3)
            {
                waitingCards.Add(this); // add the current card to the waiting List              
            }

            /* Add the card to the waiting List
             * Remove the first card of the List
             * Update the card position */
            else
            {
                waitingCards.Add(this);
                CardModel temp = waitingCards[0];
                waitingCards.RemoveAt(0);

                temp.isPick = false;
                temp.transform.position += new Vector3(0, -pickSpace);                
            }        
        }
        else // de_pick
        {
            // Remove the card from the waiting List
            foreach (CardModel cModel in waitingCards)
            {
                if (cModel.cardIndex == this.cardIndex)
                {
                    waitingCards.Remove(cModel);
                    break;
                }
            }
            this.transform.position += new Vector3(0, -pickSpace);
            isPick = false;
        }
        
    }


    public CardModel TopCard()
    {
        if (waitingCards.Count > 0)
        {
            return waitingCards[waitingCards.Count - 1];
        }
        else
        {
            return null;
        }
    }
    private void OnMouseUpAsButton()
    {
        if(inPickingPhase)
        {
            //Click to Pick
            Pick();
        }
        else
        {
            //Click to flip card
            if (!isFlipped)
            {
                Flip();
                isFlipped = true;
                print("my state :" +this.isFlipped);
            }     
        }             
    }

    public void setEmptyQueue()
    {
        waitingCards.Clear();
    }

    public void setPickingPhase(bool value)
    {
        inPickingPhase = value;
    }

    public bool getPickingPhase()
    {
        return inPickingPhase;
    }
}
