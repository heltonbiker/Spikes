using System;

namespace StCamSWareCS.SettingCtrl
{
	public class CItemForComboBox
	{
		private string m_Name;
		private int m_SettingValue = 0;

		public CItemForComboBox(string name, int val)
		{
			m_Name = name;
			m_SettingValue = val;
		}
		public int SettingValue
		{
			get{return(m_SettingValue);}
		}
		public override string ToString()
		{
			return(m_Name);
		}

	}
}
