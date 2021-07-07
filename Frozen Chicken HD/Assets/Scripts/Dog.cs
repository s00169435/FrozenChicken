using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DogPathfinder))]
public class Dog : MonoBehaviour
{
    Coroutine CoroutPoop;
    [SerializeField] List<Obstacle> PoopList = new List<Obstacle>();
    [SerializeField] Obstacle PoopPrefab;
    [SerializeField] float poopCooldown;
    [SerializeField] int maxPoopCount;

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
            yield return new WaitForEndOfFrame();
        }

        if (PoopList.Count < maxPoopCount)
        {
            PoopList.Add(Instantiate<Obstacle>(PoopPrefab, transform.position, Quaternion.identity));
            Debug.Log("Poops: " + PoopList.Count);
        }

        if (CoroutPoop != null)
            StopCoroutine(CoroutPoop);

        CoroutPoop = StartCoroutine(Poop());
    }
}
