using CommunityToolkit.Mvvm.ComponentModel;

namespace nApps.Futs.Mobile.Shared.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    private bool isBusy;

    public bool IsBusy
    {
        get => isBusy;
        set => SetProperty(ref isBusy, value);
    }


    private string? errorMessage;

    public string? ErrorMessage
    {
        get => errorMessage;
        set => SetProperty(ref errorMessage, value);
    }


    protected async Task ExecuteAsync(Func<Task> action)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            ErrorMessage = null;

            await action();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }


    protected async Task<T?> ExecuteAsync<T>(Func<Task<T>> action)
    {
        if (IsBusy)
            return default;

        try
        {
            IsBusy = true;

            ErrorMessage = null;

            return await action();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;

            return default;
        }
        finally
        {
            IsBusy = false;
        }
    }
}