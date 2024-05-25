using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;



namespace BooksApp.Helpers;

public class TypeBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var PropertyName = bindingContext.ModelName;
        var ValueProvider = bindingContext.ValueProvider.GetValue(PropertyName);

        if (ValueProvider == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        try
        {
            var DeserializedValue = JsonSerializer.Deserialize<T>(ValueProvider.FirstValue);
            bindingContext.Result = ModelBindingResult.Success(DeserializedValue);
        }
        catch
        {
            bindingContext.ModelState.TryAddModelError(PropertyName, "Valor inválido para tipo List<int>");
        }

        return Task.CompletedTask;
    }
}
