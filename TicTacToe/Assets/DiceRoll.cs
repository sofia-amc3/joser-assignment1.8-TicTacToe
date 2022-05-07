using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public Button rollButton;
    private GameController gameController;
    private int player1result;
    private int player2result;
    private string rollSide = "X";

    private Color32 initialColor = new Color32(255, 208, 51, 255);
    private Color32 initialTextColor = new Color32(31, 16, 79, 255);
    private Color32 decidedColor = new Color32(37, 22, 102, 255);
    private Color32 decidedTextColor = Color.white;
    private Color32 disabledColor = new Color32(0, 0, 0, 0);

    public Image background;

    public Text p1Score;
    public Text p2Score;
    public Text infoText;
    public Text resultText;
    public Text p1Text;
    public Text p2Text;

    public Image scoreIndicator1;
    public Image scoreIndicator2;

    public void SetControllerReference(GameController control)
    {
        gameController = control;
    }

    public void RollDie()
    {
        int result = Random.Range(1, 7);
        switch (rollSide)
        {
            case "X":
                player1result = result;
                p1Score.text = result.ToString();
                break;

            case "O":
                player2result = result;
                p2Score.text = result.ToString();
                // After both of the dice rolls, lock the button and compare results.
                ChangeButtonState(false);
                StartCoroutine(CompareResults());
                break;

            default:
                Debug.LogError("Invalid side.");
                break;
        }
        ChangeSide();
    }

    private void ChangeSide()
    {
        if (rollSide == "X")
        {
            rollSide = "O";
            infoText.text = "Player 2, roll the dice.";
        }
        else
        {
            rollSide = "X";
            infoText.text = "Player 1, roll the dice.";
        }
    }

    public void ChangeButtonState(bool state)
    {
        rollButton.interactable = state;
    }

    public void ResetScore()
    {
        p1Score.text = "-";
        p2Score.text = "-";
    }

    IEnumerator CompareResults()
    {
        if (player1result == player2result)
        {
            resultText.text = "Tie! Both players need to roll again.";
            ChangeButtonState(true);
            yield return new WaitForSeconds(1.5f);
            ResetScore();
        }
        else if (player1result > player2result)
        {
            resultText.text = "Player 1 got the highest score!";
            gameController.SetSide("X");
            SwapColours();
        }
        else
        {
            resultText.text = "Player 2 got the highest score!";
            gameController.SetSide("O");
            SwapColours();
        }
    }

    public void SwapColours()
    {
        if (scoreIndicator1.color.Equals(initialColor))
        {
            scoreIndicator1.color = decidedColor;
            scoreIndicator2.color = decidedColor;
            background.color = disabledColor;
            p1Text.color = decidedTextColor;
            p2Text.color = decidedTextColor;
        } else
        {
            scoreIndicator1.color = initialColor;
            scoreIndicator2.color = initialColor;
            background.color = decidedColor;
            p1Text.color = initialTextColor;
            p2Text.color = initialTextColor;
        }
    }
}
