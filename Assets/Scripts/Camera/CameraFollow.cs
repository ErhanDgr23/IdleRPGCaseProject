using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float smoothSpeed = 5f;

    private bool isFollowing = true;

    void Start()
    {
        // Oyuncu ölürse takibi bırakmak için event'e abone oluyoruz
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerHealth>().OnPlayerDeath += StopFollowing;
        }
    }

    void LateUpdate()
    {
        // Eğer takip durduysa veya hedef yoksa hiçbir şey yapma
        if (!isFollowing || target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }

    private void StopFollowing()
    {
        isFollowing = false;
        // İstersen burada kameranın o anki konumunda kalmasını sağlayabilirsin
    }
}