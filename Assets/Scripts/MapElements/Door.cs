using UnityEngine;

public class Door : MonoBehaviour   
{
    bool open = false;
    [SerializeField] private GameObject m_closedDoorObject;
    [SerializeField] private GameObject m_openDoorObject;

    [SerializeField] private bool m_stayOpen = false;

    public void Start()
    {
        CloseDoor();
    }

    public void OpenDoor()
    {
        m_closedDoorObject.SetActive(false);
        m_openDoorObject.SetActive(true);
        open = true;
    }

    public void CloseDoor()
    {
        if (open && !m_stayOpen)
        {
            m_closedDoorObject.SetActive(true);
            m_openDoorObject.SetActive(false);
            open = false;
        }

    }


}
