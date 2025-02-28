using System;

using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient(HttpClient httpClient)
{

    public async Task<GameSummary[]> GetGamesAsync() =>
        await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

    public async Task AddGameAsync(GameDetails game) =>
        await httpClient.PostAsJsonAsync("games", game);

    public async Task<GameDetails> GetGameAsync(int id) =>
        await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ?? throw new Exception("Game Not Found");

    public async Task updateGameAsync(GameDetails updatedGame) =>
        await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

    public async Task DeleteGameAsync(GameSummary game) =>
        await httpClient.DeleteAsync($"games/{game.Id}");

}