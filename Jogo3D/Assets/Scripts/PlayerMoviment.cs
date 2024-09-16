using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller; // Componente para controlar o movimento do personagem
    private Transform myCamera; // Referência à transformação da câmera principal

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
         float horizontal = Input.GetAxis("Horizontal"); // Obtém o input horizontal (A, D, esquerda, direita)
        float vertical = Input.GetAxis("Vertical"); // Obtém o input vertical (W, S, cima, baixo)
        Vector3 movimento = new Vector3(horizontal, 0, vertical); // Cria um vetor de movimento baseado nos inputs

        movimento = myCamera.TransformDirection(movimento); // Ajusta o vetor de movimento para corresponder à orientação da câmera
        movimento.y = 0; // Garante que não há movimento no eixo Y (vertical)

        controller.Move(movimento * Time.deltaTime * 3f); // Aplica o movimento ao personagem
        controller.Move(new Vector3(0, -9.81f, 0) * Time.deltaTime); // Aplica gravidade ao personagem

        // Rotação do personagem para enfrentar a direção do movimento
        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("IsRunning", movimento != Vector3.zero);// Ativa a animação de movimento se houver deslocamento
    }
}
