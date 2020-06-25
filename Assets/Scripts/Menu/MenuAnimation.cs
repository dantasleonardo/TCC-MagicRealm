using UnityEngine;

public class MenuAnimation : MonoBehaviour
{
    public Animator title;
    public GameObject menu;

    public bool keyIsPressed;
    public GameObject pressAnyKey;
    public Animator menuAnimation;

    private void Start()
    {
        if (GameManager.instance.startedKeyPressed)
        {
            pressAnyKey.SetActive(false);
            keyIsPressed = true;
            menuAnimation.Play("MenuAnimation");
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown && !keyIsPressed)
        {
            pressAnyKey.SetActive(false);
            keyIsPressed = true;
            menuAnimation.SetBool("KeyPressed", keyIsPressed);
            GameManager.instance.startedKeyPressed = true;
        }
    }

    public void ActiveMenu()
    {
        menu.GetComponent<Animator>().SetBool("Transition", true);
        //Move as opções do menu, fazendo assim eles aparecerem de fora da tela
        //LeanTween.moveX(menu, 750.0f, 1.0f);
    }

    //Chamado quando inicia a transição da camera
    public void PressAnyKeyTransition()
    {
        //Não estava ficando legal via script, então fiz pelo animator
        //Se quiser tentar fazer por script fica a vontade
        title.SetBool("Transition", keyIsPressed);
    }
}