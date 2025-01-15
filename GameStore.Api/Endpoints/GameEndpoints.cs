﻿using System.Reflection.Metadata.Ecma335;

using GameStore.Api.DTOs;

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Api.Endpoints
{


    public static class GameEndpoints
    {

        const string GetGameEndpointName = "GetGame";

        private static readonly List<GameDTO> games = [

            new(
                1,
                "Pokemon Go",
                "Adventure",
                0.0M,
                new DateOnly(2015, 1, 15)
            ),
            new(
                2,
                "Elden Ring",
                "RPG",
                40.00M,
                new DateOnly(2017, 4, 15)
            ),
            new(
                3,
                "Sword Art Online",
                "RPG",
                99.00M,
                new DateOnly(2022, 9, 1)
            )

        ];

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {

            var group = app.MapGroup("games");


//GET /games
            group.MapGet("/", () => games);

//GET /games/1
            group.MapGet("/{id}", (int id) => {



                GameDTO? game = games.Find(game => game.Id == id);

                return game is null ? Results.NotFound() : Results.Ok(game);

            }).WithName(GetGameEndpointName);

// POST /games
            group.MapPost("/", (CreateGameDTO newGame) =>
            {

                if (string.IsNullOrEmpty(newGame.Name))
                {
                    return Results.BadRequest("Name is Required");
                }

                GameDTO game = new(
                    games.Count + 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price,
                    newGame.ReleaseDate
                );

                games.Add(game);

                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            }).WithParameterValidation();

//PUT 
            group.MapPut("/{id}", (int id, UpdateGameDTO updatedGame) =>
            {
                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                games[index] = new GameDTO(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

                return Results.NoContent();

            });

//DELETE
            group.MapDelete("/{id}", (int id) =>
            {
                games.RemoveAll(game => game.Id == id);

                return Results.NoContent();
            });

            return group;
        }


        
    }
}
