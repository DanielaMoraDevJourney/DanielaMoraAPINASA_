using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DanielaMoraAPINASA.Models;
using DanielaMoraAPINASA.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DanielaMoraAPINASA.ViewModels
{

    public class DMApodViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DMApodViewModel()
        {
            ChosenDate_DM = DateTime.Now;
        }
        private DateTime dateTime_DM;
        public DateTime ChosenDate_DM
        {
            get { return dateTime_DM; }
            set
            {
                if (value != dateTime_DM)
                {
                    dateTime_DM = value;
                    NotifyPropertyChanged();
                }
                _ = GetPictureOfTheDay(dateTime_DM);
            }
        }
        //La diferencia entre title y Title es que title es un campo privado usado para guardar
        //el valor de title mientras que Title es una propiedad pública que proporciona acceso
        //al campo title y lanza el evento PropertyChanged cuando su valor cambia, lo que es usado
        //para el data binding y las notificaciones de cambio.
        //El getter retorna el valor del campo title mientras que el setter permite asignar un
        //nuevo valor al campo title.
        private string title_DM;
        public string Title_DM
        {
            get { return title_DM; }
            set
            {
                if (value != title_DM)
                {
                    title_DM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Uri imageUri_DM;
        public Uri ImageURI_DM
        {
            get { return imageUri_DM; }
            set
            {
                if (imageUri_DM != value)
                {
                    imageUri_DM = value;
                    NotifyPropertyChanged();
                }
            }

        }
        private string didactic_DM;
        public string Didactic_DM
        {
            get { return didactic_DM; }
            set
            {
                if (didactic_DM != value)
                {
                    didactic_DM = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DMApodService service_DM;
        public DMApodService Service_DM
        {
            get
            {
                if (service_DM == null)
                {
                    service_DM = new DMApodService();
                }
                return service_DM;
            }
        }
        private async Task GetPictureOfTheDay(DateTime day)
        {
            DMApod dto = await Service_DM.GetImage_DM(day);
            if (dto == null)
            {
                ImageURI_DM = new Uri("https://image.freepik.com/vector-gratis/error-404-no-encontradoefecto-falla_8024-5.jpg");
                Didactic_DM = "";
                Title_DM = "Intenta con otra fecha";
            }
            else
            {
                ImageURI_DM = new Uri(dto.hdurl_DM);
                Didactic_DM = dto.explanation_DM;
                Title_DM = dto.title_DM;
            }
        }
    }
}
