using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _powerUpFallingSpeed = 4.0f;
    private Player player;
    [SerializeField] private int _powerUpID; //0 - tripple shot, 1 - speed up, 2 - shield;
    [SerializeField] private AudioClip _powerUpPickUpAudio;
    private Vector3 _cameraPosition;


    void Start()
    {
        _cameraPosition = new Vector3(0, 1, -10);
        player = GameObject.Find("Player").GetComponent<Player>();

        if (player == null)
        {
            Debug.LogError("player is null. PowerUp script");
        }
    }

    void Update()
    {
        MovementCalculation();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            switch(_powerUpID)
            {
                case 0:
                    player.TrippleShotActive();
                    break;
                case 1:
                    player.SpeeedUpActive();
                    break;
                case 2:
                    player.ShieldActive();
                    break;
                default:
                    Debug.Log("Invalid selection");
                    break;
            }
            AudioSource.PlayClipAtPoint(_powerUpPickUpAudio, _cameraPosition);
            Destroy(this.gameObject);
        }
    }
    void MovementCalculation()
    {
        transform.Translate(Vector3.down * _powerUpFallingSpeed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }
}
