using MauiAppHotel.Models;

namespace MauiAppHotel.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    private readonly App propriedadesApp;

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        propriedadesApp = (App)Application.Current!;

        pck_quarto.ItemsSource = propriedadesApp.lista_quartos;
        pck_quarto.SelectedIndex = 0;

        DateTime hoje = DateTime.Today;

        dtpck_checkin.MinimumDate = hoje;
        dtpck_checkin.MaximumDate = hoje.AddMonths(6);

        DateTime checkin = dtpck_checkin.Date ?? hoje;

        dtpck_checkout.MinimumDate = checkin.AddDays(1);
        dtpck_checkout.MaximumDate = checkin.AddMonths(6);
        dtpck_checkout.Date = checkin.AddDays(1);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (pck_quarto.SelectedItem is not Quarto quarto)
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione uma acomodação.",
                "OK"
            );

            return;
        }

        DateTime checkin = dtpck_checkin.Date ?? DateTime.Today;
        DateTime checkout = dtpck_checkout.Date ?? checkin.AddDays(1);

        if (checkout <= checkin)
        {
            await DisplayAlertAsync(
                "Atenção",
                "A data de check-out deve ser posterior ao check-in.",
                "OK"
            );

            return;
        }

        var hospedagem = new Hospedagem
        {
            QuartoSelecionado = quarto,
            QntAdultos = Convert.ToInt32(stp_adultos.Value),
            QntCriancas = Convert.ToInt32(stp_criancas.Value),
            DataCheckIn = checkin,
            DataCheckOut = checkout
        };

        await Navigation.PushAsync(
            new HospedagemContratada
            {
                BindingContext = hospedagem
            }
        );
    }

    private void dtpck_checkin_DateSelected(
        object sender,
        DateChangedEventArgs e)
    {
        DateTime novaData = e.NewDate ?? DateTime.Today;

        dtpck_checkout.MinimumDate = novaData.AddDays(1);
        dtpck_checkout.MaximumDate = novaData.AddMonths(6);

        DateTime checkout = dtpck_checkout.Date ?? novaData.AddDays(1);

        if (checkout <= novaData)
        {
            dtpck_checkout.Date = novaData.AddDays(1);
        }
    }

    private async void Sobre_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Sobre());
    }
}