using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeneration : MonoBehaviour
{
    public BlockGenParameters parameters;
    public Material PlatformSymbolism;
    private Vector3 blockOffset;

    public AudioSource pointSound;
    int blockColourIndex;
    public Color blockColour;

    private bool active;
    public LineRender line;
    PauseMenu menu;

    // Start is called before the first frame update
    void Start()
    {
        PlatformSymbolism = this.GetComponent<MeshRenderer>().materials[1];
        pointSound = this.GetComponent<AudioSource>();
        line = FindObjectOfType<LineRender>();
        parameters = FindObjectOfType<BlockGenParameters>();

        menu = FindObjectOfType<PauseMenu>();
        menu.masterSound.Add(pointSound);
        this.gameObject.transform.parent.name = "Block_" + line.playerscore;
        pointSound.volume = menu.volumeSlider.value;

        active = true;

        blockColourIndex = Random.Range(0, parameters.Playercolours.Count);
        blockColour = parameters.Playercolours[blockColourIndex] * parameters.blockIntensity;
        PlatformSymbolism.color = parameters.Playercolours[blockColourIndex];
        PlatformSymbolism.SetColor("_EmissionColor", blockColour);
    }


    void OnTriggerEnter(Collider other)
    {
        // If a GameObject has an "Enemy" tag, remove him. 
        if (other.CompareTag("Player"))
        {
            if (active)
            {
                if (PlatformSymbolism.color == other.gameObject.GetComponent<ActiveColor>().primaryColour)
                {
                    StartCoroutine(LoseColor());
                    active = false;
                    parameters.NextStep(this.gameObject);
                    line.playerscore++;
                    line.GiveScore();
                    pointSound.Play();
                }else
                {
                }
            }
        }

    }

    IEnumerator LoseColor()
    {
        float final = 0f;

        while (final < parameters.blockIntensity)
        {
            PlatformSymbolism.SetColor("_EmissionColor", blockColour / final);
            final += 0.1f * parameters.inertSpeed; 
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
