using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1.0f;

    [Range(0.0f, 0.5f)]
    public float randomVolume = 0.1f; //multiplier
    [Range(0.0f, 0.5f)]
    public float randomPitch = 0.1f; //multiplier

    public bool looping = false;

    public AudioSource source; //had to make public so the manager can tell if the source is playing its clip presently.

    public void SetSource(AudioSource _source) {
        source = _source;
        source.clip = clip;
    }

    public void Play() {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f)) * AudioManager.instance.volumeMult;
        //Debug.Log(AudioManager.instance.volumeMult + " & " + source.volume + source.name);
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.loop = looping;
        source.Play();
    }

    public void Stop() {
        source.Stop();
    }
}

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public int sparesAvailable = 10;
    private int nextSpare = 1;
    public float volumeMult = 1.0f;

    public List<AudioSource> spareSources = new List<AudioSource>();

    [SerializeField]
    Sound[] sounds;



    void Awake() {
        if (instance != null) {
        }
        else {
            instance = this;
        }
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < sparesAvailable; i++) {
            MakeSpareSource(i);
        }
        volumeMult = Global.volumeMult;

		for (int i = 0; i < sounds.Length; i++) {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>()); //black magic to save audio sources from the garbage collector.
        }

	}

    private void MakeSpareSource(int i) {
        GameObject _go = new GameObject("SpareSource_" + i);
        _go.transform.SetParent(this.transform);
        _go.AddComponent<AudioSource>();
        spareSources.Add(_go.GetComponent<AudioSource>());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayLoopedSound(string _name) {
        //Looped sounds can only play in their personal AudioSource object.
        //Send the order to Play the looped sound repeatedly.
        //This function simply checks if the chosen sound is playing, and if it isn't, starts it.
        for (int i = 0; i < sounds.Length; i++) {
            if (sounds[i].name == _name) {
                if (!sounds[i].source.isPlaying) { //if it's not already playing.
                    //Debug.Log("Starting a looping sound");
                    sounds[i].Play();//Let the music play!
                    return;//we're done, return
                }
            }
        }
         //Debug.Log("Found the sound already playing, or didn't find the sound name.");
        /*I'm a tad concerned for performance; every time you have a script constantly calling PlayLoopedSound(), it
        iterates through the list checking strings quite a lot. Whether this causes issues is tbd.
        If so, the fix is to either somehow stagger this function over here.
        Or far more easily, only call the function every .5 or 1.0 seconds from the script/object that wants to loop the sound.
        */
    }

    public void PlaySound(string _name) {
        for(int i = 0; i < sounds.Length; i++) {
            if(sounds[i].name == _name) {
                if (!sounds[i].source.isPlaying) { //if it's not already playing.
                    sounds[i].Play();//Let the music play!
                    return;//we're done, return
                }
                else {//if there's already a version of that sound playing...
                    //make a temporary extra sound source
                    Sound currentSound = sounds[i];
                    AudioSource currentSpare = spareSources[nextSpare - 1];

                    if(currentSpare.isPlaying == true) {//if the SPARE is already in use...
                                                        //find a spare that isn't playing.
                        AudioSource tempAS = spareSources.Find(x => x.isPlaying == false); //find a spareSource not in use.
                        if (tempAS != null) {//if you did...
                            currentSpare = tempAS;//set the currentSpare to the one you found.
                        }
                    }//hopefully, if you didn't find any spare sources, you'll just overwrite the one you were about to overwrite anyway.

                    currentSpare.clip = currentSound.clip; //sets spareSource clip to overlapping clip
                    currentSpare.volume = currentSound.volume * (1 + Random.Range(-currentSound.randomVolume / 2f, currentSound.randomVolume / 2f)) * AudioManager.instance.volumeMult; //sets spareSource volume with random formula (based on clip)
                    currentSpare.pitch = currentSound.pitch * (1 + Random.Range(-currentSound.randomPitch / 2f, currentSound.randomPitch / 2f)); ; //sets spareSource pitch with random formula (based on clip)
                    currentSpare.Play(); //nearly forgot to play the newely changed AudioSource.
                    //DONT USE LOOPING SOUNDS FOR SPARES.
                    
                    nextSpare++;
                    if(nextSpare > sparesAvailable) {
                        nextSpare = 1;
                    }
                }
            }
        }
        //no sound with _name, sheeeeet.
        Debug.LogWarning("AudioManager; Sound not found in list, missing - " + _name);
    }

    public void StopSound(string _name) {
        for (int i = 0; i < sounds.Length; i++) {
            if(sounds[i].name == _name) {
                sounds[i].Stop();
            }
        }
    }
}
