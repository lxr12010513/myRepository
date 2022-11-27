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
    public void OnClickTurnEnd()
    {
        gameManager.TurnEnd();
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(1);
    }
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
