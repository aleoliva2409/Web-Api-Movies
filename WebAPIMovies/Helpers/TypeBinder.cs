using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WebAPIMovies.Helpers
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nameProp = bindingContext.ModelName;
            var valueProp = bindingContext.ValueProvider.GetValue(nameProp);

            if (valueProp == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(valueProp.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {

                bindingContext.ModelState.TryAddModelError(nameProp, "Invalid type");
            }

            return Task.CompletedTask;

        }
    }
}
