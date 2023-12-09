using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text scoreField;
    [SerializeField] private int valueToScore = 6;
    [SerializeField] private Animator animator;
    private int currentScore = 0;

    private void Start()
    {
        currentScore = 0;
        scoreField.text = currentScore.ToString();
    }

    public bool Scoring(int rollValue)
    {
        if (rollValue == valueToScore)
        {
            currentScore++;
            scoreField.text = currentScore.ToString();
            animator.SetTrigger(AnimatorKeys.HasLand6);
            return true;
        }
        return false;
    }

}
