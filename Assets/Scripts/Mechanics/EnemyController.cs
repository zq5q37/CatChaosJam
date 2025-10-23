using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Enemy that the player can walk behind but interact with when nearby.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyController : MonoBehaviour
    {
        internal BoxCollider2D _collider;
        internal AudioSource _audio;
        private SpriteRenderer _spriteRenderer;

        private bool playerNearby = false;
        private PlayerController player; // reference to player when in range

        public AudioClip ouch; // optional audio when interacting

        void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true; // ensure it’s a trigger
            _audio = GetComponent<AudioSource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                playerNearby = true;
                Debug.Log("Player entered enemy trigger area.");
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            var exitingPlayer = collision.GetComponent<PlayerController>();
            if (exitingPlayer != null)
            {
                playerNearby = false;
                player = null;
                Debug.Log("Player exited enemy trigger area.");
            }
        }

        void Update()
        {
            if (playerNearby && Input.GetKeyDown(KeyCode.K))
            {
                Interact();
            }
        }

        void Interact()
        {
            Debug.Log("Player interacted with enemy!");
            if (_audio && ouch) _audio.PlayOneShot(ouch);
            // add your interaction logic here, e.g., reduce health, knockback, dialogue, etc.
        }
    }
}
