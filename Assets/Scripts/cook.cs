using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class cook : MonoBehaviour
{  
    private bool burning = false;
    public GameObject fire;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -0.5 && gameObject.transform.position.x > -1.25 && gameObject.transform.position.y < 1.5)
        {
            gameObject.GetComponent<Renderer>().material.color -= new Color(0.0005f, 0.0005f, 0.0005f, 0);
        }
        if (gameObject.GetComponent<Renderer>().material.color == Color.black && !burning)
        {
            burning = true;
            Instantiate(fire, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity).transform.parent = gameObject.transform;
        }
    }
}
