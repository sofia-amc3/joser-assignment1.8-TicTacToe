using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] spaceList;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public DiceRoll diceRollInstance;

    private Color32 initialColor = new Color32(255, 208, 51, 255);
    private Color32 initialTextColor = new Color32(31, 16, 79, 255);
    private Color32 decidedColor = new Color32(37, 22, 102, 255);
    private Color32 decidedTextColor = Color.white;
    private Color32 disabledColor = new Color32(0, 0, 0, 0);

    public Text p1Text;
    public Text p2Text;

    public Image background;

    public Image symbolIndicator1;
    public Image symbolIndicator2;

    private string side;
    private int moves;

    // Start is called before the first frame update
    void Start()
    {
        SetGameControllerReferenceForButtons();
        side = "X";
        gameOverPanel.SetActive(false);
        moves = 0;
        restartButton.SetActive(false);
        SetInteractable(false);

    }

    /* Update is called once per frame
    void Update()
    {
        
    } */

    void SetGameControllerReferenceForButtons()
    {
        for (int i = 0; i < spaceList.Length; i++) spaceList[i].GetComponentInParent<Space>().SetControllerReference(this);
        diceRollInstance.SetControllerReference(this);
    }

    public string GetSide()
    {
        return side;
    }

    public void SetSide(string side)
    {
        this.side = side;
        SetInteractable(true);
        SwapColours();
    }

    void ChangeSide()
    {
        if (side == "X") side = "O";
        else side = "X";
    }

    public void EndTurn()
    {
        moves++;

        if (spaceList[0].text == side && spaceList[1].text == side && spaceList[2].text == side)
            GameOver();
        else if (spaceList[3].text == side && spaceList[4].text == side && spaceList[5].text == side)
            GameOver();
        else if (spaceList[6].text == side && spaceList[7].text == side && spaceList[8].text == side)
            GameOver();
        else if (spaceList[0].text == side && spaceList[3].text == side && spaceList[6].text == side)
            GameOver();
        else if (spaceList[1].text == side && spaceList[4].text == side && spaceList[7].text == side)
            GameOver();
        else if (spaceList[2].text == side && spaceList[5].text == side && spaceList[8].text == side)
            GameOver();
        else if (spaceList[0].text == side && spaceList[4].text == side && spaceList[8].text == side)
            GameOver();
        else if (spaceList[2].text == side && spaceList[4].text == side && spaceList[6].text == side)
            GameOver();
        else
        {
            diceRollInstance.ChangeButtonState(true);
            diceRollInstance.SwapColours();
            diceRollInstance.resultText.text = "";
            SwapColours();
            diceRollInstance.ResetScore();
        }

        if (moves >= 9)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "It's a tie!";
            restartButton.SetActive(true);
        }

        // Disable buttons for dice roll
        SetInteractable(false);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = side + " wins!";
        restartButton.SetActive(true);
    }

    void SetInteractable(bool setting)
    {
        for (int i = 0; i < spaceList.Length; i++)
            spaceList[i].GetComponentInParent<Button>().interactable = setting;
    }

    public void Restart()
    {
        side = "X";
        moves = 0;
        gameOverPanel.SetActive(false);
        SetInteractable(false);
        restartButton.SetActive(false);
        diceRollInstance.ChangeButtonState(true);
        diceRollInstance.SwapColours();
        SwapColours();
        diceRollInstance.ResetScore();
        diceRollInstance.resultText.text = "";

        for (int i = 0; i < spaceList.Length; i++)
            spaceList[i].text = "";
    }

    public void SwapColours()
    {
        if (symbolIndicator1.color.Equals(initialColor))
        {
            symbolIndicator1.color = decidedColor;
            symbolIndicator2.color = decidedColor;
            background.color = disabledColor;
            p1Text.color = decidedTextColor;
            p2Text.color = decidedTextColor;
        }
        else
        {
            symbolIndicator1.color = initialColor;
            symbolIndicator2.color = initialColor;
            background.color = decidedColor;
            p1Text.color = initialTextColor;
            p2Text.color = initialTextColor;
        }
    }
}
