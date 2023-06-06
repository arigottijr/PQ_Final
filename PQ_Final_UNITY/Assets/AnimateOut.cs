using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateOut : MonoBehaviour
{
    public int speed;
    public Vector2 p;
    public float finalPosition;
    public bool GoingOut = false;

    public GameObject closeMenu;
    public GameObject playthisNext;

    public Button artist1;
    public Button artist2;
    public Button artist3;
    public Button artist4;
    public Button artist5;
    public Button artist6;
    public Button artist7;
    public Button artist8;
    public Button artist9;
    public Button artist10;
    public Button artist11;
    public Button artist12;
    public Button artist13;
    public Button artist14;
    public Button artist15;
    public Button artist16;
    public Button artist17;
    public Button artist18;
    public Button artist19;
    public Button artist20;
    public Button artist21;
    public Button artist22;
    public Button artist23;
    public Button artist24;
    public Button artist25;
    public Button artist26;
    public Button artist27;
    public Button artist28;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GoingOut)
        {
            

            transform.Translate(Vector2.down * Time.deltaTime * speed);
            p = transform.position;
            if (p.y < finalPosition)
            {
                GoingOut = false;
            artist1.interactable = true;
            artist2.interactable = true;
            artist3.interactable = true;
            artist4.interactable = true;
            artist5.interactable = true;
            artist6.interactable = true;
            artist7.interactable = true;
            artist8.interactable = true;
            artist9.interactable = true;
            artist10.interactable = true;
            artist11.interactable = true;
            artist12.interactable = true;
            artist13.interactable = true;
            artist14.interactable = true;
            artist15.interactable = true;
            artist16.interactable = true;
            artist17.interactable = true;
            artist18.interactable = true;
            artist19.interactable = true;
            artist20.interactable = true;
            artist21.interactable = true;
            artist22.interactable = true;
            artist23.interactable = true;
            artist24.interactable = true;
            artist25.interactable = true;
            artist26.interactable = true;
            artist27.interactable = true;
            artist28.interactable = true;
            }
        }
    }

    public void GoOut()
    {
        GoingOut = true;
        closeMenu.SetActive(false);
        playthisNext.SetActive(false);
        artist1.interactable = false;
            artist2.interactable = false;
            artist3.interactable = false;
            artist4.interactable = false;
            artist5.interactable = false;
            artist6.interactable = false;
            artist7.interactable = false;
            artist8.interactable = false;
            artist9.interactable = false;
            artist10.interactable = false;
            artist11.interactable = false;
            artist12.interactable = false;
            artist13.interactable = false;
            artist14.interactable = false;
            artist15.interactable = false;
            artist16.interactable = false;
            artist17.interactable = false;
            artist18.interactable = false;
            artist19.interactable = false;
            artist20.interactable = false;
            artist21.interactable = false;
            artist22.interactable = false;
            artist23.interactable = false;
            artist24.interactable = false;
            artist25.interactable = false;
            artist26.interactable = false;
            artist27.interactable = false;
            artist28.interactable = false;
        
    }
}
