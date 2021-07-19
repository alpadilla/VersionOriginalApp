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
using System.Windows.Shapes;
using VersionOriginalApp.Services.Interfaces;
using VersionOriginalApp.ViewModels;
using Exception = System.Exception;

namespace VersionOriginalApp.Forms.Rents
{
    /// <summary>
    /// Interaction logic for ReturnRentForm.xaml
    /// </summary>
    public partial class ReturnRentForm : Window
    {
        public ReturnRentForm(IVersionOriginalApiService versionOriginalApiService)
        {
            InitializeComponent();
            DataContext = new ReturnRentViewModel(versionOriginalApiService);
        }

        private async void ReturnRent_OnClick(object sender, RoutedEventArgs e)
        {
            try {
                if (string.IsNullOrEmpty(tbRentCode.Text)) {
                    MessageBox.Show("Debe introducir un código de renta válido.");
                    return;
                }

                var returnResult = await ((ReturnRentViewModel) DataContext).MakeReturn(tbRentCode.Text);

                if (!returnResult.IsReturned) {
                    MessageBox.Show(
                        $"La renta con el código proporcionado ya se ha devuelto en la fecha" +
                        $": {returnResult.ReturnedDate:d}.");
                    return;
                }

                if (!returnResult.Returned) {
                    MessageBox.Show(returnResult.Exist
                        ? "A occurrido un error tratando de hacer la devolución de la renta."
                        : "No existe ninguna renta con el código proporcionado.");
                    return;
                }

                MessageBox.Show(returnResult.OutOfDateAmount > 0
                    ? $"Debe abonar un monto de {returnResult.OutOfDateAmount} euros por entregar la renta fuera de fecha."
                    : "Se realizó la devolución de la renta correctamente.");
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
