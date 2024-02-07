using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    public int currentPos;
    public LineRender line;
    PauseMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = FindObjectOfType<PauseMenu>();
        line = FindObjectOfType<LineRender>();
        currentPos = line.playerscore;
    }

    // Update is called once per frame
    private void Update()
    {
        if ((line.playerscore - line.maxPlatforms + 1) > currentPos)
        {
            menu.masterSound.Remove(this.gameObject.GetComponentInChildren<BlockGeneration>().pointSound);
            StartCoroutine(Destroythis());
        }
    }

    IEnumerator Destroythis()
    {

        while (transform.position.y > -10f)
        {
            transform.position -= new Vector3(0f,0.01f,0f);

            yield return new WaitForFixedUpdate();
        }

        Destroy(this.gameObject);

        yield return null;
    }
}
