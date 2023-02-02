using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Vector3 cameraPostion = new Vector3(0, 0, -100);
    
    public RectTransform playTransform = default;
    // 카메라가 플레이어를 따라가야된다.

    // Start is called before the first frame update
    void Start()
    {
        //playTransform = gameObject.cameraPosition.GetComponentMust<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // 카메라 이동 
        cameraPostion.x = Mathf.Clamp(playTransform.localPosition.x, 0f, 11520f);
        transform.localPosition = cameraPostion;
    }
}
