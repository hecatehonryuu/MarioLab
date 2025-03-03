using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableSM/Actions/Buff")]
public class BuffAction : Action
{
    public AudioClip invincibilityStart;
    public override void Act(StateController controller)
    {
        BuffStateController m = (BuffStateController)controller;
        m.gameObject.GetComponent<AudioSource>().PlayOneShot(invincibilityStart);
        m.SetRendererToFlicker();
    }
}
