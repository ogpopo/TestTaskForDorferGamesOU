using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaitUntilAnimationIsPlayed : CustomYieldInstruction
{
    private readonly Animator _animator;
    private readonly string _animationName;

    private readonly int _layerIndex;
    
    public override bool keepWaiting
    {
        get
        {
            return CheckIfAnimationHasFinishedPlaying();
        }
    }

    public WaitUntilAnimationIsPlayed(Animator animatorForCheck, string animationName)
    {
        _animator = animatorForCheck;
        _animationName = animationName;
    }

    public WaitUntilAnimationIsPlayed(Animator animatorForCheck, string animationName, int layerIndex)
    {
        _animator = animatorForCheck;
        _animationName = animationName;
        _layerIndex = layerIndex;
    }
    
    private bool CheckIfAnimationHasFinishedPlaying()
    {
        var animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(_layerIndex);

        return 
            !animatorStateInfo.IsName(_animationName);
    }
}
