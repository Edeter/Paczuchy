using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcQuestScript : MonoBehaviour {

    public Sprite[] spriteList;
    public Image refImage;
    public GameObject trigger;
    [Range(.5f,5f)]
    public float timeDelay = 1f;
    public float inactiveDelay = 1f;
    float currTime;
    int activeSprite = 0;
    Collider[] others;
    [SerializeField] LayerMask layerm;
    bool playerFound;
	// Use this for initialization
    
	void Start () {
        //trigger.transform.localScale += new Vector3(1, 0, 0); //scale testing
        refImage.gameObject.SetActive(false);
        playerFound = false;
	}

    // Update is called once per frame
    void Update()
    {

        others = Physics.OverlapBox(trigger.transform.position, Vector3.Scale(trigger.transform.localScale, trigger.transform.parent.transform.localScale) / 2, Quaternion.identity, layerm);
        bool curFound = false;
        foreach (Collider oth in others)
        {
            //Debug.Log(oth.name);
            if (oth.tag == "Player")
            {
                curFound = true;
                //Debug.Log("player in trigger");
            }

        }

        if (curFound)
        {
            if (!playerFound)         
                myTriggerEnter();
       
            playerFound = true;
        }
        else
        {
            if (playerFound)
                myTriggerExit();

            playerFound = false;
        }



            

    }

    void myTriggerEnter()
    {
        playerFound = true;
        Debug.Log("Trigger enter");
        StartCoroutine(StartQuestAnimation());

    }
    void myTriggerExit()
    {
        playerFound = false;
        Debug.Log("Trigger exit");
        StartCoroutine(setInactiveDelayed());
    }
    IEnumerator StartQuestAnimation()
    {
        refImage.gameObject.SetActive(true);
        foreach (Sprite sprit in spriteList)
        {
            if (playerFound)
            {
                refImage.sprite = sprit;
                Debug.Log("next");
                yield return new WaitForSeconds(timeDelay);
            }
            else
            {
                break;
            }
        }
    }
    IEnumerator setInactiveDelayed()
    {
        yield return new WaitForSeconds(inactiveDelay);
        if(!playerFound)
        refImage.gameObject.SetActive(false);
    }
}

