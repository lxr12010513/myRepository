using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public UIManager uIManager;
    public Animator animator;

    public GameObject[] cells;
    public GameObject[] pieces;
    public GameObject selectedPiece;
    public int[] pieceCount;

    public int currentTeam;
    public int nextTeam;

    public Text winText;
    public GameObject winTextObject;
    public GameObject winAnimation;
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
    public void OnClickMove()
    {
        CloseAllRange();
        Piece p = selectedPiece.GetComponent<Piece>();
        p.curSkill = "move";
        gameObject.GetComponent<Skills>().SkillChoose();

    }


    public void OnClickAttack()
    {
        CloseAllRange();
        Piece p = selectedPiece.GetComponent<Piece>();
        p.curSkill = "attack";
        gameObject.GetComponent<Skills>().SkillChoose();

    }


    public void OnClickSkill()
    {
        CloseAllRange();
        Piece p = selectedPiece.GetComponent<Piece>();
        p.curSkill = p.skill;
        gameObject.GetComponent<Skills>().SkillChoose();
        
    }


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
