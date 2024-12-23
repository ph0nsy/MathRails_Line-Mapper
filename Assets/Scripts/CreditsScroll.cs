using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    private Vector3 target;
    public SceneTransition loader;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        target.y += 3200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 200*Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            StartCoroutine(loader.LoadLevel(0));
        }

    }
}
