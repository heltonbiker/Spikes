using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AveragingBuffer
{
    class AveragingBuffer {

        private double[] _average_buffer;
        private double[] _rms_buffer;

        private int current_position = 0;
        private double sum = 0;


        public double Average { get { return sum / _average_buffer.length;  } }


        // CONSTRUTOR
        public AveragingBuffer(int size) {

            this.size = size;
            this.fraction = 1.0 / Convert.ToDouble(size);
            this.buffer = Enumerable.Repeat<double>(0.0, size).ToArray();
        }



        internal void Add(double val) {   
         
            sum += val - buffer[current_position];

            buffer[current_position++] = val;

            current_position %= size;

        }

    }
}
