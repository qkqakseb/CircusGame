using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 camerPostion = new Vector3(0, 0, -100);
    
    private float playTransform = default;
    // ī�޶� �÷��̾ ���󰡾ߵȴ�.

    // Start is called before the first frame update
    void Start()
    {
        playTransform = gameObject.GetComponentMust<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
