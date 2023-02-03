using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomFireRing : MonoBehaviour
{
    public GameObject randomRing = default;
    public GameObject playerPos = default;
    public GameObject parentObj = default;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(randomRingCreate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ������ �ֱ�
    IEnumerator randomRingCreate()
    {
        while (true)
        {
            // ���� ������ �ֱ�
            yield return new WaitForSeconds(Random.Range(0.1f, 2f));

            // �����ϱ�
           
            Instantiate(randomRing, Vector3.zero, Quaternion.identity, parentObj.transform).transform.localPosition = new Vector3(playerPos.transform.localPosition.x + 800f, -42f, 0f);

        }
    }
        
}
