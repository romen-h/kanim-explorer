using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanimExplorer
{
	public class SelectedObjectChangedEventArgs : EventArgs
	{
		public object Object
		{ get; private set; }
		
		public SelectedObjectChangedEventArgs(object obj)
		{
			Object = obj;
		}
	}
}
