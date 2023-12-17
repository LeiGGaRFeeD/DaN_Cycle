using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayDuration = 60f; // ������������ ����� � ��������

    public Color morningSkyColor;
    public Color daySkyColor;
    public Color eveningSkyColor;
    public Color nightSkyColor;

    public Transform sun;
    public GameObject stars;

    private float currentTime = 0f;

    private void Update()
    {
        // ��������� ������� ����� �����
        currentTime += Time.deltaTime;
        if (currentTime > dayDuration)
        {
            currentTime -= dayDuration;
        }

        // �������� ��������� ������ � ���� ���� � ����������� �� ������� �����
        float t = currentTime / dayDuration;

        // ��������� ������
        float angle = Mathf.Lerp(0f, 360f, t);
        sun.rotation = Quaternion.Euler(angle, 0f, 0f);

        // ���� ����
        Color skyColor;
        if (t < 0.25f)
        {
            skyColor = Color.Lerp(nightSkyColor, morningSkyColor, t * 4f);
        }
        else if (t < 0.5f)
        {
            skyColor = Color.Lerp(morningSkyColor, daySkyColor, (t - 0.25f) * 4f);
        }
        else if (t < 0.75f)
        {
            skyColor = Color.Lerp(daySkyColor, eveningSkyColor, (t - 0.5f) * 4f);
        }
        else
        {
            skyColor = Color.Lerp(eveningSkyColor, nightSkyColor, (t - 0.75f) * 4f);
        }
        RenderSettings.skybox.SetColor("_Tint", skyColor);

        // ��������� � ������������ �����
        stars.SetActive(t < 0.25f || t > 0.75f);
    }
}