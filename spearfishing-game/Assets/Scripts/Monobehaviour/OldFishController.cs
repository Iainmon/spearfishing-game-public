using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OldFishController : MonoBehaviour {

    FishBehaviour.Chain fishChain;
    public string fishName;
    public float alertDistance;

    GameObject player;

    public enum FishStates { EnterLiving, Living, EnterFleeing, Fleeing }
    [System.NonSerialized]
    public FishStates state = FishStates.EnterLiving;


    void Start() {
        fishChain = FishBehaviour.GetChain(this, fishName);
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update() {

        switch (state) {

            case FishStates.EnterLiving:
                StartCoroutine(AILoop());
                state = FishStates.Living;
                break;

            case FishStates.Living:
                if (IsAlertedBy(player)) {
                    StopCoroutine(AILoop());
                    state = FishStates.Fleeing;
                }
                break;

            case FishStates.EnterFleeing:
                break;
            case FishStates.Fleeing:
                break;
        }
    }


    bool IsAlertedBy(GameObject target) {
        return Vector3.Distance(transform.position, player.transform.position) <= alertDistance && CanSee(target);
    }


    bool CanSee(GameObject target) {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, player.transform.position - transform.position, Vector3.Distance(transform.position, player.transform.position));

        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.transform.tag == "Terrain") {
                return false;
            }
        }

        return true;
    }


    public IEnumerator AILoop() {
        while (true) {
            yield return StartCoroutine(fishChain.GetCurrentAction()());
            fishChain.Step();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Fish Action Functions
    public IEnumerator Swim() {


        yield break;
    }

    public IEnumerator Eat() {
        Debug.Log("Eat");
        yield break;
    }
}
