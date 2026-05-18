namespace MauiAppExamen
{
    public partial class MainPage : ContentPage
    {
        private Vehiculos vehiculos;

        public MainPage()
        {
            InitializeComponent();
            vehiculos = new Vehiculos();
        }

        private void cmdCrear_Clicked(object sender, EventArgs e)
        {
            try
            {
                var nuevo = vehiculos.CrearVehiculo(0, txtMarca.Text, txtModelo.Text, int.Parse(txtAnio.Text));
                txtId.Text = nuevo.Id.ToString(); // Mostramos el ID generado por la DB
                DisplayAlert("Éxito", $"Vehículo creado con ID {nuevo.Id}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo crear: {ex.Message}", "OK");
            }
        }

        private void cmdLeer_Clicked(object sender, EventArgs e)
        {
            try
            {
                var vehiculo = vehiculos.ReadByID(int.Parse(txtId.Text));

                if (vehiculo == null)
                {
                    DisplayAlert("No encontrado", "No existe un vehículo con ese ID", "OK");
                    return;
                }

                txtMarca.Text = vehiculo.Marca;
                txtModelo.Text = vehiculo.Modelo;
                txtAnio.Text = vehiculo.Anio.ToString();
                DisplayAlert("Éxito", $"Vehículo encontrado: {vehiculo.Marca} {vehiculo.Modelo}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo leer: {ex.Message}", "OK");
            }
        }

        private void cmdActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                vehiculos.Update(int.Parse(txtId.Text), txtMarca.Text, txtModelo.Text, int.Parse(txtAnio.Text));
                DisplayAlert("Éxito", $"Vehículo ID {txtId.Text} actualizado", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo actualizar: {ex.Message}", "OK");
            }
        }

        private void cmdEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text); // Guardamos ANTES de limpiar
                vehiculos.Delete(id);

                txtId.Text = string.Empty;
                txtMarca.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtAnio.Text = string.Empty;

                DisplayAlert("Éxito", $"Vehículo ID {id} eliminado", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"No se pudo eliminar: {ex.Message}", "OK");
            }
        }
    }
}

