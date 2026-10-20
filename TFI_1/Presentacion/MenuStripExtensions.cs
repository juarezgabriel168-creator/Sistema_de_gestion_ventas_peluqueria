using System.Linq;
using System.Windows.Forms;

namespace TFI_1.Presentacion
{
    /// <summary>
    /// Extensiones para ToolStripItemCollection que evitan ArgumentNullException
    /// cuando alguno de los ítems pasados a AddRange es null.
    /// </summary>
    public static class MenuStripExtensions
    {
        public static void AddRangeSafe(this ToolStripItemCollection items, params ToolStripItem[] toolStripItems)
        {
            var validos = toolStripItems.Where(i => i != null).ToArray();
            items.AddRange(validos);
        }
    }
}
