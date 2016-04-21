using UnityEngine;
using System.Collections;

public class AccessPrefabTest : MonoBehaviour {

	public GameObject prefab;
	int i = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (i == 0) {
			prefab.transform.GetChild (0).GetComponent<TextMesh> ().text = "bleh";
			prefab.transform.GetChild (1).GetComponent<TextMesh> ().text = "bleh2";
		}
		i++;
	}
}
