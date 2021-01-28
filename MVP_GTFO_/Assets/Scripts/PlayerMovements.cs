using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    
    [SerializeField]
    private float m_speed;

    private Vector3 m_hoverDir;

    private Rigidbody m_rigidbody;

    private bool m_hoverOn = false;
    
    


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        while (m_hoverOn == false)
        {
            Deplacement();
        }
        

        if (Input.GetButton("Hover"))
        {
            m_hoverOn = true;
            m_hoverDir = m_rigidbody.velocity;
            
        }
        
        
        
    }

    private void Deplacement()
    {
        
        float translationV = Input.GetAxis("Vertical") * m_speed * Time.deltaTime;
        float translationH = Input.GetAxis("Horizontal") * m_speed * Time.deltaTime;
        
        transform.Translate(0,0,translationV);
        transform.Translate(translationH,0,0);
    }
}
