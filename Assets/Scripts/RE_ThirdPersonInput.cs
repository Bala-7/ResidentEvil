using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class RE_ThirdPersonInput : ThirdPersonUserControl
{

    public enum CAMERA_MODE { BASE, AIM };

    #region Atributos
    // Públicos
    public Transform chest;     // El pecho del personaje. Se usa para rotar el personaje hacia la dirección en la que queremos apuntar.

    // Privados
    private MyTPCharacter tpc;          // Script asociado al modelo del robot. Uso esto solo para poder animarlo
    private Transform aimTarget;        // Objeto que situaremos en el centro de la pantalla al apuntar, y hacia el que mirará el personaje.
    private int aimDistance = 5;        // Distancia a la que situamos 'aimTarget' frente a nuestro personaje.
    private CinemachineFreeLook cfl;    // Este es el objeto que se crea en la escena al crear una cámara Cinemachine. Lo usamos para controlar la posición y movimiento de la cámara
    
    private Canvas canvas;              // Canvas que gestiona la interfaz (en este caso solo el cursor de apuntado)
    private Image aimCursor;            // Imagen del cursor de apuntado
    #endregion


    // La función Awake se llama antes que Start, y es donde se inicializan todos los parámetros de la clase
    private void Awake()
    {
        cfl = FindObjectOfType<CinemachineFreeLook>();
        tpc = FindObjectOfType<MyTPCharacter>();
        aimTarget = GameObject.FindGameObjectWithTag("AimTarget").transform;
        canvas = FindObjectOfType<Canvas>();
        aimCursor = canvas.transform.Find("AimCursor").gameObject.GetComponent<Image>();
    }

    // La función FixedUpdate se llama varias veces cada frame.
    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        bool walking = (m_Move.x != 0 || m_Move.z != 0);    // true si el personaje está moviéndose
        bool aimPressed = Input.GetKeyDown(KeyCode.Q);      // true en el frame en que se pulsa el botón de apuntar
        bool aiming = Input.GetKey(KeyCode.Q);              // true mientras el botón de apuntar esté pulsado
        
        
        tpc.GetAnimator().SetBool("IsWalking", walking);    // Se le dice al Animator que el personaje está andando    

        // Se coloca el 'aimTarget' en el centro de la cámara. Nuestro personaje mirará hacia él para apuntar
        aimTarget.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, aimDistance));


       
        if (aiming) // Si está apuntando
        {
            // -- Configuramos la camara al modo apuntado --
            SetCameraMode(CAMERA_MODE.AIM);

            // Paramos al personaje. El personaje no se mueve mientras apuntamos.
            m_Move = new Vector3(0, 0, 0);  

            // Rotamos al personaje para que apunte al 'aimTarget'. En concreto lo que apunta a 'aimTarget' es el pecho (chest) del personaje.
            Vector3 aimVector = aimTarget.position - chest.position;
            Quaternion rotation = Quaternion.LookRotation(aimVector, Vector3.up);
            transform.rotation = rotation;

        }
        else {  // Si no está apuntando

            // Reconfiguramos la camara al modo normal
            SetCameraMode(CAMERA_MODE.BASE);
        }

        // Llamamos al Move del padre para que mueva el GameObject
        m_Character.Move(m_Move, crouch, m_Jump);
        tpc.GetAnimator().SetBool("IsAiming", aiming);
        aimCursor.gameObject.SetActive(aiming);
        m_Jump = false;
    }


    /**
     *  Esta función cambia la configuración de la cámara dependiendo de si estamos en el modo de apuntado o en el modo normal
     */
    private void SetCameraMode(CAMERA_MODE mode) {

        switch (mode) {
            case CAMERA_MODE.BASE: // Esta será la configuración de la cámara cuando el personaje está andando
                cfl.m_Orbits[0].m_Height = 4.5f;
                cfl.m_Orbits[0].m_Radius = 1.75f;

                cfl.m_Orbits[1].m_Radius = 2.0f;

                cfl.m_Orbits[2].m_Height = 0.65f;

                cfl.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.3f;
                cfl.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.3f;
                cfl.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.3f;
                break;
            case CAMERA_MODE.AIM:   // Esta será la configuración de la cámara en el modo de apuntado
                // Top Ring
                cfl.m_Orbits[0].m_Height = 2.0f;
                cfl.m_Orbits[0].m_Radius = 1.0f;

                // Middle Ring
                cfl.m_Orbits[1].m_Radius = 1.3f;

                // Bottom Ring
                cfl.m_Orbits[2].m_Height = 1.2f;

                cfl.GetRig(0).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.2f;
                cfl.GetRig(1).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.2f;
                cfl.GetRig(2).GetCinemachineComponent<CinemachineComposer>().m_ScreenX = 0.2f;

                break;
            default: break;
        }
    
    }

}



