using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    public float speed;

    private Vector3 dir;

    public GameObject ps;

    private bool isDead;

    public GameObject resetBtn;

    private int score = 0;
    
    public Text scoreText;

    public Animator gameOverAnim;

    public Text newHighScore;

    public Image background;

    public Text[] scoreTexts;

    public Transform contactPoint;

    public LayerMask whatIsGround;

    public bool playing = false;

	void Start () 
    {   
        isDead = false;

        dir = Vector3.zero;
    }
	
	void Update () 
    {
        if (!IsGrounded() && playing)
        {

            isDead = true; 

            GameOver();

            resetBtn.SetActive(true); 

            if (transform.childCount > 0) 
            {
                transform.GetChild(0).transform.parent = null;
            }
        }

        if (Input.GetMouseButtonDown(0) && !isDead)
        {
            playing = true;
            score++;
            scoreText.text = score.ToString();

            if (dir == Vector3.forward)
            {
                dir = Vector3.left;
            }
            else
            {
                dir = Vector3.forward;
            }
        }

        float amoutToMove = speed * Time.deltaTime;

        transform.Translate(dir * amoutToMove);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup") 
        {
            other.gameObject.SetActive(false); 
            Instantiate(ps, transform.position, Quaternion.identity); 
            score+= 3;
            scoreText.text = score.ToString();
            CombatTextManager.Instance.CreateText(other.transform.position, "+3", new Color32(255, 4, 238, 255), false);
        }
    }

    void OnTriggerExit(Collider other)
    {
   }

    private void GameOver()
    {
        gameOverAnim.SetTrigger("GameOver");

        scoreTexts[1].text = score.ToString();

        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", score);

            newHighScore.gameObject.SetActive(true);

            background.color = new Color32(255, 118, 246, 255);

            foreach (Text txt in scoreTexts)
            {
                txt.color = Color.white;
            }

        }

        scoreTexts[3].text = PlayerPrefs.GetInt("BestScore",0).ToString();
        
    }

    private bool IsGrounded()
    {
        Collider[] colliders = Physics.OverlapSphere(contactPoint.position,.5f, whatIsGround);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }


        return false;
    }
}
