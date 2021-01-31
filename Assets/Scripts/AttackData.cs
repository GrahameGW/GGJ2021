using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : MonoBehaviour {
    public int dmg = 0;
    public int owner;
    public HashSet<int> hasHit;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        int hitId = collision.gameObject.GetInstanceID();

        if (owner == hitId || hasHit.Contains(hitId)) {
            // it's our attack or we have already been hit
            return;
        }

        Creature hit = collision.gameObject.GetComponent<Creature>();
        if (hit){
            hit.Damage(dmg);
            hasHit.Add(hitId);
        }
        

    }

}
