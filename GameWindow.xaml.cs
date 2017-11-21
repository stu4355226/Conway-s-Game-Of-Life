using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Threading;
using System.Collections;
using GameOfLife.ViewModel;
namespace GameOfLife
{

    public partial class GameWindow : Window
    {
        public static ArrayList startpos = new ArrayList();
		private static WindowViewModel viewModel;

		public GameWindow()
        {
            InitializeComponent();
			viewModel = new WindowViewModel();
		}


		private void StartingPos_Click(object sender, RoutedEventArgs e)
		{
			foreach (Cell reset in LifeGrid.Children)
			{
				reset.NextAlive = false;
			}
			foreach (Cell savedCell in startpos)
			{
				foreach (Cell cell in LifeGrid.Children)
				{
					if (cell.Row == savedCell.Row && cell.Column == savedCell.Column)
						cell.NextAlive = true;

				}
			}
			viewModel.Update(LifeGrid);
		}

		private void SavePos_Click(object sender, RoutedEventArgs e)
		{
			startpos.Clear();
			foreach (Cell cell in LifeGrid.Children)
			{
				if (cell.Alive == true)
					startpos.Add(cell);
			}
		}
           
     
        private void NextStep_Click(object sender, RoutedEventArgs e)
        {
			viewModel.Next(LifeGrid);
			viewModel.Update(LifeGrid);
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (Cell cell in LifeGrid.Children)
                cell.NextAlive = false;
			viewModel.Update(LifeGrid);
		}


		private async void Autostep_Checked(object sender, RoutedEventArgs e)
		{
			ToggleButton auto = (ToggleButton)sender;
			await AutoRun(auto);
		}

		private async Task AutoRun(ToggleButton runbutton)
		{
			while (runbutton.IsChecked == true)
			{

				int wacht = (int)DelaySlider.Value;
				viewModel.Next(LifeGrid);
				viewModel.Update(LifeGrid); await Task.Delay(wacht);
			}
		}


	

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			LifeGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
			LifeGrid.VerticalAlignment = VerticalAlignment.Stretch;

			for (int i = 0; i < 50; i++)
			{
				ColumnDefinition columnDefinition = new ColumnDefinition();
				RowDefinition rowDefinition = new RowDefinition();
				LifeGrid.ColumnDefinitions.Add(columnDefinition);
				LifeGrid.RowDefinitions.Add(rowDefinition);
			}

			Cell cell;

			for (int row = 0; row < LifeGrid.RowDefinitions.Count; row++)
			{
				for (int column = 0; column < LifeGrid.ColumnDefinitions.Count; column++)
				{

					cell = new Cell(false, row, column);
					cell.Opacity = 0;
					cell.Width = LifeGrid.Width / LifeGrid.ColumnDefinitions.Count;
					cell.Height = LifeGrid.Height / LifeGrid.RowDefinitions.Count;
					cell.SetValue(Grid.ColumnProperty, column);
					cell.SetValue(Grid.RowProperty, row);
					LifeGrid.Children.Add(cell);
				}
			}
		}
	}
}
