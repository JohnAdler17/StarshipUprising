using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] private bool dontDestroy = false;

    public int GetDamage() {
      return damage;
    }

    public void Hit() {
      if (!dontDestroy) {
        Destroy(gameObject);
      }
    }
}
