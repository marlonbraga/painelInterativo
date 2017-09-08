using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot : MonoBehaviour
{
	[SerializeField]
	private EnumTypes.Direction direction;

	public EnumTypes.Direction Direction {
		get
		{
			return direction;
		}

		private set
		{
			direction = value;
		}
	}
}
