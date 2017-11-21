﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Threading;
using System.ComponentModel;

namespace GameOfLife
{
    public class Cell : Button
    {
		#region Field 
		public int Row { get; set; }
        public int Column { get; set; }
		public bool NextAlive { get; set; }
		private bool AliveValue;
		#endregion

		#region Constructor 
		public Cell(bool currentState, int row, int column)
		{
			Row = row;
			Column = column;
			Alive = currentState;
		}
		#endregion




		protected override void OnClick()
		{
			base.OnClick();
			if (NextAlive == true)
			{
				NextAlive = false;
				this.Opacity = 0;
			}
			else
			{
				NextAlive = true;
				this.Opacity = 1;
			}

			if (Alive == true)
			{
				Alive = false;
				this.Opacity = 0;
			}
			else
			{
				Alive = true;
				this.Opacity = 1;
			}
		}

		public void ChangeState()
		{

			if (NextAlive == true)
			{
				Alive = true;
			}
			if (NextAlive == false)
				Alive = false;
		}
		public bool Alive 
        {
            get
            {
                return AliveValue;
            }
            set
            {
                AliveValue = value;
                
            }
        }



        public void NextState(int neighbours)
        {
            int count = neighbours;
            if (Alive == true && count < 2 || count > 3)
            {
                NextAlive = false;
            }
            else if (Alive==false && count == 3)
                NextAlive = true;
        }



    }
}
