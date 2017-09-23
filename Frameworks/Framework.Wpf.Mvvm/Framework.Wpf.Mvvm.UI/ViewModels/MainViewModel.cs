using Framework.Wpf.Mvvm.Core.ViewModels;
using Framework.Wpf.Mvvm.Core.ViewModels.Base;
using Framework.Wpf.Mvvm.UI.ViewModels.Content;

namespace Framework.Wpf.Mvvm.UI.ViewModels
{
	public class MainViewModel : Base
	{
		public MainViewModel()
		{
			InitializeViews();
		}
		private void InitializeViews()
		{
			HeaderView = new HeaderViewModel();
			NavigationView = new NavigationViewModel();
			FeaturedView = new FeaturedViewModel();
			
			BeforeMainView = new BeforeMainViewModel();
			AfterMainView = new AfterMainViewModel();
			
			AsideFirstView = new AsideFirstViewModel();
			AsideSecondView = new AsideSecondViewModel();
			
			MessageView = new MessagesViewModel();
			BeforeContentView = new BeforeContentViewModel();
			ContentView = new ContentViewModel();
			AfterContentView = new AfterContentViewModel();
			
			TripelFirstView = new TripelFirstViewModel();
			TripelSecondView = new TripelSecondViewModel();
			TripelThirdView = new TripelThirdViewModel();
			
			QuadFirstView = new QuadFirstViewModel();
			QuadSecondView = new QuadSecondViewModel();
			QuadThirdView = new QuadThirdViewModel();
			QuadFourthView = new QuadFourthViewModel();

			FooterView = new FooterViewModel();
		}

		public HeaderViewModel HeaderView { get; set; }
		public NavigationViewModel NavigationView { get; set; }
		public BeforeMainViewModel BeforeMainView { get; set; }
		public FeaturedViewModel FeaturedView { get; set; }
		public AsideFirstViewModel AsideFirstView { get; set; }
		public AsideSecondViewModel AsideSecondView { get; set; }
		public MessagesViewModel MessageView { get; set; }
		public BeforeContentViewModel BeforeContentView { get; set; }
		public ContentViewModel ContentView { get; set; }
		public AfterContentViewModel AfterContentView { get; set; }
		public AfterMainViewModel AfterMainView { get; set; }
		public TripelFirstViewModel TripelFirstView { get; set; }
		public TripelSecondViewModel TripelSecondView { get; set; }
		public TripelThirdViewModel TripelThirdView { get; set; }
		public QuadFirstViewModel QuadFirstView { get; set; }
		public QuadSecondViewModel QuadSecondView { get; set; }
		public QuadThirdViewModel QuadThirdView { get; set; }
		public QuadFourthViewModel QuadFourthView { get; set; }
		public FooterViewModel FooterView { get; set; }
	}
}