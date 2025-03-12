using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	// Generate Id:e12c74d3-bedb-4063-b07d-0bf5e6fd1bd6
	public partial class UIGameStartPanel
	{
		public const string Name = "UIGameStartPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnStartGame;
		[SerializeField]
		public UnityEngine.UI.Button QuitGame;
		[SerializeField]
		public UnityEngine.UI.Button BtnCoinUpgrade;
		[SerializeField]
		public UnityEngine.UI.Button BtnAchivement;
		[SerializeField]
		public CoinUpgradePanel CoinUpgradePanel;
		[SerializeField]
		public AchivementPanel AchivementPanel;
		
		private UIGameStartPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnStartGame = null;
			QuitGame = null;
			BtnCoinUpgrade = null;
			BtnAchivement = null;
			CoinUpgradePanel = null;
			AchivementPanel = null;
			
			mData = null;
		}
		
		public UIGameStartPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UIGameStartPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UIGameStartPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
