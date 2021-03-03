using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{

    [SerializeField]
    float m_speed ;
    Vector3 m_initialPos;

    // Start is called before the first frame update
    void Start()
    {
        m_initialPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * m_speed, Camera.main.transform);
        
        if (transform.position.z < -10)
        {
            transform.position = m_initialPos;
        }



    }
}
