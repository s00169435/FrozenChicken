using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DogPathfinder))]
public class DogPooper : MonoBehaviour
{
    Coroutine CoroutPoop;
    [SerializeField] List<Obstacle> poopList = new List<Obstacle>();
    public List<Obstacle> PoopList { get { return poopList; } }
    [SerializeField] Obstacle PoopPrefab;
    [SerializeField] float poopCooldown;
    [SerializeField] int maxPoopCount;
    public Vector3 tilePosition;

    // Start is called before the first frame update
    void Start()
    { 
        CoroutPoop = StartCoroutine(Poop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    IEnumerator Poop()
    {
        Debug.Log("Entering poop");
        float timeElapsed = 0;

        while (timeElapsed < poopCooldown)
        {
            timeElapsed += Time.deltaTime;
            Debug.Log((int)timeElapsed);
            yield return new WaitForEndOfFrame();
        }

        if (poopList.Count < maxPoopCount)
        {
            poopList.Add(Instantiate<Obstacle>(PoopPrefab, tilePosition, Quaternion.identity));
            Debug.Log("Poops: " + poopList.Count);
        }

        if (CoroutPoop != null)
            StopCoroutine(CoroutPoop);

        CoroutPoop = StartCoroutine(Poop());
    }
}
