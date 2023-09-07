using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _engineThrust = 10.0f;

	[SerializeField]
	private float _rotationSpeed = 20.0f;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip _rocketEngine;


	private Rigidbody _playerRb;



	// Start is called before the first frame update
	void Start() {

		_playerRb = GetComponent<Rigidbody>();

		_audioSource = GetComponent<AudioSource>();

		}

	// Update is called once per frame
	void Update() {
		ActivatesThrust();
		ActivateZAxisRotation();
		}

	void ActivatesThrust() {

		if ( Input.GetKey( KeyCode.Space ) ) {

			_playerRb.AddRelativeForce( _engineThrust * Time.deltaTime * Vector3.up );

			Debug.Log( "Space pressed!" );

			// check to see if audio clip is not playing
			if ( !_audioSource.isPlaying ) {

				_audioSource.PlayOneShot( _rocketEngine );
				}

			}

		else {
			_audioSource.Stop();

			}

		}

	// ship rotates round the Z axis on a or d key press
	void ActivateZAxisRotation() {

		if ( Input.GetKey( KeyCode.A ) ) {
			AppliesRotation( _rotationSpeed );

			Debug.Log( "Rotate left!" );

			}
		else if ( Input.GetKey( KeyCode.D ) ) {

			AppliesRotation( -_rotationSpeed );

			Debug.Log( "Rotate right" );

			}
		}

	void AppliesRotation( float rotationThisFrame ) {
		// freeze rotation so we can control manually
		_playerRb.freezeRotation = true;

		transform.Rotate( rotationThisFrame * Time.deltaTime * Vector3.forward );
		// Unfreeze rotation 
		_playerRb.freezeRotation = false;

		}
	}
