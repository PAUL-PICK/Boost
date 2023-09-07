using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

	[SerializeField]
	private float _levelLoadDelay = 2.0f;

	[SerializeField]
	private AudioClip _crash;

	[SerializeField]
	private AudioClip _win;

	private AudioSource _audioSource;

	private bool _isTransitioning = false;

	private void Start() {

		_audioSource = GetComponent<AudioSource>();

		}





	private void OnCollisionEnter( Collision collision ) {
		// if transitioning "return" don't run the switch block
		if ( _isTransitioning ) { return; }


		switch ( collision.gameObject.tag ) {

			case "Friendly":

				Debug.Log( "You Hit A Friendly Object" );

				break;

			case "Finish":

				StartWinSequence();

				break;

			default:

				StartsCrashSequence();

				break;
			}

		}

	void StartWinSequence() {

		_audioSource.Stop();

		_audioSource.PlayOneShot( _win );

		// disables player movement
		GetComponent<PlayerMovement>().enabled = false;

		// calls the load next level method after stated delay
		Invoke( "LoadNextLevel", _levelLoadDelay );

		// To Do Add Particles On Win

		}

	void StartsCrashSequence() {

		// To Do Add Particles On Crash
		_isTransitioning = true;

		_audioSource.Stop();

		_audioSource.PlayOneShot( _crash );

		GetComponent<PlayerMovement>().enabled = false;
		// using invoke to call the method allows us to put a time delay
		Invoke( "ReloadsLevel", _levelLoadDelay );

		}

	void ReloadsLevel() {

		_isTransitioning = true;

		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		Invoke( "StartsCrashSequence", _levelLoadDelay );

		// loads the scene we are on
		SceneManager.LoadScene( currentSceneIndex );

		// calls the method after stated delay


		}

	void LoadNextLevel() {
		// gets the index of this scene and stores it in a local variable
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		//adds 1 to this scenes current index
		int nextSceneIndex = currentSceneIndex + 1;

		// checks if nextSceneIndex is the same as the total amount of scenes in build settings
		if ( nextSceneIndex == SceneManager.sceneCountInBuildSettings ) {

			//and if it is sets the scene index back to first scene 0
			nextSceneIndex = 0;

			}

		// load scene
		SceneManager.LoadScene( nextSceneIndex );

		}


	}
