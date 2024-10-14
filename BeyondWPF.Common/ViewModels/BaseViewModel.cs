using BeyondWPF.Core.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BeyondWPF.Common.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        // Événement pour notifier les changements de propriétés
        public event PropertyChangedEventHandler PropertyChanged;

        // Méthode pour déclencher PropertyChanged
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Méthode utilitaire pour setter une propriété et notifier le changement
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // Un booléen pour gérer les états de chargement
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        // Un titre commun que tous les ViewModels peuvent utiliser
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        // Commande de base que les ViewModels peuvent réutiliser
        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??= new RelayCommand(Refresh);

        // Méthode de rafraîchissement virtuelle (les ViewModels enfants peuvent la surcharger)
        public virtual void Refresh()
        {
            // Logique par défaut pour rafraîchir
        }

        // Méthode pour gérer les erreurs (les ViewModels enfants peuvent surcharger)
        public virtual void HandleError(string message)
        {
            // Logique de gestion des erreurs par défaut
        }
    }
}
