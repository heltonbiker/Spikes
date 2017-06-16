using System;
using System.Collections.Generic;
using System.Text;

namespace SensorTechnology
{
	class CFPS
	{
		private bool m_bEnoughDataExist = false;
		private int m_nCurPos = 0;
		private int m_nSampleCount = 0;
		private double[] m_aElapsedLog;
		private System.Diagnostics.Stopwatch m_StopWatich = null;

		public CFPS(int nSampleCount)
		{
			m_nSampleCount = nSampleCount;
			m_aElapsedLog = new double[m_nSampleCount];
			m_StopWatich = System.Diagnostics.Stopwatch.StartNew();
		}

		public void Check()
		{
			m_aElapsedLog[m_nCurPos] = m_StopWatich.Elapsed.TotalMilliseconds;
			m_nCurPos = m_nCurPos + 1;
			if(m_nSampleCount <= m_nCurPos)
			{
				m_bEnoughDataExist = true;
				m_nCurPos = 0;
			}
		}

		public double GetFPS()
		{
			int nStartPos = m_nCurPos;
			int nEndPos = m_nCurPos - 1;
			if(nEndPos < 0)
			{
				nEndPos = m_nSampleCount - 1;
			}

			int nCurSampleCount = m_nSampleCount - 1;

			if(!m_bEnoughDataExist)
			{
				nStartPos = 0;
				nEndPos = m_nCurPos - 1;
				nCurSampleCount = m_nCurPos - 1;
			}

			double dblReval = 0.0;

			if(1 <= nCurSampleCount)
			{
				double dblDelta = m_aElapsedLog[nEndPos] - m_aElapsedLog[nStartPos];
				dblReval = nCurSampleCount * 1000 / dblDelta;
			}

			return(dblReval);
		}

		public void Reset()
		{
			m_bEnoughDataExist = false;
			m_nCurPos = 0;
		}
	}
}
