using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace KanimExplorer
{
	internal static class UIUtils
	{
		internal static bool TryWithErrorMessage(Func<bool> func, string actionContext, ILogger log = null, [CallerMemberName] string callingFunctionName = null)
		{
			ArgumentNullException.ThrowIfNull(func);
			ArgumentNullException.ThrowIfNull(actionContext);
			ArgumentNullException.ThrowIfNull(callingFunctionName);

			using (log?.BeginScope(callingFunctionName))
			{
				try
				{
					return func();
				}
				catch (Exception ex)
				{
					if (log != null)
					{
						log.LogError(ex, $"An error occurred while {actionContext} from {callingFunctionName}.");
						MessageBox.Show($"An error occured.\nPlease check the log for more details", actionContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
					
					throw;
				}
			}
		}
	}
}
