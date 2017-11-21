using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace TimelineScrubbing
{
	public class ObservableObject : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		public void RaisePropertyChanged(String propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
		{
			var lambda = (LambdaExpression)property;

			MemberExpression memberExpression;
			if (lambda.Body is UnaryExpression unaryExpression)
			{
				memberExpression = (MemberExpression)unaryExpression.Operand;
			}
			else
				memberExpression = (MemberExpression)lambda.Body;

			RaisePropertyChanged(memberExpression.Member.Name);
		}
	}
}
