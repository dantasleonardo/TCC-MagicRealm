using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image foregroundBar;
    public float totalValue;
    public float updateSpeedSeconds;
    public bool isACastle;
    public bool isActive;

    private void Update()
    {
        transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x,
            Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
            transform.eulerAngles.z);
    }

    public void UpdateBar(float currentValue)
    {
        var value = currentValue / totalValue;
        StartCoroutine(changeBar(value));
    }

    IEnumerator changeBar(float value)
    {
        var preChange = foregroundBar.fillAmount;
        float elapsed = 0.0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundBar.fillAmount = Mathf.Lerp(preChange, value, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundBar.fillAmount = value;
    }

    public void BarIsActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    private void OnBecameInvisible()
    {
        if (isACastle) this.gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        if (!isACastle) this.gameObject.SetActive(true);
    }
}