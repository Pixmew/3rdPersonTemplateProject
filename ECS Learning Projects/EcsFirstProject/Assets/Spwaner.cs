using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spwaner : MonoBehaviour
{

    public GameObject PrefabEntity;
    public GameObject prefabGameObject;
    public Vector3 offset;
    public TextMeshProUGUI text;
    private int count = 0;
    public bool UseECS;
    public bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        text.text = count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            StartCoroutine("spawn");
        }
        
    }

    IEnumerator spawn()
    {
        for (int i = 0; i < Random.Range(1, 20); i++)
        {
            if (UseECS)
            {
                Instantiate(PrefabEntity, transform.position + offset, transform.rotation);
            }
            else
            {
                Instantiate(prefabGameObject, transform.position + new Vector3(Random.Range(0f, offset.x), Random.Range(0f, offset.y), Random.Range(0f, offset.z)), transform.rotation);
            }
            count++;
            text.text = count.ToString();
        }
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
    }
}
