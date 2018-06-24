using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

    private float fallDealy = 1.5f;

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {   
            TileManager.Instance.SpawnTile();

            StartCoroutine(FallDown());
        }
    }

    IEnumerator FallDown()
    {   
        yield return new WaitForSeconds(fallDealy);

        GetComponent<Rigidbody>().isKinematic = false;

        yield return new WaitForSeconds(2);

        switch (gameObject.name)
        {
            case "LeftTile":
                TileManager.Instance.LeftTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;

            case "TopTile":
                TileManager.Instance.TopTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }


    }
}
