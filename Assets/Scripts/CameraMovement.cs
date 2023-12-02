using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
