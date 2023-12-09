using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private Transform diceModel;
    [SerializeField] private Button rollBtn;
    [SerializeField] private Animator animator;

    [FormerlySerializedAs("orientationValue")]
    [Tooltip("L'index + 1 corespond a la valeur de la face ")]
    [SerializeField]
    private Vector3[] orientationValues;

    [Range(0f, 1f)] [SerializeField] private float randomRotateDuration = 0.5f;


    [SerializeField] private float rotateSpeed;
    [SerializeField] private int targetOrientation;

    private bool canGoToOrientation;
    private float currentTime = 0f;
    private Vector3 rdmRotate;

    private void Update()
    {
        if (canGoToOrientation)
        {
            currentTime += Time.deltaTime;

            if (currentTime < randomRotateDuration)
            {
                diceModel.Rotate(rdmRotate * (Time.deltaTime * rotateSpeed));
            }
            else
            {
                diceModel.rotation = Quaternion.Lerp(diceModel.rotation,
                    Quaternion.Euler(orientationValues[targetOrientation]), currentTime);
                if (currentTime >= 1)
                {
                    canGoToOrientation = false;
                    currentTime = 0;
                    rollBtn.interactable = true;
                    if (scoreManager.Scoring(targetOrientation + 1))
                        animator.SetTrigger(AnimatorKeys.HasLand6);
                }
            }
        }
    }

    public void Roll()
    {
        GenerateRdmRotate();
        rollBtn.interactable = false;
        targetOrientation = UnityEngine.Random.Range(0, orientationValues.Length);
        canGoToOrientation = true;
    }

    private void GenerateRdmRotate()
    {
        rdmRotate = new Vector3(
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f),
            UnityEngine.Random.Range(0f, 1f)
        );
    }
}