using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{

    public List<GameObject> backgrounds;
    public List<GameObject> currentBackgrounds;
    private GameObject characterCam;
    
    public List<Vector3> origonalPositions;
    private float origonalX;
    private float BackgroundDistance;
    public float backgroundScale;
    public float xPixels;
    public float followXRate;
    public float followYRate;
    public float yOffset;
    // Use this for initialization
    void Start()
    {
        GameObject character = GameObject.Find("Character");
        BackgroundDistance = (xPixels / 100.0f) * backgroundScale;
        characterCam = GameObject.Find("Main Camera");
        origonalX = characterCam.transform.position.x;
        Vector3 characterPos = character.transform.position - new Vector3((BackgroundDistance * 2), 0, 0);
        Quaternion characterRot = character.transform.rotation;
        for (int i = 0; i < 5; i++)
        {
            GameObject background = Instantiate(backgrounds[Random.Range(0, backgrounds.Count)], characterPos, characterRot);
            background.transform.position = new Vector3(background.transform.position.x + (BackgroundDistance * i), background.transform.position.y + 4, 10);
            currentBackgrounds.Add(background);
            origonalPositions.Add(background.transform.position);
        }


    }

    // Update is called once per frame
    void Update()
    {
        //creates a new background if background is needed on the right side of the screen
        if (characterCam.transform.position.x >= currentBackgrounds[currentBackgrounds.Count - 1].transform.position.x - BackgroundDistance)
        {
            Vector3 backgroundPos = currentBackgrounds[currentBackgrounds.Count - 1].transform.position + new Vector3(BackgroundDistance, 0, 10);
            Quaternion backgroundRot = currentBackgrounds[currentBackgrounds.Count - 1].transform.rotation;
            GameObject background = Instantiate(backgrounds[Random.Range(0, backgrounds.Count)], backgroundPos, backgroundRot);
            currentBackgrounds.Add(background);
            origonalPositions.Add(background.transform.position);
        }

        //create a new background if background is needed on the left side of the screen
        if (characterCam.transform.position.x <= currentBackgrounds[0].transform.position.x + BackgroundDistance)
        {
            Vector3 backgroundPos = currentBackgrounds[0].transform.position + new Vector3(-BackgroundDistance, 0, 0);
            Quaternion backgroundRot = currentBackgrounds[0].transform.rotation;
            GameObject background = Instantiate(backgrounds[Random.Range(0, backgrounds.Count)], backgroundPos, backgroundRot);
            currentBackgrounds.Insert(0, background);
            origonalPositions.Insert(0, background.transform.position);
        }

    }
    private void FixedUpdate()
    {
        currentBackgrounds[0].transform.position = origonalPositions[0] + new Vector3(origonalX + (characterCam.transform.position.x / followXRate), yOffset + characterCam.transform.position.y / followYRate, 0);
        for (int i = 0; i < currentBackgrounds.Count; i++)

        {
            if (i > 0)
            {
                currentBackgrounds[i].transform.position = new Vector3(currentBackgrounds[0].transform.position.x + (BackgroundDistance * i), currentBackgrounds[0].transform.position.y, 10);
            }
        }
    }

}
