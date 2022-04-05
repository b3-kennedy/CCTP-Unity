using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTest : MonoBehaviour
{

    Material currentMat;

    // Start is called before the first frame update
    void Start()
    {
        currentMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentMat.SetInt("Boolean_b78a691b4160461ab7a23dcb12ddb6a3", 1);
            //Debug.Log(currentMat.GetFloat("Vector1_f9bcf94455744efba76af3fafa658f44"));
        }
    }
}
