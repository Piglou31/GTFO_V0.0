using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD2;
public class GameManagerSingleton : Singleton<GameManagerSingleton>
{
    public float m_scrollSpeed = 0;

    public Camera m_camera = null;

    public float m_playerSpeed = 0;

    public Transform m_playerTransform = null;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.name = "GameManager";
        
        m_camera = FindObjectOfType<Camera>();
        m_camera.name = "GameCamera";
    }
}
