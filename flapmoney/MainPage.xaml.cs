namespace flapmoney
{
	public partial class MainPage : ContentPage
	{
		const int gravidade = 10;
		const int fps = 300;
		bool faliceu = true;
		double LarguraJanela = 0;
		double AlturaJanela = 0;
		int velocidade = 80;

		public MainPage()
		{
			InitializeComponent();
		}

		

		void OnGameOverClicked(object s, TappedEventArgs a)

		{
			FrameGameOver.IsVisible=false;
			Inicializar();
			Desenha();
		}

		void Inicializar()
		{
			faliceu=false;
			Mario.TranslationY=0;
		}
		
		protected override void OnSizeAllocated(double AJ, double LJ)
		{
			base.OnSizeAllocated(AJ, LJ);
			LarguraJanela = LJ;
			AlturaJanela = AJ;
		}

		void AndarCanos()
		{
			CanoCima.TranslationX -= velocidade;
			CanoBaixo.TranslationX -= velocidade;

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
				CriaGravidade();
				AndarCanos();
				await Task.Delay(fps);
			}
		}

		async void CriaGravidade()
		{
			Mario.TranslationY += gravidade;
			
			
		}

		
	}
}
