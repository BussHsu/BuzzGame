using UnityEngine;
using System.Collections;

public class MatchTypeException : BaseException
{
	public readonly Unit attacker;
	public readonly Unit target;

	public MatchTypeException(Unit att, Unit tar):base(false)
	{
		attacker = att;
		target = tar;
	}

}
