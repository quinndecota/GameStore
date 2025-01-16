﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.DTOs



{
    public record class CreateGameDTO(
        [Required][StringLength(50)] string Name,
        int GenreId,
        [Range(0,100)] decimal Price,
        [Required] DateOnly ReleaseDate);
}
