using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Text demage;
    public Text health;
    public Text moveRange;
    public Text attackRange;
    public GameObject attackButton;
    public GameObject moveButton;
    public GameObject skillButton;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        ShowButtons();
        ShowPieceInformation();
    }

    //����turnend��ť�����
    public void OnClickTurnEnd()
    {
        gameManager.TurnEnd();
    }

    //����restart��ť�����
    public void OnClickRestart()
    {
        SceneManager.LoadScene(1);
    }

    //�Ҳ�move��ť�����
    public void OnClickMove()
    {
        gameManager.CloseAllRange();
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        p.curSkill = "move";
        gameManager.GetComponent<Skills>().SkillChoose();

    }

    //�Ҳ�attack��ť�����
    public void OnClickAttack()
    {
        gameManager.CloseAllRange();
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        p.curSkill = "attack";
        gameManager.GetComponent<Skills>().SkillChoose();

    }

    //�Ҳ�skill��ť�����
    public void OnClickSkill()
    {
        gameManager.CloseAllRange();
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        p.curSkill = p.skill;
        gameManager.GetComponent<Skills>().SkillChoose();

    }

    //��ʾ�Ҳఴť
    public void ShowButtons()
    {
        attackButton.SetActive(false);
        moveButton.SetActive(false);
        skillButton.SetActive(false);
        if (gameManager.selectedPiece == null || gameManager.selectedPiece.GetComponent<Piece>().team!=gameManager.currentTeam)
        {
            return;
        }
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        attackButton.SetActive(true);
        moveButton.SetActive(true);
        skillButton.SetActive(true);
        attackButton.GetComponent<Button>().interactable = !p.hasAttacked;
        moveButton.GetComponent<Button>().interactable = !p.hasMoved;
        skillButton.GetComponent<Button>().interactable = !p.hasSkilled;
    }


    //���Ͻ���ʾ����
    public void ShowPieceInformation()
    {
        demage.text = "";
        health.text = "";
        moveRange.text = "";
        attackRange.text = "";
        if (gameManager.selectedPiece==null)
        {
            return;
        }
        Piece p = gameManager.selectedPiece.GetComponent<Piece>();
        demage.text = p.demage.ToString();
        health.text = p.health.ToString();
        moveRange.text = p.moveRange.ToString();
        attackRange.text = p.attackRange.ToString();
    }

}
