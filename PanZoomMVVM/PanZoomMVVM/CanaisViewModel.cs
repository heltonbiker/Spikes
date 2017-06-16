using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace PanZoomMVVM
{
    public class CanaisViewModel : ViewModelBase
    {
        public List<PointCollection> Canais 
        {
            get { return _canais; }
            set
            {
                _canais = value;
                RaisePropertyChanged(() => Canais);
            }
        }
        List<PointCollection> _canais;

        // CONSTRUTOR
        public CanaisViewModel()
        {
            Canais = new List<PointCollection>();
            var canalum = Enumerable.Range(0,20)
                                    .Select((v,i) => new Point(v, i%2));
            var canaldois = Enumerable.Range(0,40)
                                      .Select((v,i) => new Point(v*0.5, i%2));
            Canais.Add(new PointCollection(canalum));
            Canais.Add(new PointCollection(canaldois));
        }

    }
}
