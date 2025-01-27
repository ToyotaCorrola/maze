using UnityEngine;

public class Item : MonoBehaviour
{
    public enum SmellState { Good, Rotten, Questionable }
    public SmellState smellState;

    void Start()
    {
        // Randomly assign a smell state when the game starts
        AssignRandomSmell();
    }

    public void AssignRandomSmell()
    {
        int random = Random.Range(0, 3); // 0 = Good, 1 = Rotten, 2 = Questionable
        smellState = (SmellState)random;
        Debug.Log($"{gameObject.name} assigned smell: {smellState}");
    }

    public string Inspect()
    {
        switch (smellState)
        {
            case SmellState.Good:
                return "It smells good.";
            case SmellState.Rotten:
                return "It smells rotten.";
            case SmellState.Questionable:
                return "It's hard to tell...";
            default:
                return "Unknown smell.";
        }
    }

    public string Collect()
    {
        return smellState.ToString();
    }

    public string ResolveQuestionable()
    {
        return Random.Range(0, 2) == 0 ? "Good" : "Rotten"; // Randomly resolve the state
    }
}
