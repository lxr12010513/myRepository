using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//地图块脚本
public class Cell : MonoBehaviour
{
   


    //格子类型，如泉水等
    public string type;
    
    //是否可选，用于鼠标点击和悬浮事件的判断
    public bool selectable;

    //当前格子上棋子的阵营，没有棋子则为0
    public int team;

    //显示范围高亮
    public GameObject rangeCell;

    //鼠标悬浮高亮
    public GameObject selectingCell;

    //当前行动棋子高亮
    public GameObject selectedCell;

    //本格上的棋子
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

    //格子特殊效果，回血等
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
