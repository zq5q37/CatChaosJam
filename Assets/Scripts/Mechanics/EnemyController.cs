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

            // Play sound
            if (_audio && ouch)
                _audio.PlayOneShot(ouch);

            // Make enemy fall
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb == null)
                rb = gameObject.AddComponent<Rigidbody2D>(); // add Rigidbody if missing
            rb.bodyType = RigidbodyType2D.Dynamic; // enable physics
            rb.gravityScale = 3f; // fall faster
            rb.constraints = RigidbodyConstraints2D.None; // allow rotation

            // Disable collider so it doesn't block the player
            if (_collider != null)
                _collider.enabled = false;

            // Optional: destroy after 3 seconds
            Destroy(gameObject, 3f);
        }
    }
}
