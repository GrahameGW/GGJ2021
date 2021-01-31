using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : MonoBehaviour {
    protected int dmg = 0;
    protected int owner = 0;
    protected HashSet<int> hasHit = new HashSet<int>();


    public void SetOwner(int id) {
        owner = id;
    }

    public void SetDmg(int damage) {
        dmg = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        int hitId = collision.gameObject.GetInstanceID();
        if (owner == hitId || hasHit.Contains(hitId)) {
            // it's our attack or we have already been hit
            return;
        }

        Creature hit = collision.gameObject.GetComponent<Creature>();
        if (hit){
            Debug.Log("Attack hit");
            hit.Damage(dmg);
            hasHit.Add(hitId);
        }
        

    }

}
