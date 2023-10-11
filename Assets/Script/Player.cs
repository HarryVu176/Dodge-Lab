using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float speed = 5f;

	public AudioSource shootSound;

	// Gun variables
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private Transform firingPoint;
	[Range(0.1f, 2f)]
	[SerializeField] private float fireRate = 0.5f;

    // Add a rapid fire rate
    [Range(0.05f, 1f)]
    [SerializeField] private float rapidFireRate = 0.1f; 

	private Rigidbody2D rb;
	private float mx;
	private float my;

	private float fireTimer;

	private Vector2 mousePos;

	private void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		mx = Input.GetAxisRaw("Horizontal");
		my = Input.GetAxisRaw("Vertical");
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

		transform.localRotation = Quaternion.Euler(0, 0, angle);

        // Checking right mouse button is being held down
        if (Input.GetMouseButton(1) && fireTimer <= 0f) {
			Shoot();
			fireTimer = rapidFireRate; // Set the fire timer to the rapid fire rate
		}
        else if (Input.GetMouseButton(0) && fireTimer <= 0f) {
			Shoot();
			fireTimer = fireRate;
		} else {
			fireTimer -= Time.deltaTime;
		}

		// if user press esc, back to main menu
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
		}
	}

	private void FixedUpdate() {
		rb.velocity = new Vector2(mx, my).normalized * speed;
	}

	private void Shoot() {
		Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
		shootSound.Play();
	}
}
