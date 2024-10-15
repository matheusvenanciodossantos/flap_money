namespace flapmoney
{
	public partial class MainPage : ContentPage
	{
		const int gravidade = 5;
		const int fps = 20;
		const int maxTempoPulando = 3;
		const int ForcaPulo = 30;
		bool faliceu = true;
		double LarguraJanela = 0;
		double AlturaJanela = 0;
		int xrl8 = 7;
		int TempoPulando = 0;
		bool EstaPulando = false;
		public MainPage()
		{
			InitializeComponent();
		}


		void AplicaPulo()
		{
			Mario.TranslationY -= ForcaPulo;
			TempoPulando++;
			if (TempoPulando > maxTempoPulando)
			{
				EstaPulando = false;
				TempoPulando = 0;
			}
			

		}
		void JumpMarinho(object sender, TappedEventArgs e)
			{
				EstaPulando = true;
			}
		void OnGameOverClicked(object s, TappedEventArgs a)

		{
			FrameGameOver.IsVisible = false;
			Inicializar();
			Desenha();
		}

		void Inicializar()
		{
			faliceu = false;
			Mario.TranslationY = 0;
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
			Mario.TranslationY += gravidade;



		}


		bool VerificaColisaoCima()
		{
			var minAltura = -AlturaJanela / 2;
			if (Mario.TranslationY <= minAltura)
				return true;

			else
				return false;
		}

		bool VerificaColisaoBaxo()
		{
			var maxAltura = AlturaJanela / 2;
			if (Mario.TranslationY >= maxAltura)
				return true;

			else
				return false;

		}

		bool VerificaColisaoCanoDeCima()
		{
			var minAltura = -LarguraJanela / 2;
			if (Mario.TranslationY <= minAltura)
				return true;

			else
				return false;
		}
		bool VerificaColisao()
		{
			if (!faliceu)
			{
				if (VerificaColisaoCima() || VerificaColisaoBaxo())
				{
					return true;
				}
			}
			return false;
		}


	}
}
