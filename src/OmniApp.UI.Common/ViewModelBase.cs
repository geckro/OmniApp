using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OmniApp.UI.Common;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Raises the event <see cref="PropertyChanged" />.
    ///     Uses the <see cref="System.Runtime.CompilerServices.CallerMemberNameAttribute" />, which automatically passes the name of the caller property.
    /// </summary>
    /// <param name="propertyName"></param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    ///     Sets a property and raises the <see cref="OnPropertyChanged" /> event.
    /// </summary>
    /// <param name="property">A reference to the backing field of the property.</param>
    /// <param name="value">The new value to assign to the property.</param>
    /// <param name="propertyName">
    ///     The name of the property being set. The compiler provides this automatically through
    ///     <see cref="System.Runtime.CompilerServices.CallerMemberNameAttribute" />, so it is typically not specified explicitly.
    /// </param>
    /// <typeparam name="T">The type of the property being set.</typeparam>
    protected virtual bool SetProperty<T>(ref T property, T value, [CallerMemberName] string? propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(property, value))
        {
            return false;
        }

        property = value;
        OnPropertyChanged(propertyName);

        return true;
    }
}
