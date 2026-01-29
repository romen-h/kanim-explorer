using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanimLib.DocumentModel
{
	public interface IDocument : INotifyPropertyChanged
	{
		bool UnsavedChanges
		{ get; }
		
		IEnumerable<string> Paths
		{ get; }
		
		void SetChangesFlag();
		bool Reload();
		bool Save();
		bool SaveAs(params string[] paths);
	}
}
