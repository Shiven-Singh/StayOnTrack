using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CombatTextManager : MonoBehaviour {

    private static CombatTextManager instance;

    public float speed;

    public Vector3 direction;

    public GameObject textPrefab;

    public RectTransform canvasTransform;

    public float fadeTime;

    public Transform camTransform;

    public static CombatTextManager Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return CombatTextManager.instance; 
        }
    }


    public void CreateText(Vector3 position, string text, Color color, bool critical)
    {
        GameObject sct = (GameObject)Instantiate(textPrefab, position, Quaternion.identity);

        sct.transform.SetParent(canvasTransform);

        sct.GetComponent<RectTransform>().localScale = new Vector3(.1f, .1f, .1f);

        sct.GetComponent<CombatText>().Initialize(speed, direction,fadeTime, critical);

        sct.GetComponent<Text>().text = text;

        sct.GetComponent<Text>().color = color;
    }
}
