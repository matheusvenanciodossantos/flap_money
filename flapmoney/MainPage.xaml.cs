namespace flapmoney
{
	public partial class MainPage : ContentPage
	{
		const int gravidade = 2;
		const int fps = 25;
		const int maxTempoPulando = 2;
		const int ForcaPulo = 35;
		const int AberturaDoCano = 70;
		bool faliceu = true;
		double LarguraJanela = 10;
		double AlturaJanela = 10;
		int xrl8 = 7;
		int TempoPulando = 20;
		int Score = 0;
		bool EstaPulando = false;


		public MainPage()
		{
			InitializeComponent();
			CanoBaixo.TranslationX = -5000;
			CanoCima.TranslationX = -5000;
		}


		void AplicaPulo()
		{
			Goku.TranslationY -= ForcaPulo;
			TempoPulando++;
			if (TempoPulando > maxTempoPulando)
			{
				EstaPulando = false;
				TempoPulando = 0;
			}


		}
		void JumpGokuzinho(object sender, TappedEventArgs e)
		{
			EstaPulando = true;
		}
		void OnGameOverClicked(object s, TappedEventArgs a)

		{
			FrameGameOver.IsVisible = false;
			Inicializar();
			Desenha();
			LabelScore.IsVisible = true;
		}

		void Inicializar()
		{
			CanoCima.TranslationX = -LarguraJanela;
			CanoCima.TranslationY = -LarguraJanela;
			Goku.TranslationX = 0;
			Goku.TranslationY = 0;
			faliceu = false;
			Goku.TranslationY = 0;
		}

		protected override void OnSizeAllocated(double LJ, double AJ)
		{
			base.OnSizeAllocated(LJ, AJ);
			LarguraJanela = LJ;
			AlturaJanela = AJ;
		}

		void AndarCanos()
		{
			CanoCima.TranslationX -= xrl8;
			CanoBaixo.TranslationX -= xrl8;

			if (CanoCima.TranslationX < -LarguraJanela)
			{
				CanoBaixo.TranslationX = 0;
				CanoCima.TranslationX = 0;
				var maxAltura = -100;
				var minAltura = -CanoBaixo.HeightRequest;
				CanoCima.TranslationY = Random.Shared.Next((int)minAltura, (int)maxAltura);
				CanoBaixo.TranslationY = CanoCima.TranslationY + AberturaDoCano + CanoBaixo.HeightRequest;
				Score++;
				LabelScore.Text = "Canos:" + Score.ToString("D3");
				FrameScore.Text = "Score:" + Score.ToString("D3");

			}
		}


		async Task Desenha()
		{
			while (!faliceu)
			{
				{
					if (EstaPulando)
						AplicaPulo();
					else
						CriaGravidade();
				}
				CriaGravidade();
				AndarCanos();
				if (VerificaColisao())
				{
					faliceu = true;
					FrameGameOver.IsVisible = true;
					break;
				}
				await Task.Delay(fps);
			}
		}

		async void CriaGravidade()
		{
			Goku.TranslationY += gravidade;
			//Goku.TranslationY -= gravidade;
		}

		//COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES

		





		bool VerificaColisaoCima()
		{
			var minAltura = -AlturaJanela / 2;
			if (Goku.TranslationY <= minAltura)
				return true;

			else
				return false;
		}

		bool VerificaColisaoBaxo()
		{
			var maxAltura = AlturaJanela / 2;
			if (Goku.TranslationY >= maxAltura)
				return true;

			else
				return false;

		}

		bool VerificaColisaoCanoDeCima()
		{
			var PosHGoku = (LarguraJanela / 2) - (Goku.WidthRequest / 2);
			var PosVGoku = (LarguraJanela / 2) - (Goku.HeightRequest / 2) + Goku.TranslationY;
			if (PosHGoku >= Math.Abs(CanoCima.TranslationX) - CanoCima.WidthRequest &&
				PosHGoku <= Math.Abs(CanoCima.TranslationX) + CanoCima.WidthRequest &&
				PosVGoku <= CanoCima.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		bool VerificaColisaoCanoDeBaixo()
		{
			var PosHGoku = (LarguraJanela / 2) - (Goku.WidthRequest / 2);
			var PosVGoku = (LarguraJanela / 2) - (Goku.HeightRequest / 2) + Goku.TranslationY;
			if (PosHGoku >= Math.Abs(CanoBaixo.TranslationX) - CanoBaixo.WidthRequest &&
				PosHGoku <= Math.Abs(CanoBaixo.TranslationX) + CanoBaixo.WidthRequest &&
				PosVGoku >= CanoBaixo.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		bool VerificaColisao()
		{
			if (!faliceu)
			{
				if (VerificaColisaoCima() || VerificaColisaoBaxo() || VerificaColisaoCanoDeCima()||VerificaColisaoCanoDeBaixo())
				{
					return true;
				}
			}
			return false;
		}

		//COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES_COLISÕES
	}
}
