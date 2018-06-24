using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatText : MonoBehaviour {

    private float speed;

    private Vector3 direction = Vector3.zero;

    private float fadeTime;

    public AnimationClip animation;

    private bool stay = true;
   
    void Update()
    {
        if (!stay) 
        {
            float translation = speed * Time.deltaTime;

            transform.Translate(direction * translation);
        }
    }

    public void Start()
    {
        transform.LookAt(2 * transform.position - CombatTextManager.Instance.camTransform.position);
    }

    public void Initialize(float speed, Vector3 direction, float fadeTime, bool critical)
    {
        this.speed = speed;
        this.direction = direction;
        this.fadeTime = fadeTime;
        stay = critical;

        if (critical)
        {
            GetComponent<Animator>().SetTrigger("Critical");
            StartCoroutine(Critical()); 
        }
        else 
        {
            StartCoroutine(FadeOut()); 
        }
    }

    private IEnumerator Critical()
    {   
       yield return new WaitForSeconds(animation.length);

stay = false;

        StartCoroutine(FadeOut());
    }
    

    private IEnumerator FadeOut()
    {
        float startAlpha = GetComponent<Text>().color.a;

        float rate = 1.0f / fadeTime; 

        float progress = 0.0f; 


        while (progress < 1.0) 
        {
            Color tmpColor = GetComponent<Text>().color;

            GetComponent<Text>().color = new Color(tmpColor.r,tmpColor.g,tmpColor.b,Mathf.Lerp(startAlpha, 0, progress)); 

            progress += rate * Time.deltaTime; 

            yield return null;
        }

       
        Destroy(gameObject);

    }
}
