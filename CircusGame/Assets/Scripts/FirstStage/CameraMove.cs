using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 cameraPostion = new Vector3(0, 0, -100);
    
    public RectTransform playTransform = default;
    // ī�޶� �÷��̾ ���󰡾ߵȴ�.

    // Start is called before the first frame update
    void Start()
    {
        //playTransform = gameObject.cameraPosition.GetComponentMust<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // ī�޶� �̵� 
        cameraPostion.x = Mathf.Clamp(playTransform.localPosition.x, 0f, 11520f);
        transform.localPosition = cameraPostion;
    }
}
