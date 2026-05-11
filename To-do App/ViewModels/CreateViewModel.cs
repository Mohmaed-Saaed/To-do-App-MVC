using System.ComponentModel.DataAnnotations;

namespace To_do_App.ViewModels
{
    public class CreateViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
