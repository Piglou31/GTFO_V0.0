using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class CameraBorderBlock : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int m_borderWidthX;
    
    private Camera m_gameCam;
    // Start is called before the first frame update
    void Start()
    {
        m_gameCam = GameManagerSingleton.Instance.m_camera;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_gameCam.WorldToViewportPoint(transform.position);
        // Debug.Log(pos.x);
        
        pos.x = Mathf.Clamp01(pos.x);
        
        // Debug.Log(pos.x);
        transform.position = m_gameCam.ViewportToWorldPoint(new Vector3(pos.x,pos.y,pos.z));
    }
}
