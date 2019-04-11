using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ColorSwitch
{
    public class Player : MonoBehaviour
    {
        public float jumpForce = 10f;
        public int score = 0;

        public TextMesh Scorer;

        public Rigidbody2D rigid;
        public SpriteRenderer rend;

        public Color[] colors = new Color[4];

        public UnityEvent onGameOver;

        private Color currentColor;

        private Color oldColor;
        public Color a;


        void Start()
        {
            RandomizeColor();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
            {
                rigid.velocity = Vector2.up * jumpForce;
            }

            if (score >= 3)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(1);
            }

        }

        void OnTriggerEnter2D(Collider2D col)
        {
           

            if (col.name == "ColorChanger")
            {
                
                RandomizeColor();
                Destroy(col.gameObject);
                return;
            }

            if (col.name == "Star")
            {
                // Add score
                Destroy(col.gameObject);
                score++;

                Scorer.text = "Score: " + score.ToString();

                return;
            }

            SpriteRenderer spriteRend = col.GetComponent<SpriteRenderer>();
            if (spriteRend != null &&
               spriteRend.color != rend.color)
            {
                Debug.Log("GAME OVER!");
                onGameOver.Invoke();

                    Destroy(this.gameObject);
                    Debug.Log("dead");

                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.buildIndex);

            }
        }

        void RandomizeColor()
        {

            currentColor = this.rend.color;

            int index = Random.Range(0, 4);

            rend.color = colors[index];


        }
    }
}