namespace PM2E130148;

public partial class CalculadoraEnvios : ContentPage
{
    public CalculadoraEnvios()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        if (string.IsNullOrWhiteSpace(txtPeso.Text))
        {
            await DisplayAlert("Atención", "Por favor ingrese la descripción del paquete.", "Ok");
            return;
        }

        //Validamos que el peso sea un número válido y mayor a 0
        if (!double.TryParse(txtAltura.Text, out double peso) || peso <= 0)
        {
            await DisplayAlert("Atención", "Por favor ingrese un peso válido mayor a 0 kg.", "Ok");
            return;
        }

        // Validamos que se haya seleccionado una zona en el Picker
        if (pckDestino.SelectedIndex == -1)
        {
            await DisplayAlert("Atención", "Por favor seleccione una zona de destino.", "Ok");
            return;
        }

        // Determinamos tarifa por zona según la selección
        double tarifaZona = 0;
        string zonaSeleccionada = pckDestino.SelectedItem.ToString();

        // Convertimos la zona seleccionada al formato solicitado ("Envio Local", "Envio Nacional", "Envio Internacional")
        string textoZona = zonaSeleccionada switch
        {
            "Zona Local" => "Envio Local",
            "Zona Nacional" => "Envio Nacional",
            "Zona Internacional" => "Envio Internacional",
            _ => "Envio Local"
        };

        switch (zonaSeleccionada)
        {
            case "Zona Local":
                tarifaZona = 50.0;
                break;
            case "Zona Nacional":
                tarifaZona = 100.0;
                break;
            case "Zona Internacional":
                tarifaZona = 250.0;
                break;
            default:
                tarifaZona = 50.0;
                break;
        }

        // Determinamos recargo por servicio exprés y su texto
        bool esExpres = chkExpres.IsChecked;
        double recargoExpres = esExpres ? 100.0 : 0.0;
        string textoExpres = esExpres ? "Con servicio exprés" : "Sin servicio exprés";

        //Calculamos costo total: (Peso x Tarifa de Zona) + Recargo Exprés
        double costoTotal = (peso * tarifaZona) + recargoExpres;

        lblValorIMC.Text = $"Costo Total: L {costoTotal:F2}";
        lblResultado.Text = $"Resultado";
        lblResultado.TextColor = Colors.Black;

        lblRangosExplicacion.Text = $"{textoZona}\n" +
                                   $"({peso} kg - {textoExpres})";
        lblRangosExplicacion.IsVisible = true;

        // Cambiamos el color de fondo y borde del Border según el monto total
        if (costoTotal < 300)
        {
            brResultado.BackgroundColor = Color.FromArgb("#DCFCE7");
            brResultado.Stroke = Color.FromArgb("#16A34A");
        }
        else
        {
            brResultado.BackgroundColor = Color.FromArgb("#E0F2FE"); 
            brResultado.Stroke = Color.FromArgb("#0284C7"); 
        }

        // 9. Hacer visible la tarjeta de resultados
        brResultado.IsVisible = true;
    }
}