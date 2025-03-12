/****************************************************************************
 * 2025.3 WIN-C740RE9VRAA
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class AchivementPanel
	{
		[SerializeField] public UnityEngine.UI.Button AchivementItemTemplate;
		[SerializeField] public UnityEngine.UI.Button BtnClose;
		[SerializeField] public RectTransform AchivementItemRoot;

		public void Clear()
		{
			AchivementItemTemplate = null;
			BtnClose = null;
			AchivementItemRoot = null;
		}

		public override string ComponentName
		{
			get { return "AchivementPanel";}
		}
	}
}
