using System;
using System.Text;
using LocalizationSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public TextAsset m_TextFileEn;
    public TextAsset m_TextFilePt;
    public TextMeshProUGUI m_TextUI;
    public int m_RoleSize = 36;
    public int m_PersonSize = 22;
    public int m_Space = 40;
    public float m_Speed = 2.0f;

    public Canvas m_Canvas;

    private void Start()
    {
        string[] lines = new String[0];
        switch (LocalizationManager.instance.GetLanguageKey())
        {
            case LanguageKey.English:
                lines = m_TextFileEn.text.Replace("\r", "").Split('\n');
                break;
            case LanguageKey.Portuguese:
                lines = m_TextFilePt.text.Replace("\r", "").Split('\n');
                break;
        }
        StringBuilder builder = new StringBuilder();


        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (line.Contains("t:"))
            {
                line = line.Replace("t:", "").ToUpperInvariant();
                line = $"<b><size={m_RoleSize}>{line}</size></b>";
            }
            else
            {
                line = $"<b><size={m_PersonSize}>{line}</size></b>";
            }

            builder.Append(line).Append($"<size={m_Space}>\n</size>");
        }

        m_TextUI.text = builder.ToString();
        Canvas.ForceUpdateCanvases();
    }

    private void Update()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
    }
}