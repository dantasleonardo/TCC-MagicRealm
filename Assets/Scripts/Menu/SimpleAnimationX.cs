using UnityEngine;

public class SimpleAnimationX : MonoBehaviour
{
    public GameObject back;
    public float animationTime;

    public void animationBack()
    {
        LeanTween.moveX(back, 50.0f, animationTime);
    }

    private void Start()
    {
        animationBack();
    }
}