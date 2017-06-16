using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace MenuComboBox
{
	public class ClosePopupAction : TriggerAction<Popup>
	{

		public object TargetObject
		{
			get { return GetValue(TargetObjectProperty); }
			set { SetValue(TargetObjectProperty, value); }
		}

		public static readonly DependencyProperty TargetObjectProperty
			= DependencyProperty.Register("TargetObject", typeof(Popup),
			typeof(ClosePopupAction));

		protected override void Invoke(object parameter)
		{
			object target = TargetObject ?? AssociatedObject;
			PropertyInfo propertyInfo = target.GetType().GetProperty(
				"IsOpen",
				BindingFlags.Instance | BindingFlags.Public
				| BindingFlags.NonPublic | BindingFlags.InvokeMethod);

			propertyInfo.SetValue(target, false);
		}
	}
}
