namespace MauiAppHotel.Models
{
    public class Hospedagem
    {
        public string CodigoReserva { get; set; } =
            $"HA-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";

        public Quarto QuartoSelecionado { get; set; } = null!;

        public int QntAdultos { get; set; }

        public int QntCriancas { get; set; }

        public DateTime DataCheckIn { get; set; }

        public DateTime DataCheckOut { get; set; }

        public int Estadia
        {
            get => DataCheckOut.Subtract(DataCheckIn).Days;
        }

        public double ValorTotal
        {
            get
            {
                double valorAdultos =
                    QntAdultos * QuartoSelecionado.ValorDiariaAdulto;

                double valorCriancas =
                    QntCriancas * QuartoSelecionado.ValorDiariaCrianca;

                return (valorAdultos + valorCriancas) * Estadia;
            }
        }
    }
}