using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	public Text infoText;
	public SpriteRenderer player2;
	public SpriteRenderer hit;
	float size = 1.0f;
	Vector2 origin;
	Vector3 angles;
	float distance = 3.0f;

	void checkInput() {
		Vector3 pos = transform.position;
		angles = transform.eulerAngles;
		if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift)) {
			angles.z -= Input.GetAxisRaw("Horizontal") * Time.deltaTime * 50.0f;
		} else {
			pos.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * 5.0f;
			pos.y += Input.GetAxisRaw("Vertical") * Time.deltaTime * 5.0f;
		}
		transform.position = pos;
		origin = new Vector2(pos.x, pos.y);

		transform.eulerAngles = angles;
		player2.gameObject.transform.eulerAngles = angles;
		Vector3 player2Pos = gameObject.transform.localPosition;
		player2Pos.x += distance;
		player2.gameObject.transform.localPosition = player2Pos;
	}

	// Use this for initialization
	void Start () {
		checkInput();
	}

	// Update is called once per frame
	void Update () {
		checkInput();
		//public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = Mathf.Infinity, int layerMask = DefaultRaycastLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity); 
		RaycastHit2D ray = Physics2D.BoxCast(origin, new Vector2(size, size), angles.z, new Vector2(10f, 0), distance);
		if (ray.collider) {
			infoText.text = ray.collider.name;
			hit.enabled = true;
			Vector2 hitPos = ray.point;
			hit.gameObject.transform.position = new Vector3(hitPos.x, hitPos.y, 0);
		} else {
			infoText.text = "No data";
			hit.enabled = false;
		}
	}
}
