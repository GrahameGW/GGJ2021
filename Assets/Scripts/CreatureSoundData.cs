using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Creature Sound Data")]
public class CreatureSoundData : ScriptableObject
{
    public AudioClip AttackClip;

    public AudioClip JumpClip;

    public AudioClip LandingClip;

    public AudioClip MovementClip;

    public AudioClip DamagedClip;

    public AudioClip DeathClip;
}
