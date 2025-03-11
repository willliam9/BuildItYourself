using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Stats;
    [SerializeField]
    private GameObject m_Show;

    public void Hide()
    {
        m_Stats.SetActive(false);
        m_Show.SetActive(true);

    }

    public void Show()
    {
        m_Stats.SetActive(true);
        m_Show.SetActive(false) ;

    }

}
