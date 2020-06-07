using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenLogic : MonoBehaviour
{
    int nextStep = 60;
    public Transform startPos;
    [SerializeField]
    GameObject road;

    // Update is called once per frame
    void Update()
    {
        Instantiate(road, new Vector3(startPos.position.x, startPos.position.y, startPos.position.z + nextStep), Quaternion.identity);
        nextStep += 60;
    }
}
