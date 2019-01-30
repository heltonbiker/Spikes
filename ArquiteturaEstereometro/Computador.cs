namespace ArquiteturaEstereometro
{
	public abstract class Computador
	{
		void Configurar()
		{
			var rasterestereografo = ConectarRasterestereografo();
			var calibracao = ObterCalibracao();
			rasterestereografo.SalvarCalibracao(calibracao);
		}


		void Executar()
		{
			var rasterestereografo = ConectarRasterestereografo();

			var calibracao = rasterestereografo.LerCalibracao();
			rasterestereografo.IniciarModoCaptura();

			rasterestereografo.AtualizarConfiguracao(
				new ParametrosCaptura
				{
					AlturaProjecao = 0.5,
					Exposicao = 0.3,
					Ganho = 0.6
				});

			rasterestereografo.SubirDescer(ModoSubidaDescida.Subindo);
			rasterestereografo.SubirDescer(ModoSubidaDescida.Descendo);
			rasterestereografo.SubirDescer(ModoSubidaDescida.Parado);

			rasterestereografo.Capturar();

			var imagemBranca = rasterestereografo.LerImagemBranca();
			var imagemListras = rasterestereografo.LerImagemListras();
			
			ProcessarResultados(calibracao, imagemListras, imagemBranca);
		}


		protected abstract void ProcessarResultados(
			CalibracaoVert calibracao, 
			Imagem imagemListras, 
			Imagem imagemBranca);


		protected abstract RasterEstereografo ConectarRasterestereografo();

		protected abstract CalibracaoVert ObterCalibracao();
	}

	public enum ModoSubidaDescida
	{
		Parado,
		Subindo,
		Descendo
	}
}