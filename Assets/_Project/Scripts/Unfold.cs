using UnityEngine;

public class Unfold : MonoBehaviour
{
    bool unFolded;

    private void Awake()
    {
        TriggerAnimator("Unfold");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !unFolded)
        {
            //TriggerAnimator("Unfold");
            unFolded = true;
        }
    }
    void TriggerAnimator(string triggerString)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerString);
    }
}