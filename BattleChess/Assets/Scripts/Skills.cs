using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //技能准备阶段，显示范围等
    public void SkillChoose()
    {
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        string type = p.curSkill;
        if (type=="move")
        {
            gameManager.CloseAllRange();
            int range = gameManager.selectedPiece.GetComponent<Piece>().moveRange;
            foreach (var c in gameManager.cells)
            {
                if (Mathf.Abs(c.transform.position.x - gameManager.selectedPiece.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - gameManager.selectedPiece.transform.position.y) <= range
                    && c.GetComponent<Cell>().team==0)
                {
                    c.GetComponent<Cell>().rangeCell.SetActive(true);
                    c.GetComponent<Cell>().selectable = true;
                }
            }
            gameManager.isSelecting = true;

        }
        else if (type=="attack")
        {
            gameManager.CloseAllRange();
            int range = gameManager.selectedPiece.GetComponent<Piece>().attackRange;
            foreach (var c in gameManager.cells)
            {    
                if (Mathf.Abs(c.transform.position.x - gameManager.selectedPiece.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - gameManager.selectedPiece.transform.position.y) <= range
                    && c.GetComponent<Cell>().team != gameManager.selectedPiece.GetComponent<Piece>().team)
                {
                    c.GetComponent<Cell>().rangeCell.SetActive(true);
                    c.GetComponent<Cell>().selectable = true;
                }
            }
            gameManager.isSelecting = true;

        }
        else if (type =="toonSkeleSoldier") {
            SkillAction(null);
     
        }
        else if (type=="toonSkeleArcher")
        {
            SkillAction(null);
            
        }
        else if (type=="wizard")
        {
            gameManager.CloseAllRange();
            int range = gameManager.selectedPiece.GetComponent<Piece>().attackRange;
            foreach (var c in gameManager.cells)
            {
                if (Mathf.Abs(c.transform.position.x - gameManager.selectedPiece.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - gameManager.selectedPiece.transform.position.y) <= range)
                {
                    c.GetComponent<Cell>().rangeCell.SetActive(true);
                    c.GetComponent<Cell>().selectable = true;
                }
            }
            gameManager.isSelecting = true;
        }
    }

    //高亮鼠标悬浮方块
    public void ShowSelectedCell(GameObject cell)
    {
        string type = gameManager.selectedPiece.GetComponent<Piece>().curSkill; 
        if (type=="move"||type=="attack")
        {
            cell.GetComponent<Cell>().selectingCell.SetActive(true);
        }
        else if(type=="wizard")
        {
            foreach(var c in gameManager.cells)
            {
                if (Mathf.Abs(c.transform.position.x - cell.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - cell.transform.position.y) <= 1)
                {
                    c.GetComponent<Cell>().selectingCell.SetActive(true);
                }
            }
        }

    }

    //取消高亮
    public void CloseSelectedCell(GameObject cell)
    {
        string type = gameManager.selectedPiece.GetComponent<Piece>().curSkill;
        if (type == "move" || type == "attack")
        {
            cell.GetComponent<Cell>().selectingCell.SetActive(false);
        }
        else if (type == "wizard")
        {
            foreach (var c in gameManager.cells)
            {
                if (Mathf.Abs(c.transform.position.x - cell.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - cell.transform.position.y) <= 1)
                {
                    c.GetComponent<Cell>().selectingCell.SetActive(false);
                }
            }
        }
    }


    //执行技能效果
    public void SkillAction(GameObject cell)
    {
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        string type = p.curSkill;
        if (type=="move")
        {
            p.SetStandCell(false);
            p.transform.position = new Vector2(cell.transform.position.x, cell.transform.position.y);
            gameManager.CloseAllRange();
            p.SetStandCell(true);
            p.hasMoved = true;
        }
        else if (type == "attack")
        {
            GameObject enemy = cell.GetComponent<Cell>().piece;
            if (enemy == null || enemy.GetComponent<Piece>().team == p.team)
            {
                return;
            }
            enemy.GetComponent<Piece>().health -= p.demage;
            gameManager.CloseAllRange();
            p.hasAttacked = true;
        }
        else if (type == "toonSkeleSoldier")
        {
            p.health += 3;

            p.hasSkilled = true;
            gameManager.CloseAllRange();

        }
        else if (type == "toonSkeleArcher")
        {
            p.attackRange++;
            p.moveRange++;

            p.hasSkilled = true;
            gameManager.CloseAllRange();

        }
        else if (type == "wizard")
        {
            int cnt = 0;
            foreach (var c in gameManager.cells)
            {
                if (Mathf.Abs(c.transform.position.x - cell.transform.position.x) +
                    Mathf.Abs(c.transform.position.y - cell.transform.position.y) <= 1)
                {

                    GameObject enemy = c.GetComponent<Cell>().piece;
                    if (enemy == null ||enemy.GetComponent<Piece>().team==p.team)
                    {
                        continue;
                    }
                    enemy.GetComponent<Piece>().health-=p.demage;
                    cnt++;
                }
            }
            if (cnt>0)
            {
                p.hasSkilled = true;
                gameManager.CloseAllRange();
            }

        }

    }
}
