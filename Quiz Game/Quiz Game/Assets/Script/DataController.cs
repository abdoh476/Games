using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

    public roundData[] allRoundData;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("MenuScreen");
	}
	public roundData GetCurrentRoundData()
    {
        return allRoundData[0];//to be expanded later but now it's zero
    }
	// Update is called once per frame
	void Update () {
		
	}
}
