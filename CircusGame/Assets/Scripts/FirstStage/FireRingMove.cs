using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRingMove : MonoBehaviour
{
    float playSpeed = -10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3을 초기화 한 후 입력된 수 만큼 가속도를 준다.
        Vector3 pos = new Vector3(Time.deltaTime * playSpeed, 0f,0f);
        transform.localPosition += pos;
    }
}
