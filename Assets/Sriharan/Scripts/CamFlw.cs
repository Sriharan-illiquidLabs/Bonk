using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFlw : MonoBehaviour
{

    public Transform player;

    public Vector3 offset;
    public Vector3 turnOffset;
   
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(player.transform.position.x + offset.x,
            player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, pos, 5f * Time.deltaTime);

        Quaternion target = Quaternion
        .Euler(player.localEulerAngles.x +
        turnOffset.x, player.localEulerAngles.y,
        player.localEulerAngles.z);
        transform.rotation = Quaternion
        .Slerp(transform.rotation, target,
        10f *
        Time.deltaTime);
    }
}
