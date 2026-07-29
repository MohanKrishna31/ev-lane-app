using System;
using System.Collections.Generic;
using System.Text;

namespace nApps.Futs.Mobile.Shared.Models;

public class SelectOption<T>
{
    public T? Value { get; set; }

    public string Text { get; set; } = string.Empty;
}