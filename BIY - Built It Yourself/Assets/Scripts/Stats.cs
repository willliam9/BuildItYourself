using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Stats : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI m_Money;
    [SerializeField]
    private TextMeshProUGUI m_Popullation;
    [SerializeField]
    private TextMeshProUGUI m_Hapinesse;
    [SerializeField]
    private TextMeshProUGUI m_Security;
    [SerializeField]
    private TextMeshProUGUI m_Electricity;
    [SerializeField]
    private TextMeshProUGUI m_Water;
    [SerializeField]
    private TextMeshProUGUI m_Pollution;
    [SerializeField]
    private TextMeshProUGUI m_Trash;
    [SerializeField]
    private TextMeshProUGUI m_Volume;

    private GameManager m_GameManager;

    void Start()
    {
        m_GameManager = FindAnyObjectByType<GameManager>();
       // Debug.LogError(m_GameManager.Electricity * 100 / 1000);
    }

    //void Update()
    //{
    //    m_Money.text = m_GameManager.Money.ToString() + "$";
    //    //m_Pollution.text = m_GameManager.Pollution.ToString();
    //    m_Hapinesse.text = m_GameManager.Happiness.ToString() +"/1000";
    //    m_Security.text = m_GameManager.Security.ToString() + "/1000";
    //   m_Electricity.text = m_GameManager.Electricity.ToString() + "/1000";
    //    m_Water.text = m_GameManager.Water.ToString() + "/1000";
    //    m_Pollution.text = m_GameManager.Pollution.ToString() + "/1000";
    //    m_Trash.text = m_GameManager.Trash.ToString() + "/1000";
    //   m_Volume.text = m_GameManager.Volume.ToString() + "/1000";


    //    ChangeColorTexte();
    //}

    private void ChangeColorTexte()
    {
        if ((m_GameManager.Happiness * 100 / 1000) >= 70)
            m_Hapinesse.color = Color.green;
        else if ((m_GameManager.Happiness * 100 / 1000) >= 40 && (m_GameManager.Happiness * 100 / 1000) <= 70)
            m_Hapinesse.color = Color.yellow;
        else if ((m_GameManager.Happiness * 100 / 1000) < 40)
            m_Hapinesse.color = Color.red;

        if ((m_GameManager.Security * 100 / 1000) >= 70)
            m_Security.color = Color.green;
        else if ((m_GameManager.Security * 100 / 1000) >= 40 && (m_GameManager.Security * 100 / 1000) <= 70)
            m_Security.color = Color.yellow;
        else if ((m_GameManager.Security * 100 / 1000) < 40)
            m_Security.color = Color.red;

        if ((m_GameManager.Electricity * 100 / 1000) >= 70)
            m_Electricity.color = Color.green;
        else if ((m_GameManager.Electricity * 100 / 1000) >= 40 && (m_GameManager.Electricity * 100 / 1000) <= 70) 
            m_Electricity.color = Color.yellow;
        else if ((m_GameManager.Electricity * 100 / 1000) < 40)
            m_Electricity.color = Color.red;

        if ((m_GameManager.Water * 100 / 1000) >= 70)
            m_Water.color = Color.green;
        else if ((m_GameManager.Water * 100 / 1000) >= 40 && (m_GameManager.Water * 100 / 1000) <= 70)
            m_Water.color = Color.yellow;
        else if ((m_GameManager.Water * 100 / 1000) < 40)
            m_Water.color = Color.red;

        if ((m_GameManager.Pollution * 100 / 1000) >= 70)
            m_Pollution.color = Color.green;
        else if ((m_GameManager.Pollution * 100 / 1000) >= 40 && (m_GameManager.Pollution * 100 / 1000) <= 70)
            m_Pollution.color = Color.yellow;
        else if ((m_GameManager.Pollution * 100 / 1000) < 40)
            m_Pollution.color = Color.red;

        if ((m_GameManager.Trash * 100 / 1000) >= 70)
            m_Trash.color = Color.green;
        else if ((m_GameManager.Trash * 100 / 1000) >= 40 && (m_GameManager.Trash * 100 / 1000) <= 70)
            m_Trash.color = Color.yellow;
        else if ((m_GameManager.Trash * 100 / 1000) < 40)
            m_Trash.color = Color.red;

        if ((m_GameManager.Volume * 100 / 1000) >= 70)
            m_Volume.color = Color.green;
        else if ((m_GameManager.Volume * 100 / 1000) >= 40 && (m_GameManager.Volume * 100 / 1000) <= 70)
            m_Volume.color = Color.yellow;
        else if ((m_GameManager.Volume * 100 / 1000) < 40)
            m_Volume.color = Color.red;
    }
}
