using UnityEngine;
using DG.Tweening;
public class Unfold : MonoBehaviour
{
    void Awake()
    {
        transform.position = new Vector3(transform.position.x, -2.03f, transform.position.z);
        transform.DOMove(new Vector3(transform.position.x, 0, transform.position.z), 1f).OnComplete(UnfoldMe);
    }

    void UnfoldMe()
    {
        TriggerAnimator("Unfold");
    }
    void TriggerAnimator(string triggerString)
    {
        gameObject.GetComponent<Animator>().SetTrigger(triggerString);
    }
}