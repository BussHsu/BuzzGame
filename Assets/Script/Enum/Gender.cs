using UnityEngine;
using System.Collections;

[System.Flags]
public enum Gender
{
	None = 0,
	Male = 1 << 0,
	Female = 1 << 1,
	Both = 1 << 2,
	UnKnow = 1 << 3
}

//性別，不會擴充所以用enum就好
//影響可裝備的種類
//誘惑的效果？