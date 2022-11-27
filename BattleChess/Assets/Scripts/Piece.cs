using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    //Æå×Ó½Å±¾

    public string curSkill;
    public string skill;
    public string skill2;
    public string skill3;

    public bool attackable;
    public int demage;
    public int health;
    public int maxHealth;

    public int moveRange;
    public int attackRange;
    public bool hasMoved;
    public bool hasAttacked;
    public bool hasSkilled;

    public int team;

    public GameManager gameManager;
    
    public Canvas canvas;
    public Slider hpBar;

    public GameObject defeatedAnimation;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetStandCell(true);

        hpBar = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        hpBar.value = 1;
  
    }

    void Update()
    {
        if (health>maxHealth)
        {
            health = maxHealth;
        }
        hpBar.value = (float)health / maxHealth;
        if (health <= 0)
        {
            SetStandCell(false);
            Instantiate(defeatedAnimation, transform.position, Quaternion.identity);
            gameManager.PieceDestroy(gameObject);
        }
    }
    
    public void OnMouseDown()
    {
        Debug.Log("this is a " + this.tag + " position: " + transform.position.x + "," + transform.position.y);
        Cell c = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {

                c = hit.collider.GetComponent<Cell>();
                
            }
        }
        if (c != null)
        {
            c.OnMouseDown();
        }

    }

    public void OnMouseEnter()
    {
        Cell c = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {

                c = hit.collider.GetComponent<Cell>();

            }
        }
        if (c != null)
        {
            c.OnMouseEnter();
        }
    }
    public void OnMouseExit()
    {
        Cell c = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {

                c = hit.collider.GetComponent<Cell>();

            }
        }
        if (c != null)
        {
            c.OnMouseExit();
        }
    }
    /*
    public void Move(float _x,float _y)
    {
        
        SetStandCell(false);
        transform.position = new Vector2(_x, _y);
        gameManager.CloseAllRange();
        SetStandCell(true);
        hasMoved = true;
        
    }
    */

    public void SetStandCell(bool _stand)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach(var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {
                if (_stand==true)
                {
                    hit.collider.GetComponent<Cell>().team = team;
                }
                else
                {
                    hit.collider.GetComponent<Cell>().team = 0;
                }
            }
        }
    }

}
