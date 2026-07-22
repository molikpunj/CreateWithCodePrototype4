using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool hasPowerup;
    private InputSystem_Actions control;
    private Rigidbody rb;
    private GameObject focalPoint;
    public float moveForce;
    public float powerUpForce;
    public GameObject powerUpRing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        control = new InputSystem_Actions();
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        // powerUpRing = GameObject.Find("PowerUpIndicator");
    }

    private void OnEnable()
    {
        control.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = control.Player.Move.ReadValue<Vector2>();
        rb.AddForce(focalPoint.transform.forward * moveInput.y * moveForce * Time.deltaTime);
        powerUpRing.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp") && !hasPowerup)
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerUpRing.SetActive(true);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerUpRing.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRB = collision.collider.gameObject.GetComponent<Rigidbody>();
            Vector3 directionForce = collision.transform.position - transform.position;
            enemyRB.AddForce(directionForce * powerUpForce, ForceMode.Impulse);
        }
    }
}
