using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOut : MonoBehaviour
{
    public int speed;
    public Vector2 p;
    public float finalPosition;
    public bool GoingOut = false;

    public GameObject closeMenu;
    public GameObject playthisNext;

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
            }
        }
    }

    public void GoOut()
    {
        GoingOut = true;
        closeMenu.SetActive(false);
        playthisNext.SetActive(false);
        
    }
}
