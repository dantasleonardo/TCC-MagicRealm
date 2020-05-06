using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;
using UnityEngine.Serialization;

public class MenuAnimation : MonoBehaviour
{
    public Animator title;
    public GameObject menu;
    
    public bool keyIsPressed;
    public GameObject pressAnyKey;
    public Animator menuAnimation;


    private void Update()
    {
        if (Input.anyKeyDown && !keyIsPressed) 
        {
            pressAnyKey.SetActive(false);
            keyIsPressed = true;
            menuAnimation.SetBool("KeyPressed", keyIsPressed);
        }
    }
    
    public void ActiveMenu()
    {
        menu.SetActive(true);
        //Move as opções do menu, fazendo assim eles aparecerem de fora da tela
        LeanTween.moveX(menu, 100.0f, 1f);
    }

    //Chamado quando inicia a transição da camera
    public void PressAnyKeyTransition() {
        //Não estava ficando legal via script, então fiz pelo animator
        //Se quiser tentar fazer por script fica a vontade
        title.SetBool("Transition", keyIsPressed);
    }
}
