using System;

namespace ArquiteturaEstereometro
{
	public abstract class RasterEstereografo
	{
		Camera _camera;
		Projetor _projetor;

		bool _modoCaptura = false;

		ParametrosCaptura _parametros;

		public void IniciarModoCaptura()
		{
			_modoCaptura = true;
			_camera.IniciarModoCaptura();
			_projetor.IniciarModoCaptura();
		}

		public void AtualizarConfiguracao(ParametrosCaptura parametros)
		{
			_parametros = parametros;
		}

		public abstract CalibracaoVert LerCalibracao();


		public event EventHandler<Imagem> ImagemCapturada;

		public Imagem LerImagemBranca()
		{
			throw new NotImplementedException();
		}

		public Imagem LerImagemListras()
		{
			throw new NotImplementedException();
		}

		public void Capturar()
		{
			_camera.PararStreaming();
			_projetor.ProjetarListrada();
			var imagemListrada = _camera.Capturar();
			_projetor.ProjetarBranca();
			var imagemBranca = _camera.Capturar();
			_projetor.ProjetarListrada();
			_camera.IniciarStreaming();
		}

		public abstract void SubirDescer(ModoSubidaDescida modo);

		public void SalvarCalibracao(CalibracaoVert calibracao)
		{
			throw new NotImplementedException();
		}
	}



	public abstract class Camera
	{
		public abstract void IniciarModoCaptura();

		public void Configurar(double ganho, double exposicao)
		{
			ConfigurarGanho(ganho);
			ConfigurarExposicao(exposicao);
		}

		public abstract void IniciarStreaming();
		public abstract void PararStreaming();

		protected abstract void ConfigurarGanho(double ganho);

		protected abstract void ConfigurarExposicao(double ganho);

		public abstract Imagem Capturar();
	}

	public abstract class Projetor
	{
		Imagem _imagemBranca = CriarImagemBranca();
		Imagem _imagemListrada;

		public abstract void IniciarModoCaptura();

		public void Configurar(double alturaProjecao)
		{
			var listrada = CriarListrada(alturaProjecao);
			ProjetarListrada();
			CapturarListrada();
			EnviarListrada();
		}

		protected abstract Imagem CriarListrada(double alturaProjecao);

		protected abstract void ProjetarListrada();

		protected abstract void ProjetarBranca();

		protected abstract void CapturarListrada();

		protected abstract void EnviarListrada();

		protected abstract Imagem CriarImagemBranca();

	}

	public abstract class Imagem { }

	public abstract class CalibracaoVert { }

	public class ParametrosCaptura
	{
		public double AlturaProjecao { get; set; }
		public double Ganho { get; set; }
		public double Exposicao { get; set; }
	}
}
