using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public UIManager uIManager;
    public Animator animator;

    //所有格子
    public GameObject[] cells;

    //所有棋子
    public GameObject[] pieces;

    //当前行动棋子
    public GameObject selectedPiece;

    //剩余棋子数，用于判断胜利
    public int[] pieceCount;

    //当前回合阵营
    public int currentTeam;

    //下一回合阵营
    public int nextTeam;

    //胜利动画
    public Text winText;
    public GameObject winTextObject;
    public GameObject winAnimation;

    //是否正在选择格子，如move选择移动地点，attack选择攻击目标
    public bool isSelecting;

 
    // Start is called before the first frame update
    void Start()
    {

        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GameObject.Find("TurnEndAnimation").GetComponent<Animator>();

        cells = GameObject.FindGameObjectsWithTag("Cell");
        pieces = GameObject.FindGameObjectsWithTag("Piece");
        currentTeam = 1;
        nextTeam = 2;

        
        pieceCount = new int[3];
        foreach(var piece in pieces)
        {
            Piece p = piece.GetComponent<Piece>();
            pieceCount[p.team]++;
        }

        winTextObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

            Highlight();
        
        if (Input.GetMouseButton(1))
        {
            CloseAllRange();
            selectedPiece = null;
            
        }
    }

    //关闭范围显示
    public void CloseAllRange()
    {
        isSelecting = false;
        foreach(var cell in cells)
        {
            cell.GetComponent<Cell>().rangeCell.SetActive(false);
            cell.GetComponent<Cell>().selectingCell.SetActive(false);
            cell.GetComponent<Cell>().selectable = false;
        }
    }


    //交换回合
    public void TurnEnd()
    {
        pieces = GameObject.FindGameObjectsWithTag("Piece");
        CloseAllRange();
        selectedPiece = null;
        int t = currentTeam;
        currentTeam = nextTeam;
        nextTeam = t;

        

        foreach(var c in cells)
        {

            Cell cell = c.GetComponent<Cell>();
            cell.CellEffect();
            if (cell.piece==null)
            {
                continue;
            }
            Piece p = cell.piece.GetComponent<Piece>();
            if (p.team == currentTeam)
            {
                p.hasMoved = false;
                p.hasAttacked = false;
                p.hasSkilled = false;
                p.attackable = false;
            }
        }
        if (currentTeam==1)
        {
            animator.Play("TurnEnd");
        }
        if (currentTeam == 2)
        {
            animator.Play("TurnEnd2");
        }

        Debug.Log("Turn end, now player is " + currentTeam);
    }

    //高亮当前行动棋子
    public void Highlight()
    {

        foreach (var cell in cells)
        {
            cell.GetComponent<Cell>().selectedCell.SetActive(false);
        }
        if (selectedPiece==null)
        {
            return;
        }
        RaycastHit2D[] hits = Physics2D.RaycastAll(selectedPiece.transform.position, Vector2.zero);
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Cell")
            {
                hit.collider.GetComponent<Cell>().selectedCell.SetActive(true);     
            }
        }
    }
    //销毁棋子
    public void PieceDestroy(GameObject _gameObject)
    {
        int team = _gameObject.GetComponent<Piece>().team;
        pieceCount[team]--;
        if (pieceCount[team] <= 0)
        {
            winAnimation.SetActive(true);
            winTextObject.SetActive(true);
            if (team == 1)
            {
                winText.text = "Team 2 Win!";
            }
            else
            {
                winText.text = "Team 1 Win!";
            }
        }
        
        Destroy(_gameObject);
        pieces = GameObject.FindGameObjectsWithTag("Piece");
    }

}
