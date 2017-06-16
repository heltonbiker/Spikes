using System;
using System.Collections.Generic;
using System.Text;

namespace StCamSWareCS.SettingCtrl
{
	public interface ISettingValue : ISettingCtrl
	{
		int SettingValue { get;set;}
		event System.EventHandler SettingValueChanged;
	}
}
