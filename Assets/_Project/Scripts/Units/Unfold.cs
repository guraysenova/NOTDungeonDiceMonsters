using UnityEngine;
public class Unfold : MonoBehaviour
{
    void Awake()
    {
        TriggerAnimator("Unfold");
    }
    void TriggerAnimator(string triggerString)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerString);
    }
}