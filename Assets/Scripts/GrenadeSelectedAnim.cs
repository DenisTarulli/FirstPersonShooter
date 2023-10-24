using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSelectedAnim : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    private const string IS_SELECTED = "IsSelected";

    private void Start()
    {
        animator = GetComponent<Animator>();        
    }

    public void GrenadeAnim()
    {
        animator.SetBool(IS_SELECTED, true);
        animator.SetBool(IS_SELECTED, false);
    }
}
