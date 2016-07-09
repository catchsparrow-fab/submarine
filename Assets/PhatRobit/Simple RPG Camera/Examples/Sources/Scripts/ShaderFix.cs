using UnityEngine;
using System.Collections;

namespace PhatRobit
{
	public class ShaderFix : MonoBehaviour
	{
		void Start()
		{
			Renderer[] renderers = GetComponentsInChildren<Renderer>();

			foreach(Renderer renderer in renderers)
			{
				foreach(Material material in renderer.materials)
				{
					material.SetInt("_ZWrite", 1);
				}
			}
		}
	}
}