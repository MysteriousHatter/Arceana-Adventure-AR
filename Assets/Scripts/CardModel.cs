using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    
    static List<CardModel> waitingCards;

    public Material[] materialDirectory;
    public bool isPick = false;
    public bool isDecoration = false;

    MeshRenderer myMesh;
    int cardIndex;
    bool inPickingPhase = true;

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

    public void Flip()
    {
        Vector3 start = this.transform.eulerAngles;
        Vector3 end = this.transform.eulerAngles + new Vector3(0f, -180f);
        StartCoroutine(Rotate(start, end)) ;
    }

    public IEnumerator Move(Vector3 start, Vector3 end, float speed)
    {
        float lerpTime = 0.5f;
        lerpTime = lerpTime / speed; //if speed = 1, it takes 0.5s to finsih the animation
        float currentTime = 0;
        float t = 0;

        while (t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / lerpTime;
            this.transform.position = Vector3.Lerp(start, end, t);
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
            Flip();
        }             
    }

    public void setEmptyQueue()
    {
        waitingCards.Clear();
    }


}
