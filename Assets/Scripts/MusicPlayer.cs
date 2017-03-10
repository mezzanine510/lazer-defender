using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;

	public AudioClip startClip;
	public AudioClip gameClip;

	private static AudioSource music;

	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
			music.Play();
		}
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 0 || level == 2)
		{
			music.clip = startClip;
		}

		if (level == 1)
		{
			music.clip = gameClip;
		}

		music.loop = true;

		if (level != 0)
		{
			music.Stop();
			music.Play();
		}
	}

}
