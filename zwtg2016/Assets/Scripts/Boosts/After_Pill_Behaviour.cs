using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class After_Pill_Behaviour : MonoBehaviour {

	AudioSource[] audioSource;
	BlurOptimized  myBlur;

	public AudioClip mainAudio;
	public AudioClip amphAudio;
	public AudioClip ecstasyAudio;
	public AudioClip lsdAudio;
	public AudioClip pcpAudio;
	public AudioClip shroomsAudio;

	public GameObject HealingAura;
	public BoxCollider floorBoxCollider;
	public MeshCollider hospitalMeshCollider;
	FirstPersonController fpc;
	GameObject child;

	float standardDuration = 15.0f;
	float prolongedDuration = 20.0f;
	float timeLeftAmp, timeLeftMeth, timeLeftLSD, timeLeftPCP, timeLeftShrooms;

	float hp;

	bool startAmphetamineEffect = false;
	bool isAmphetamineEffect = false;
	float walkSpeed_before;
	float runSpeed_before;
	float walkSpeed;
	float runSpeed;

	bool startEcstasyEffect = false;
	bool isEcstasyEffect = false;
	float jumpSpeed_before;

	bool startLSDEffect = false;
	//bool isLSDEffect = false;

	float hp_before;
	bool startPCPEffect = false;
	//bool isPCPEffect = false;

	bool startShroomEffect = false;


	void Start () {
		timeLeftAmp = timeLeftMeth = timeLeftLSD = timeLeftPCP = standardDuration;
		timeLeftShrooms = prolongedDuration;

		fpc = GameObject.FindObjectOfType<FirstPersonController> (); 
		hp_before = GetComponent<Health> ().currentHitPoints;

		walkSpeed_before = fpc.m_WalkSpeed;
		runSpeed_before = fpc.m_RunSpeed;

		jumpSpeed_before = fpc.m_JumpSpeed;

		hospitalMeshCollider = GameObject.FindObjectOfType<MeshCollider> ();
		floorBoxCollider = GameObject.FindObjectOfType<BoxCollider> ();

		child = this.transform.Find ("FirstPersonCharacter").gameObject;

		audioSource = child.GetComponents<AudioSource> ();

		audioSource [0].clip = mainAudio;
		audioSource [1].clip = amphAudio;
		audioSource [2].clip = ecstasyAudio;
		audioSource [3].clip = lsdAudio;
		audioSource [4].clip = pcpAudio;
		audioSource [5].clip = shroomsAudio;

		myBlur = child.GetComponent<BlurOptimized> ();
		myBlur.enabled = false;

        setRainbow(false);
    }

    private void setRainbow(bool on)
    {
        Shader shader = on ? Shader.Find("_Shaders/Rainbow") : Shader.Find("Standard");

        GameObject walls = GameObject.Find("default");
        Material[] materials = walls.GetComponent<Renderer>().sharedMaterials;
        for (int i = 0; i < materials.Length; ++i)
        {
            materials[i].shader = shader;
        }
    }

    void Update () {

		// Boosts time effects
		// 1. amphetamine
		if (startAmphetamineEffect == true) {
			
			timeLeftAmp -= Time.deltaTime;

			if (timeLeftAmp < 0) {
				startAmphetamineEffect = false;
				isAmphetamineEffect = false;
				fpc.m_WalkSpeed = walkSpeed_before;
				fpc.m_RunSpeed = runSpeed_before;
				timeLeftAmp = standardDuration;

				audioSource[1].mute = true;
				if ( (audioSource [2].mute == true && audioSource [3].mute == true) && 
					(audioSource [4].mute == true && audioSource [5].mute == true) ) {
					audioSource[0].mute = false;
					audioSource [0].Play ();
				}

			}
		}   

		// 2 (3). ecstasy
		if (startEcstasyEffect == true) {

			timeLeftMeth -= Time.deltaTime;

			if (timeLeftMeth < 0) {
				startEcstasyEffect = false;
				isEcstasyEffect = false;
				fpc.m_JumpSpeed = jumpSpeed_before;
				timeLeftMeth = standardDuration;

				audioSource[2].mute = true;
				if ( (audioSource [1].mute == true && audioSource [3].mute == true) &&
					(audioSource [4].mute == true && audioSource [5].mute == true) ) {
					audioSource[0].mute = false;
					audioSource [0].Play ();
				}
			}
		} 

		// 3 (4). LSD
		if (startLSDEffect == true) {

			timeLeftLSD -= Time.deltaTime;

			if(timeLeftLSD < 0) {
				startLSDEffect = false;
				//isLSDEffect = false;
				hospitalMeshCollider.enabled = true;
				floorBoxCollider.enabled = false;
				timeLeftLSD = standardDuration;

				audioSource[3].mute = true;
				if ( (audioSource [1].mute == true && audioSource [2].mute == true) &&
					(audioSource [4].mute == true && audioSource [5].mute == true) ){
					audioSource[0].mute = false;
					audioSource [0].Play ();
				}

                setRainbow(false);
            }
		} 

		// 4 (5). PCP
		if (startPCPEffect == true) {

			timeLeftPCP -= Time.deltaTime;

			if(timeLeftPCP < 0) {
				startPCPEffect = false;
				//isPCPEffect = false;
				hp = GetComponent<Health> ().currentHitPoints;
				hp = hp_before;
				timeLeftPCP = standardDuration;

				audioSource[4].mute = true;
				if ( (audioSource [1].mute == true && audioSource [2].mute == true) &&
					(audioSource [3].mute == true && audioSource [5].mute == true) ){
					audioSource[0].mute = false;
					audioSource [0].Play ();
				}
			}
		} 

		// 5 (6). Shrooms
		if (startShroomEffect == true) {

			timeLeftShrooms -= Time.deltaTime;

			if (timeLeftShrooms < 0) {
				startShroomEffect = false;
				//isShroomEffect = false;
				myBlur.enabled = false;
				timeLeftShrooms = prolongedDuration;

				audioSource[5].mute = true;
				if ( (audioSource [1].mute == true && audioSource [2].mute == true) &&
					(audioSource [3].mute == true && audioSource [4].mute == true) ){
					audioSource[0].mute = false;
					audioSource [0].Play ();
				}
			}
		}
	}


	// Boost after pill pickup
	void OnTriggerEnter(Collider other) {

		// 1. amphetamine - double walking and running speed
		if (other.tag == "amphetamine_pill") {
			Destroy (other.gameObject);
			timeLeftAmp = standardDuration;

			if (startShroomEffect == false) {

				startAmphetamineEffect = true;
				if (startAmphetamineEffect == true && isAmphetamineEffect == false) {
					fpc.m_WalkSpeed = walkSpeed_before * 2;
					fpc.m_RunSpeed = runSpeed_before * 2;
					isAmphetamineEffect = true;

					audioSource [0].mute = true;
					audioSource [1].mute = false;
					audioSource [2].mute = true;
					audioSource [3].mute = true;
					audioSource [4].mute = true;
					audioSource [5].mute = true;
					audioSource [1].Play ();
				}    
			}
		}
			
		// 2. methamphetamine - healing, plus 1/4 of max hp
		else if (other.tag == "methamphetamine_pill") {
			Destroy (other.gameObject);

			if (startShroomEffect == false) {
				hp = GetComponent<Health> ().currentHitPoints;

				hp += 25;
				if (hp > 100)
					hp = 100;
				else
					Instantiate (HealingAura, transform.position, transform.rotation);
			}
		}	  

		// 3. ecstasy - double jumping height
		else if (other.tag == "ecstasy_pill") {
			Destroy (other.gameObject);
			timeLeftMeth = standardDuration;

			if (startShroomEffect == false) {

				startEcstasyEffect = true;
				if (startEcstasyEffect == true && isEcstasyEffect == false) {
					fpc.m_JumpSpeed = jumpSpeed_before * 2;
					isEcstasyEffect = true;

					audioSource [0].mute = true;
					audioSource [1].mute = true;
					audioSource [2].mute = false;
					audioSource [3].mute = true;
					audioSource [4].mute = true;
					audioSource [5].mute = true;
					audioSource [2].Play ();
				}   
			}
		}

		// 4. LSD - walking through walls
		else if (other.tag == "LSD_pill") {
			Destroy (other.gameObject);
			timeLeftLSD = standardDuration;

			if (startShroomEffect == false) {

				startLSDEffect = true;
				if (startLSDEffect == true) { // && isLSDEffect == false) {
					hospitalMeshCollider.enabled = false;
					floorBoxCollider.enabled = true;
					//isLSDEffect = true;

					audioSource [0].mute = true;
					audioSource [1].mute = true;
					audioSource [2].mute = true;
					audioSource [3].mute = false;
					audioSource [4].mute = true;
					audioSource [5].mute = true;
					audioSource [3].Play ();

                    setRainbow(true);
                }
            }
		}

		// 5. PCP - berserker mode
		else if (other.tag == "PCP_pill") {
			Destroy (other.gameObject);
			timeLeftPCP = standardDuration;

			if (startShroomEffect == false) {

				startPCPEffect = true;
				if (startPCPEffect == true) {
					hp = GetComponent<Health> ().currentHitPoints;

					if (hp <= 0)
						hp += 100;

					audioSource [0].mute = true;
					audioSource [1].mute = true;
					audioSource [2].mute = true;
					audioSource [3].mute = true;
					audioSource [4].mute = false;
					audioSource [5].mute = true;
					audioSource [4].Play ();
				}
			}
		}

		// 6. Shroom - baked mode, 'Dude, you've been unlucky'
		else if (other.tag == "shroom_pill") {
			Destroy (other.gameObject);
			timeLeftShrooms = prolongedDuration;

			startShroomEffect = true;
			if (startShroomEffect == true) {
				myBlur.enabled = true;
				audioSource [0].mute = true;
				audioSource [1].mute = true;
				audioSource [2].mute = true;
				audioSource [3].mute = true;
				audioSource [4].mute = true;
				audioSource [5].mute = false;
				audioSource [5].Play ();
			}

		}

	}
}