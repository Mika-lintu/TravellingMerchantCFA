using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPosition : MonoBehaviour {

	void Update () {

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = newPosition;
	}
}
