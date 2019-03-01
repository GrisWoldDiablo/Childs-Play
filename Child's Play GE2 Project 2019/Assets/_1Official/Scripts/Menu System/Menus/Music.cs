using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour {

    private AudioSource audioSource;

    public AudioSource AudioSource { get { return audioSource; } set { audioSource = value; } }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
