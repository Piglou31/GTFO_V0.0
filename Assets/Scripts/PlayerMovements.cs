using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    
    [SerializeField][Range(0,50)][Tooltip("Vitesse de déplacement en mètres par secondes")] private int m_acceleration;
    [SerializeField][Range(0,1000)][Tooltip("Vitesse de déplacement de base en mètres par secondes ")] private float m_basespeed;
    [SerializeField][Range(0,1)][Tooltip("Vitesse de décélération")] private float m_deceleration;
    [SerializeField][Range(0,1)][Tooltip("Vitesse de décélération")] private float m_maxSpeed;
    [SerializeField][Range(0,4)][Tooltip("Durée de hover en secondes")] private float m_hoverTime = 3f;
    private float m_hoverTimer = 0;

    private Vector3 m_hoverDir;

    private Rigidbody m_rigidbody;

    private bool m_hovering = false;

    private Material m_mat;
    
    [SerializeField][Tooltip("La couleur s'assumera le machin lorsqu'il truc")] private Color m_hoverColor;
    private Color m_defaultColor;
    
    


    // Start is called before the first frame update
    void Start()
    {
        m_mat = gameObject.GetComponent<Renderer>().material;

        m_defaultColor = m_mat.color;
        
        m_rigidbody = GetComponent<Rigidbody>();

        m_rigidbody.useGravity = true;

        StartCoroutine ("Accelerate");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hovering == false) Deplacement();
        
        if (Input.GetButton("Fire1") && m_hovering == false) HoverStart();

        if (m_hovering) Hover();

        //VelocityHandling();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Deplacement()
    {
        

        float forceV = Input.GetAxis("Vertical") * m_acceleration;
        float forceH = Input.GetAxis("Horizontal") * m_acceleration;
        
        Vector3 force = Vector3.forward * forceV +Vector3.right * forceH;
        
        m_rigidbody.AddForce(force * Time.deltaTime , ForceMode.Impulse);
    }

    /*private void VelocityHandling()
    {
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical") == 0)m_rigidbody.velocity = m_rigidbody.velocity * m_deceleration;
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Horizontal") == 0)m_rigidbody.velocity = m_rigidbody.velocity * m_deceleration;
    }*/
    
    /// <summary>
    /// 
    /// </summary>
    private void HoverStart()
    {
        m_mat.color = m_hoverColor;
        Debug.Log("Hover Start");
        m_hovering = true;

        m_rigidbody.useGravity = false;

        m_rigidbody.velocity = Vector3.zero;
        
        float translationV = Input.GetAxis("Vertical") * m_acceleration;
        float translationH = Input.GetAxis("Horizontal") * m_acceleration;
            
        m_hoverDir = new Vector3(translationH,0,translationV);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 
    /// </summary>
    private void Hover()
    {
        
        Debug.Log(m_hoverTimer);
        
        if (m_hoverTimer >= m_hoverTime) HoverEnd();
        
        m_rigidbody.AddForce(m_hoverDir * Time.deltaTime, ForceMode.Impulse);
        
        m_hoverTimer += Time.deltaTime;
    }

    /// <summary>
    /// 
    /// </summary>
    private void HoverEnd()
    {
        m_mat.color = m_defaultColor;
        Debug.Log("Hover End");
        m_hovering = false;

        m_rigidbody.useGravity = true;
        
        m_hoverDir = Vector3.zero;

        m_hoverTimer = 0;
    }


    IEnumerator Accelerate() 
    {
        while (m_acceleration < 60)
        {
        m_acceleration +=1;
        yield return new WaitForSeconds(1f);
        Debug.Log("truc");
        }
        m_acceleration = 1000;
    }
}