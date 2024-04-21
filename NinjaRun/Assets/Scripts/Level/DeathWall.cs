using UnityEngine;

namespace Level
{
    public class DeathWall : MonoBehaviour
    {
        public static DeathWall deathWall;
        
        [SerializeField] private Transform player;
        [SerializeField] private float speed, pullEfect, length = 100f;
        private float startPosX, temp, distance;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            deathWall = this;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Use this for initialization
        void Start()
        {
            startPosX = transform.position.x;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (player == null)
                return;

            //Debug.Log(Vector2.Distance(player.transform.position, transform.position));
            distance = Vector2.Distance(player.position, transform.position);
            if (distance< 10)
            {
                _rigidbody2D.velocity = new Vector2(speed, 0);
            }
            else if (distance < 15)
            {
                _rigidbody2D.velocity = new Vector2(speed * 2, 0);
            }
            else if (distance < 20)
            {
                _rigidbody2D.velocity = new Vector2(speed * 3, 0);
            }
            else if (distance < 40)
            {
                _rigidbody2D.velocity = new Vector2(speed * 4, 0);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(speed * 10, 0);
            }

            //temp = (player.position.x * (1 - pullEfect));

            //distance = (player.position.x * pullEfect);

            //transform.position = new Vector3(startPosX + distance, transform.position.y, transform.position.z);

            //if (temp > startPosX + length)
            //{
            //    startPosX += length;
            //}
        }
        
    }
}