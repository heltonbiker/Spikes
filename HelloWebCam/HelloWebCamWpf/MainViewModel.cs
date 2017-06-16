using AForge.Video.DirectShow;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWebCamWpf
{
	public class MainViewModel : ViewModelBase
	{
		public ObservableCollection<LiveCameraViewModel> Cameras { get; set; }
			= new ObservableCollection<LiveCameraViewModel>();

		public MainViewModel()
		{
			var foundDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

			for (int i = 0; i < foundDevices.Count; i++)
			{
				var device = foundDevices[i];
				var lcvm = new LiveCameraViewModel(device.MonikerString);
				Cameras.Add(lcvm);
			}

		}


	}
}
