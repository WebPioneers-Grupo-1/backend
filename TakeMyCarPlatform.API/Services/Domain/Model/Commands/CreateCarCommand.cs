using System;
using MediatR;

namespace TakeMyCar.Application.Internal.Commands
{
    public class CreateCarCommand : IRequest<int> 
    {
        public string Name { get; }
        public decimal Price { get; }
        public string Url { get; }

        public CreateCarCommand(string name, decimal price, string url)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
            if (price <= 0) throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("URL cannot be empty.", nameof(url));

            Name = name;
            Price = price;
            Url = url;
        }
    }
}
