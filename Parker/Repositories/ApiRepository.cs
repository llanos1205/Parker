using System.Net;
using System.Text;
using System.Text.Json;
using Parker.Interfaces;
using Parker.Models;

namespace Parker.Repositories;

public class ApiRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly HttpClient _httpClient;
    private string _accessToken = "";
    private string _refreshToken = "";

    public ApiRepository()
    {
        _httpClient = new HttpClient();
        // Configure base URL or other settings for your API
        _httpClient.BaseAddress = new Uri("");
    }

    public async Task<T> GetAsync<T>(string endpoint, string accessToken)
    {
        try
        {
            // Include the access token in the request headers
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content);
            }

            // Handle non-success status codes here
            // You may want to throw an exception or return a default value
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., network errors) here
            // You may want to log the error or throw a custom exception
        }
        finally
        {
            // Clear the authorization header after the request
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        return default; // Return a default value or null on failure
    }

    public async Task<bool> GetTokensAsync(string endpoint, AuthModels.CredentialsModel credentials)
    {
        try
        {
            var jsonData = JsonSerializer.Serialize(credentials);
            var payload = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, payload);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<AuthModels.TokenModel>(content);

                // Store the access token and refresh token
                this._accessToken = tokenResponse?.Access;
                this._refreshToken = tokenResponse?.Refresh;

                return true;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return false;
            }
            // Handle non-success status codes here
            // You may want to throw an exception or return a default value
        }
        catch (Exception ex)
        {
            var d = ex;
            // Handle exceptions (e.g., network errors) here
            // You may want to log the error or throw a custom exception
        }


        return false;
    }

    // Add a method for refreshing the access token
    public async Task<bool> RefreshAccessTokenAsync()
    {
        // Implement token refresh logic here
        // Make a request to the token endpoint, and return the new access token
        // You may want to handle token expiration and error scenarios

        return false; // Replace with the actual new access token
    }

    public async Task<bool> PostAsync<T>(string endpoint, T data, string accessToken)
    {
        try
        {
            // Include the access token in the request headers
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            // Handle non-success status codes here
            // You may want to throw an exception or return false
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., network errors) here
            // You may want to log the error or throw a custom exception
        }
        finally
        {
            // Clear the authorization header after the request
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        return false; // Return false on failure
    }


    public TEntity GetById(int id)
    {
        return default;
    }

    public List<TEntity> GetAll()
    {
        return default;
    }

    public void Add(TEntity entity)
    {
    }

    public void Update(TEntity entity)
    {
    }

    public void Delete(int id)
    {
    }
}