using System.Data.SqlClient;
using Customers.Data.Entities;
using Customers.Domain;
using Customers.Domain.Models;
using Customers.Domain.Repositories;
using Dapper;

namespace Customers.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private string _connectionString;

    public CustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<ICommandResult<CustomerId>> Create(Customer customer)
    {
        var entity = CustomerEntity.FromDomain(customer);
        var sql =
        @$" INSERT INTO Customers 
            VALUES(
                @{nameof(entity.Id)}, 
                @{nameof(entity.FirstName)},
                @{nameof(entity.LastName)},
                @{nameof(entity.PhoneNumber)},
                @{nameof(entity.PhoneRegion)})";
        try
        {
            using var connection = GetOpenConnection();
            await connection.ExecuteAsync(sql, entity);
            return CommandResult.CreateSuccess(customer.Id);
        }
        catch (Exception ex)
        {
            return CommandResult.CreateFailed<CustomerId>(ex.Message);
        }


    }

    public async Task<ICommandResult<Customer>> Get(CustomerId id)
    {
        var sqlCustomer = @$"SELECT * FROM Customers c WHERE c.Id = @{nameof(id)}";
        var sqlAddresses = @$"SELECT * FROM Addresses WHERE customerId = @{nameof(id)}";

        using var connection = GetOpenConnection();
        var customerEntity = await connection.QueryFirstAsync<CustomerEntity>(sqlCustomer, new { id });
        var Addresses = await connection.QueryAsync(sqlAddresses, new { id });

        return customerEntity.ToDomain();
    }

    public async Task<ICommandResult<Customer>> Update(Customer customer)
    {
        var entity = CustomerEntity.FromDomain(customer);
        var customerUpdateSql = @$"Update Customers Set FirstName = @{nameof(entity.FirstName)} where id = @{nameof(customer.Id)}";
        var addressesMerge = @"
                    MERGE INTO Addresses AS TARGET 
                    USING (
                    VALUES
                        (@Id, @CustomerId, @StreetName, @Number, @Observation)
                    ) AS SOURCE (Id, CustomerId, StreetName, [Number], Observation)
                    ON SOURCE.Id = TARGET.Id
                    WHEN MATCHED THEN
                    UPDATE SET 
                        StreetName = StreetName,
                        [Number] = [Number], 
                        Observation = Observation
                    WHEN NOT MATCHED BY TARGET THEN
                    INSERT (Id, CustomerId, StreetName, [Number], Observation)
                    VALUES (Id, CustomerId, StreetName, [Number], Observation)
                    WHEN NOT MATCHED BY Source then DELETE;";

        var addresses = customer.Addresses.Select(address => AddressEntity.FromDomain(customer.Id, address)).ToList();
        using var connection = GetOpenConnection();
        await connection.ExecuteAsync(customerUpdateSql, entity);
        await connection.ExecuteAsync(addressesMerge, addresses);
        return CommandResult.CreateSuccess(customer);
    }

    private SqlConnection GetOpenConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}