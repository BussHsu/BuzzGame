using UnityEngine;
using System.Collections;

public class CityNode : MapNode 
{

	int CityNum;

	#region implemented abstract members of MapNode

	public override bool ConnectCity (int cityNum)
	{
		return cityNum==this.CityNum;
	}

	#endregion
}
