using UnityEngine;
using UnityEngine.Events;
public class SimpleTrigger : MonoBehaviour
{
    public string triggerTag = "Player";
    public UnityEvent onTriggerEnter;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            onTriggerEnter.Invoke();    
        }
    }
}
