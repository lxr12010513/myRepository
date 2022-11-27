using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //µØÍ¼¿é½Å±¾

    public string type;
    
    public bool selectable;
    public int team;
    public GameObject rangeCell;
    public GameObject selectingCell;
    public GameObject selectedCell;

    public GameObject piece;

    public GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        piece = null;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Piece")
            {
                piece = hit.collider.gameObject;
            }
        }
    }

    public void CellEffect()
    {
        if (piece==null)
        {
            return;
        }
        if (type=="spring")
        {
            piece.GetComponent<Piece>().health += 2;
        }
    }


    public void OnMouseDown()
    {
        Debug.Log("this is a " + this.tag + " position: " + transform.position.x + "," + transform.position.y);
        if (selectable)
        {
              
            gameManager.GetComponent<Skills>().SkillAction(gameObject);

        }
        else
        {
            gameManager.selectedPiece = piece;
            gameManager.CloseAllRange();
        }
    }
    public void OnMouseEnter()
    {
        if (selectable && gameManager.isSelecting)
        {
            gameManager.GetComponent<Skills>().ShowSelectedCell(gameObject);
        }
    }
    public void OnMouseExit()
    {
        if (selectable && gameManager.isSelecting) {
            gameManager.GetComponent<Skills>().CloseSelectedCell(gameObject);
        }
    }
}
