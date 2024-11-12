using UnityEngine;

public class EnemySignToggle : MonoBehaviour
{
    [SerializeField] GameObject questionMark;
    [SerializeField] GameObject exclamationMark;

    private void Start()
    {
        // Ensure both signs are initially off
        questionMark.SetActive(false);
        exclamationMark.SetActive(false);
    }

    public void ShowQuestionMark()
    {
        questionMark.SetActive(true);
        exclamationMark.SetActive(false);
    }

    public void ShowExclamationMark()
    {
        exclamationMark.SetActive(true);
        questionMark.SetActive(false);
    }

    public void HideSigns()
    {
        questionMark.SetActive(false);
        exclamationMark.SetActive(false);
    }
}
