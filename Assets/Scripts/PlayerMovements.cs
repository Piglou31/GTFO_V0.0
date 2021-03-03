using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    
    [SerializeField][Range(0,20)][Tooltip("Vitesse de déplacement en mètres par secondes")] private int m_speed;
    [SerializeField][Range(0,20)][Tooltip("Vitesse de déplacement de base en mètres par secondes ")] private float m_basespeed;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (m_hovering == false) Deplacement();
        
        if (Input.GetButton("Fire1") && m_hovering == false) HoverStart();

        if (m_hovering) Hover();

    }

    /// <summary>
    /// 
    /// </summary>
    private void Deplacement()
    {
        

        float translationV = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
        float translationH = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        
        transform.Translate(Vector3.forward * translationV,Space.World);
        transform.Translate(Vector3.right * translationH,Space.World);

        StartCoroutine ("Accelerate");
    }
    
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
        
        float translationV = Input.GetAxis("Vertical") * m_speed;
        float translationH = Input.GetAxis("Horizontal") * m_speed;
            
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
        
        transform.Translate(m_hoverDir * Time.deltaTime, Space.World);
        
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
        if (m_speed < 50)
        {
        m_speed +=1;
        yield return new WaitForSeconds(1f);
        Debug.Log("truc");
        }
    }
}