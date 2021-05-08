using System;

namespace Faregosoft.NewApi.Data.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public Guid Image { get; set; }

        public string ImageFullPath => Image == Guid.Empty
            ? $"https://faregosoftnewapi.azurewebsites.net/images/noimage.png"
            : $"https://faregosoft.blob.core.windows.net/products/{Image}";
    }
}
