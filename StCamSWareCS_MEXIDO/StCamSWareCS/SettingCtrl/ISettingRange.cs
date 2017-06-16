using System;

namespace StCamSWareCS.SettingCtrl
{
	public interface ISettingRange : ISettingValue
	{
		int SettingMin{get;set;}
		int SettingMax{get;set;}
	}
}
