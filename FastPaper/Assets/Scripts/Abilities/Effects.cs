using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
	public bool requiresTarget;
}

public abstract class NonTargetedEffect : Effect
{
	public abstract void OnTrigger(InSceneCard attachedTo);
}

public abstract class TargetedEffect : Effect
{
	public abstract void OnTrigger(InSceneCard attachedTo, InSceneCard target);
}