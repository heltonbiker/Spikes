﻿using System.Windows;
using System.Windows.Controls;

namespace TimelineScrubbing
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			DataContext = new ScrubbingViewModel();
		}
	}
}
