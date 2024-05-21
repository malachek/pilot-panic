using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    public List<InteractableBehavior> m_NearbyInteractables { get; private set; }
    
    public InteractableBehavior[] m_SortedInteractables {  get; private set; }
    private int m_SortedIndex = 0;

    public InteractableBehavior m_SelectedInteractable { get; private set; }

    private void Awake()
    {
        m_NearbyInteractables = new List<InteractableBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<InteractableBehavior>(out InteractableBehavior interactable))
        {
            m_NearbyInteractables.Add(interactable);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<InteractableBehavior>(out InteractableBehavior interactable))
        {
            if(interactable == m_SelectedInteractable)
            {
                CycleInteractables(0);
            }

            m_NearbyInteractables.Remove(interactable);
        }
    }

    public void CycleInteractables()
    {
        CycleInteractables(1);
    }

    public void CycleInteractables(int addIndex)
    {
        m_SortedInteractables = m_NearbyInteractables.OrderBy((d) => (d.gameObject.transform.position - transform.position).sqrMagnitude).ToArray();
        
        if(m_SortedInteractables.Length <= 0)
        {
            m_SelectedInteractable = null;
            return;
        }
        
        m_SortedIndex += addIndex;
        if (m_SortedIndex >= m_SortedInteractables.Length) { m_SortedIndex = 0; }

        m_SelectedInteractable = m_SortedInteractables[m_SortedIndex];
    }

    public InteractableBehavior GetInteractable()
    {
        if (!m_NearbyInteractables.Contains(m_SelectedInteractable))
        {
            CycleInteractables();
        }
        //Debug.Log(m_SelectedInteractable);
        return m_SelectedInteractable;
    }
}