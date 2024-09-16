using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller; // Componente para controlar o movimento do personagem
    private Transform myCamera; // Refer�ncia � transforma��o da c�mera principal

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform; 
    }

    // Update is called once per frame
    void Update()
    {
         float horizontal = Input.GetAxis("Horizontal"); // Obt�m o input horizontal (A, D, esquerda, direita)
        float vertical = Input.GetAxis("Vertical"); // Obt�m o input vertical (W, S, cima, baixo)
        Vector3 movimento = new Vector3(horizontal, 0, vertical); // Cria um vetor de movimento baseado nos inputs

        movimento = myCamera.TransformDirection(movimento); // Ajusta o vetor de movimento para corresponder � orienta��o da c�mera
        movimento.y = 0; // Garante que n�o h� movimento no eixo Y (vertical)

        controller.Move(movimento * Time.deltaTime * 3f); // Aplica o movimento ao personagem
        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime); // Aplica gravidade ao personagem

        // Rota��o do personagem para enfrentar a dire��o do movimento
        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("IsRunning", movimento != Vector3.zero);// Ativa a anima��o de movimento se houver deslocamento
    }
}
