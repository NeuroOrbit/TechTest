using Microsoft.Extensions.Logging;
using Rabobank.TechnicalTest.GCOB.Dtos;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Rabobank.TechnicalTest.GCOB.Repositories
{
    public class InMemoryAddressRepository : IAddressRepository
    {
        private ConcurrentDictionary<int, AddressDto> Addresses { get; } = new ConcurrentDictionary<int, AddressDto>();
        private ILogger _logger;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="logger"></param>
        public InMemoryAddressRepository(ILogger<InMemoryAddressRepository> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GenerateIdentityAsync
        /// </summary>
        /// <returns></returns>
        public Task<int> GenerateIdentityAsync()
        {
            _logger.LogDebug("Generating Address identity");
            return Task.Run(() =>
            {
                if (Addresses.Count == 0) return 1;

                var x = Addresses.Values.Max(c => c.Id);
                return ++x;
            });
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task<AddressDto> GetAsync(int identity)
        {
            _logger.LogDebug($"Find Many Addresses with identity {identity}");

            if (!Addresses.ContainsKey(identity)) throw new Exception(identity.ToString());
            _logger.LogDebug($"Found Address with identity {identity}");
            return Task.FromResult(Addresses[identity]);
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task InsertAsync(AddressDto address)
        {
            if (Addresses.ContainsKey(address.Id))
            {
                throw new Exception(
                    $"Cannot insert address with identity '{address.Id}' " +
                    "as it already exists in the collection");
            }

            Addresses.TryAdd(address.Id, address);
            _logger.LogDebug($"New address inserted [ID:{address.Id}]. " +
                          $"There are now {Addresses.Count} legal entities in the store.");
            return Task.FromResult(address);
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Task UpdateAsync(AddressDto address)
        {
            if (!Addresses.ContainsKey(address.Id))
            {
                throw new Exception(
                    $"Cannot update address with identity '{address.Id}' " +
                    "as it doesn't exist");
            }

            Addresses[address.Id] = address;
            _logger.LogDebug($"New address updated [ID:{address.Id}].");

            return Task.FromResult(address);
        }


    }
}
