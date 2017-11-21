using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameOfLife.ViewModel
{
	public class WindowViewModel
	{
		public void Next(Grid LifeGrid)
		{

			foreach (Cell cell in LifeGrid.Children)
			{
				int count = 0;
				for (int y = cell.Row - 1; y <= cell.Row + 1; y++)
				{
					if (y >= 0 && y <= LifeGrid.RowDefinitions.Count - 1)
					{
						for (int x = cell.Column - 1; x <= cell.Column + 1; x++)
						{
							if (x >= 0 && x <= LifeGrid.ColumnDefinitions.Count - 1)
							{
								Cell neighbour = LifeGrid.Children[y * LifeGrid.ColumnDefinitions.Count + x] as Cell;
								if (neighbour.Alive == true && (cell.Column != x || cell.Row != y))
									count++;
							}
						}
					}
				}
				cell.NextState(count);
			}
		}

		public void Update(Grid LifeGrid)
		{
			foreach (Cell cell in LifeGrid.Children)
			{
				cell.Alive = cell.NextAlive;
				if (cell.Alive == true)
					cell.Opacity = 1;


				else
					cell.Opacity = 0;
			}
		}
	}

}
